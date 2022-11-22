using System.ComponentModel.DataAnnotations;
using VacationManager.Core;

namespace VacationManager.Data.Entities;

public enum VacationType
{
    Paid = 0,
    Unpaid,
    Medical
}

public class Vacation : EntityBase<int>
{
    public AppUser Applicant { get; set; }
    
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public DateTime CreatedOn { get; set; }
    
    public bool? IsHalfDay { get; set; }
    public bool IsApproved { get; set; }
    
    public VacationType Type { get; set; }
    public byte[]? AttachedFile { get; set; }
}