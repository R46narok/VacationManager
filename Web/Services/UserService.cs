using System;
using System.Collections.Generic;
using System.Linq;
using Core.Data.Entities;
using Core.Data.Repositories.Interfaces;
using Models;
using Web.Services.Interfaces;

namespace Web.Services;

public class UserService : IUserService
{
    public readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public List<User> GetUsers()
    {
        return _userRepository.GetUsers().ToList();
    }

    public User GetUser(string username)
    {
        return _userRepository.GetUser(username);
    }

    public User GetUserById(string id)
    {
        return _userRepository.GetUserById(id);
    }

    public void AddUser(User user)
    {
        _userRepository.AddUser(user);
    }

    public void ChangeRole(User user, Role role)
    {
        user.Role = role;
        _userRepository.EditUser(user);
    }

    public void JoinTeam(User user, Team team)
    {
        user.Team = team;
        _userRepository.EditUser(user);
    }

    public void LeaveTeam(User user)
    {
        user.Team = null;
        _userRepository.EditUser(user);
    }

    public void EditUser(User user)
    {
        _userRepository.EditUser(user);
    }

    public void DeleteUser(User user)
    {
        _userRepository.DeleteUser(user);
    }
}