using Core.Data.Entities;
using Models;

namespace Web.Services.Interfaces;

public interface IVacationDocumentService
{
    void GenerateDocument(User user, Vacation vacation);
}