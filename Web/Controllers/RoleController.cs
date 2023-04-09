using System.Linq;
using AutoMapper;
using Core.Data;
using Core.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.SearchModel;
using Repositories.Helpers;
using ViewModels.Input;
using Web.Services.Interfaces;

namespace Web.Controllers;

public class RoleController : Controller
{
    private readonly IMapper _mapper;
    private readonly IRoleService _roleService;

    public RoleController(IMapper mapper, IRoleService roleService)
    {
        _mapper = mapper;
        _roleService = roleService;
    }

    #region DeleteRole

    [HttpGet]
    [Route("role/delete/{id}")]
    public IActionResult Delete([FromRoute] string id)
    {
        if (Logged.CEOAuth())
        {
            if (id is null) return NotFound();
            var deleteRole = _roleService.GetRole(id);
            if (deleteRole is null) return NotFound();
            _roleService.DeleteRole(deleteRole);
            return RedirectToAction("Index", "Role");
        }

        return Unauthorized();
    }

    #endregion

    [HttpGet]
    public IActionResult Index()
    {
        var search = new RoleSearch();
        search.Roles = _roleService.GetRoles();
        return View(search);
    }

    [HttpGet]
    public IActionResult Search(RoleSearch model)
    {
        model.Roles = _roleService.GetRoles();
        if (model.Name is not null) model.Roles = model.Roles.Where(x => x.Name.Contains(model.Name)).ToList();
        return View("Index", model);
    }

    #region CreateRole

    [HttpGet]
    public IActionResult Create()
    {
        if (Logged.CEOAuth())
        {
            var model = new RoleViewModel();
            return View(model);
        }

        return Unauthorized();
    }

    [HttpPost]
    public IActionResult Create(RoleViewModel model)
    {
        if (ModelState.IsValid)
        {
            var role = _mapper.Map<Role>(model);
            _roleService.AddRole(role);
            return RedirectToAction("Index", "Role");
        }

        return View(model);
    }

    #endregion

    #region EditRole

    [HttpGet]
    [Route("role/edit/{id}")]
    public IActionResult Edit([FromRoute] string id)
    {
        if (Logged.CEOAuth())
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var role = _roleService.GetRole(id);
            var model = new EditRoleViewModel();
            model.Id = id;
            model.Name = role.Name;
            return View(model);
        }

        return Unauthorized();
    }

    [HttpPost("role/edit/{id}")]
    public IActionResult Edit(RoleViewModel model, string id)
    {
        if (ModelState.IsValid)
        {
            var role = _mapper.Map<Role>(model);
            role.Id = id;
            _roleService.EditRole(role);
            return RedirectToAction("Index", "Role");
        }

        // return View(model);
        return BadRequest();
    }

    #endregion
}