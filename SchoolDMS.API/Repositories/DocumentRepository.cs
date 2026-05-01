using Microsoft.EntityFrameworkCore;
using SchoolDMS.API.Data;
using SchoolDMS.API.Models.Entities;
using SchoolDMS.API.Repositories.Interfaces;

namespace SchoolDMS.API.Repositories
{
    public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Document>> GetDocumentsByVisitIdAsync(int visitId)
        {
            return await _dbSet
                .Include(d => d.UploadedBy)
                .Where(d => d.VisitId == visitId)
                .ToListAsync();
        }
    }
}
