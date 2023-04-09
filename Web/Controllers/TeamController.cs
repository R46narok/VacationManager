using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Data;
using Core.Data.Entities;
using Core.Data.SearchModel;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.SearchModel;
using Repositories.Helpers;
using ViewModels.Input;
using Web.Services.Interfaces;

namespace Web.Controllers;

public class TeamController : Controller
{
    private readonly IMapper mapper;
    private readonly IProjectService projectService;
    private readonly ITeamService teamService;
    private readonly IUserService userService;

    public TeamController(IMapper mapper, ITeamService teamService, IUserService userService,
        IProjectService projectService)
    {
        this.mapper = mapper;
        this.teamService = teamService;
        this.userService = userService;
        this.projectService = projectService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var search = new TeamSearch();

        search.Results = teamService.GetTeams();

        return View(search);
    }

    [HttpGet]
    public IActionResult Search(TeamSearch search)
    {
        search.Results = teamService.GetTeams();
        search.Results.ForEach(x => x.TeamLeader = userService.GetUserById(x.TeamLeaderId));

        if (search.Name is not null) search.Results = search.Results.Where(x => x.Name.Contains(search.Name)).ToList();
        if (search.TeamLeadNames is not null)
            search.Results = search.Results.Where(x =>
                x.TeamLeader is not null && (x.TeamLeader.FirstName.Contains(search.TeamLeadNames) ||
                                             x.TeamLeader.LastName.Contains(search.TeamLeadNames))).ToList();

        return View("Index", search);
    }

    [HttpGet]
    [Route("team/delete/{id}")]
    public IActionResult Delete([FromRoute] string id)
    {
        if (Logged.CEOAuth())
        {
            var team = teamService.GetTeam(id);
            foreach (var user in userService.GetUsers())
                if (user.TeamId == id)
                    user.TeamId = null;

            teamService.DeleteTeam(team);
            return RedirectToAction("Index", "Team");
        }

        return Unauthorized();
    }

    #region CreateTeam

    [HttpGet]
    public IActionResult Create()
    {
        if (Logged.CEOAuth())
        {
            var model = new TeamViewModel();
            return View(model);
        }

        return Unauthorized();
    }

    [HttpGet("Team/Edit/{id}")]
    public IActionResult Edit(string id)
    {
        var model = new AddDeveloperViewModel();
        return View(model);
    }

    [HttpPost("Team/Edit/{teamId}")]
    public IActionResult Edit(AddDeveloperViewModel model, string teamId)
    {
        var team = teamService.GetTeam(teamId);
        if (team.Developers is null) team.Developers = new List<User>();
        var developer = userService.GetUser(model.DeveloperUsername);
        teamService.AddUserToTeam(developer, team);
        return RedirectToAction("Index", "Team");
    }

    [HttpPost]
    public IActionResult Create(TeamViewModel model) 
    {
        if (ModelState.IsValid)
        {
            var team = mapper.Map<Team>(model);
            var teamMembers = new List<User>();
            var teamLeader = userService.GetUser(model.TeamLeaderUsername);
            var usernames = model.DevelopersUsernames.Split(' ').ToList();
            foreach (var username in usernames) teamMembers.Add(userService.GetUser(username));
            teamLeader.Team = team;

            team.Developers = teamMembers;
            team.TeamLeader = teamLeader;

            team.Project = projectService.GetProjects().FirstOrDefault(x => x.Name == model.ProjectName);
            teamService.AddTeam(team);

            return RedirectToAction("Index", "Team");
        }

        return View(model);
    }

    #endregion
}