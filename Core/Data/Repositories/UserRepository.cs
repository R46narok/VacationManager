using Core.Data.Entities;
using Core.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly VacationManagerDbContext _context;

    public UserRepository(VacationManagerDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IQueryable<User> GetUsers()
    {
        return _context.Users.Include(x => x.Team).Include(x => x.Role).AsQueryable();
    }

    public User GetUser(string username)
    {
        return _context.Users.Include(x => x.Team).Include(x => x.Role).FirstOrDefault(x => x.UserName == username);
    }

    public User GetUserById(string id)
    {
        return _context.Users.FirstOrDefault(x => x.Id == id);
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void EditUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void DeleteUser(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}