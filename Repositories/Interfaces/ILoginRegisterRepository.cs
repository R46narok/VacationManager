﻿using Models;

namespace Repositories.Interfaces;

public interface ILoginRegisterRepository
{
    void Login(string username, string password);
    void Register(User user);
    void ChangePassword(string username, string newPassword);
}