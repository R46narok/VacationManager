using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.SearchModel;
using Repositories.Helpers;
using Repositories.Interfaces;
using ViewModels.Input;
using Web.Services.Interfaces;

namespace Web.Controllers;

public class UserController : Controller
{
    private readonly ILoginRegisterRepository loginRegisterRepository;
    private readonly IMapper mappingProfile;
    private readonly IRoleService roleService;
    private ITeamService teamService;
    private readonly IUserService userService;

    public UserController(IMapper _mappingProfile, ILoginRegisterRepository _loginRegisterRepository,
        IRoleService _roleService,
        ITeamService _teamService, IUserService _userService)
    {
        mappingProfile = _mappingProfile;
        loginRegisterRepository = _loginRegisterRepository;
        roleService = _roleService;
        teamService = _teamService;
        userService = _userService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var search = new UserSearch();
        search.Results = userService.GetUsers();
        return View(search);
    }

    [HttpGet]
    public IActionResult Search(UserSearch search)
    {
        search.Results = userService.GetUsers();
        if (search.FirstName is not null)
            search.Results = search.Results.Where(x => x.FirstName.Contains(search.FirstName)).ToList();
        if (search.LastName is not null)
            search.Results = search.Results.Where(x => x.LastName.Contains(search.LastName)).ToList();
        if (search.Role != "Избери роля...")
            search.Results = search.Results.Where(x => x.Role.Name == search.Role).ToList();
        if (search.Team != "Избери екип...")
            search.Results = search.Results.Where(x => x.Team.Name == search.Team).ToList();

        return View("Index", search);
    }

    [HttpGet]
    [Route("user/profile/{username}")]
    public IActionResult Profile([FromRoute] string username)
    {
        var user = userService.GetUser(username);

        return View(user);
    }

    [HttpGet]
    [Route("user/delete/{username}")]
    public IActionResult Delete([FromRoute] string username)
    {
        if (Logged.CEOAuth())
        {
            var user = userService.GetUser(username);

            userService.DeleteUser(user);
            return RedirectToAction("Index", "User");
        }

        return Unauthorized();
    }

    #region Register

    [HttpGet]
    public IActionResult Register()
    {
        if (Logged.CEOAuth())
        {
            var model = new RegisterUserViewModel();

            return View(model);
        }

        return Unauthorized();
    }

    [HttpPost]
    public IActionResult Register(RegisterUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("passConfirm", "Паролите не съвпадат!");
                return View(model);
            }

            var user = mappingProfile.Map<User>(model);
            var roles = roleService.GetRoles();
            user.Role = roles.SingleOrDefault(x => x.Name == model.RoleName);
            try
            {
                // user.TeamId = "0";
                // user.Team = teamService.GetTeam("0");
                loginRegisterRepository.Register(user);
                Logged.User = user;
            }
            catch (Exception)
            {
                ModelState.AddModelError("usernameExists", "Потребител със същото име вече съществува");
                return View(model);
            }
        }
        else
        {
            return View(model);
        }

        return RedirectToAction("Index", "Home");
    }

    #endregion

    #region Login

    [HttpGet]
    public IActionResult Login()
    {
        var model = new LoginUserViewModel();

        return View(model);
    }

    [HttpPost]
    public IActionResult Login(LoginUserViewModel model)
    {
        if (ModelState.IsValid)
            try
            {
                var username = model.Username;
                var password = model.Password;
                loginRegisterRepository.Login(username, password);
            }
            catch (Exception)
            {
                ModelState.AddModelError("loginError", "Невалидно потребителско име или парола");
                return View(model);
            }
        else
            return View(model);

        return RedirectToAction("Index", "Home");
    }

    #endregion
}