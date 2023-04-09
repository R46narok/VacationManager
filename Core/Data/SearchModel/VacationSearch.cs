using System;
using System.Collections.Generic;
using Core.Data.Entities;

namespace Models.SearchModel;

public class VacationSearch
{
    public DateTime FromDate { get; set; }
    public List<Vacation> Result { get; set; }
}