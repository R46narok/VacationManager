using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Data;
using Core.Data.Entities;
using Core.Data.SearchModel;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.SearchModel;
using Repositories.Helpers;
using ViewModels.Input;
using Web.Services.Interfaces;

namespace Web.Controllers;

public class ProjectController : Controller
{
    private readonly IMapper _mapper;

    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService, IMapper mapper)
    {
        _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public IActionResult Index()
    {
        var search = new ProjectSearch();
        search.Results = _projectService.GetProjects();
        return View(search);
    }

    [HttpGet]
    public IActionResult Search(ProjectSearch search)
    {
        search.Results = _projectService.GetProjects();
        if (search.Name is not null) search.Results = search.Results.Where(x => x.Name.Contains(search.Name)).ToList();

        if (search.Description is not null)
            search.Results = search.Results.Where(x => x.Description.Contains(search.Description)).ToList();

        return View("Index", search);
    }

    [HttpGet]
    public IActionResult Create()
    {
        if (Logged.CEOAuth())
        {
            var model = new ProjectViewModel();
            return View(model);
        }

        return Unauthorized();
    }

    [HttpPost]
    public IActionResult Create(ProjectViewModel viewModel)
    {
        var project = _mapper.Map<Project>(viewModel);
        project.Teams = new List<Team>();
        _projectService.AddProject(project);
        return RedirectToAction("Index", "Project");
    }

    [HttpGet]
    [Route("Project/Details/{id}")]
    public IActionResult Details([FromRoute] string id)
    {
        var project = _projectService.GetProject(id);

        var model = new ProjectViewModel
        {
            Name = project.Name,
            Description = project.Description,
            Teams = project.Teams
        };

        return View(model);
    }

    [HttpGet("Project/Edit/{id}")]
    public IActionResult Edit(string id)
    {
        if (Logged.CEOAuth())
        {
            var project = _projectService.GetProject(id);
            var projectViewModel = _mapper.Map<ProjectViewModel>(project);
            return View(projectViewModel);
        }

        return Unauthorized();
    }

    [HttpGet("Project/Delete/{id}")]
    public IActionResult Delete(string id)
    {
        if (Logged.CEOAuth())
        {
            _projectService.DeleteProject(_projectService.GetProject(id));
            return RedirectToAction("Index", "Project");
        }

        return Unauthorized();
    }

    [HttpPost("Project/Edit/{id}")]
    public IActionResult Edit(ProjectViewModel viewModel, string id)
    {
        var project = _mapper.Map<Project>(viewModel);
        project.Id = id;
        _projectService.EditProject(project);

        return RedirectToAction("Index", "Project");
    }
}