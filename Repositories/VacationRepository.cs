using System;
using System.Linq;
using DataAccess;
using Models;
using Repositories.Interfaces;

namespace Repositories;

public class VacationRepository : IVacationRepository
{
    private readonly VacationManagerDbContext _context;

    public VacationRepository(VacationManagerDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IQueryable<Vacation> GetVacations()
    {
        return _context.Vacations.AsQueryable();
    }

    public Vacation GetVacation(string name)
    {
        return _context.Vacations.Find(name);
    }

    public void AddVacation(Vacation vacation)
    {
        _context.Vacations.Add(vacation);
        _context.SaveChanges();
    }

    public void EditVacation(Vacation vacation)
    {
        _context.Vacations.Update(vacation);
        _context.SaveChanges();
    }

    public void DeleteVacation(Vacation vacation)
    {
        _context.Vacations.Remove(vacation);
        _context.SaveChanges();
    }
}