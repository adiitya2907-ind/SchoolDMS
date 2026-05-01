using SchoolDMS.API.Models.Entities;

namespace SchoolDMS.API.Repositories.Interfaces
{
    public interface IVisitRepository : IRepository<Visit>
    {
        Task<Visit?> GetVisitWithDetailsAsync(int visitId);
        Task<IEnumerable<Visit>> GetVisitsWithDetailsAsync(int? engineerId = null, int? schoolId = null, int? statusId = null);
        Task<IEnumerable<Visit>> GetPendingVerificationsAsync();
    }
}
