using System.Collections.Generic;
using Core.Data.Entities;
using Models;

namespace Web.Services.Interfaces;

public interface IVacationService
{
    List<Vacation> GetVacations();
    Vacation GetVacation(string id);
    void AddVacation(Vacation vacation);
    void ApproveVacation(Vacation vacation);
    void EditVacation(Vacation vacation);
    void DeleteVacation(Vacation vacation);
}