using Microsoft.EntityFrameworkCore;
using SchoolDMS.API.Data;
using SchoolDMS.API.Models.Entities;
using SchoolDMS.API.Models.Enums;
using SchoolDMS.API.Repositories.Interfaces;

namespace SchoolDMS.API.Repositories
{
    public class VisitRepository : GenericRepository<Visit>, IVisitRepository
    {
        public VisitRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Visit?> GetVisitWithDetailsAsync(int visitId)
        {
            return await _dbSet
                .Include(v => v.School)
                .Include(v => v.Engineer)
                .Include(v => v.Project)
                .Include(v => v.Documents)
                .Include(v => v.ApprovalWorkflows)
                .FirstOrDefaultAsync(v => v.VisitId == visitId);
        }

        public async Task<IEnumerable<Visit>> GetVisitsWithDetailsAsync(int? engineerId = null, int? schoolId = null, int? statusId = null)
        {
            var query = _dbSet
                .Include(v => v.School)
                .Include(v => v.Engineer)
                .Include(v => v.Project)
                .AsQueryable();

            if (engineerId.HasValue)
                query = query.Where(v => v.EngineerId == engineerId.Value);

            if (schoolId.HasValue)
                query = query.Where(v => v.SchoolId == schoolId.Value);

            if (statusId.HasValue)
                query = query.Where(v => (int)v.Status == statusId.Value);

            return await query.OrderByDescending(v => v.VisitDate).ToListAsync();
        }

        public async Task<IEnumerable<Visit>> GetPendingVerificationsAsync()
        {
            return await _dbSet
                .Include(v => v.School)
                .Include(v => v.Engineer)
                .Where(v => v.Status == VisitStatusEnum.PendingVerification)
                .OrderBy(v => v.VisitDate)
                .ToListAsync();
        }
    }
}
