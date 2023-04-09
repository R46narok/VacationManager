using System;
using System.Collections.Generic;
using System.Linq;
using Core.Data.Entities;
using Core.Data.Enums;
using Core.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using Web.Services.Interfaces;

namespace Web.Services;

public class VacationService : IVacationService
{
    private readonly IVacationRepository _vacationRepository;

    public VacationService(IVacationRepository vacationRepository)
    {
        _vacationRepository = vacationRepository ?? throw new ArgumentNullException(nameof(vacationRepository));
    }

    public List<Vacation> GetVacations()
    {
        return _vacationRepository.GetVacations().Include(x => x.Applicant).ToList();
    }

    public Vacation GetVacation(string id)
    {
        return GetVacations().Find(x => x.Id == id);
    }

    public void AddVacation(Vacation vacation)
    {
        _vacationRepository.AddVacation(vacation);
    }

    public void ApproveVacation(Vacation vacation)
    {
        vacation.Status = ApprovalStatus.Approved;
        _vacationRepository.EditVacation(vacation);
    }

    public void EditVacation(Vacation vacation)
    {
        _vacationRepository.EditVacation(vacation);
    }

    public void DeleteVacation(Vacation vacation)
    {
        _vacationRepository.DeleteVacation(vacation);
    }
}