using System.Linq;
using Models;

namespace Repositories.Interfaces;

public interface IVacationRepository
{
    IQueryable<Vacation> GetVacations();
    Vacation GetVacation(string id);
    void AddVacation(Vacation vacation);
    void EditVacation(Vacation vacation);
    void DeleteVacation(Vacation vacation);
}