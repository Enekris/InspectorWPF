using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Domains.Entities;
using Inspector.Persistence.DbSettings;
using Microsoft.EntityFrameworkCore;

namespace Inspector.Persistence.Repositories
{
    public class DocumentsFirstRepository : BaseRepository<DocumentsFirstDb>, IDocumentsFirstRepository
    {
        public DocumentsFirstRepository(DbContextOptions<RegistrationOIContext> dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}
