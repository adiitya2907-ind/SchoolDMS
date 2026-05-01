using SchoolDMS.API.Models.Entities;

namespace SchoolDMS.API.Repositories.Interfaces
{
    public interface IDocumentRepository : IRepository<Document>
    {
        Task<IEnumerable<Document>> GetDocumentsByVisitIdAsync(int visitId);
    }
}
