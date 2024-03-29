﻿using Core.Data.Entities;

namespace Core.Data.Repositories.Interfaces;

public interface IUserRepository
{
    IQueryable<User> GetUsers();
    User GetUser(string username);
    User GetUserById(string id);
    void AddUser(User user);
    void EditUser(User user);
    void DeleteUser(User user);
}