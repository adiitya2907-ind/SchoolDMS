using Microsoft.EntityFrameworkCore;
using SchoolDMS.API.Data;
using SchoolDMS.API.Models.Entities;
using SchoolDMS.API.Repositories.Interfaces;

namespace SchoolDMS.API.Repositories
{
    public class ApprovalRepository : GenericRepository<ApprovalWorkflow>, IApprovalRepository
    {
        public ApprovalRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ApprovalWorkflow>> GetHistoryByVisitIdAsync(int visitId)
        {
            return await _dbSet
                .Include(a => a.Verifier)
                .Where(a => a.VisitId == visitId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }
    }
}
