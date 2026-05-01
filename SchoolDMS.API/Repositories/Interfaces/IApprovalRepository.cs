using SchoolDMS.API.Models.Entities;

namespace SchoolDMS.API.Repositories.Interfaces
{
    public interface IApprovalRepository : IRepository<ApprovalWorkflow>
    {
        Task<IEnumerable<ApprovalWorkflow>> GetHistoryByVisitIdAsync(int visitId);
    }
}
