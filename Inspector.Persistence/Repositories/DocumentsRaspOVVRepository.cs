using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Domains.Entities;
using Inspector.Persistence.DbSettings;
using Microsoft.EntityFrameworkCore;

namespace Inspector.Persistence.Repositories
{
    public class DocumentsRaspOVVRepository : BaseRepository<DocumentsRaspOVVDb>, IDocumentsRaspOVVRepository
    {
        public DocumentsRaspOVVRepository(DbContextOptions<RegistrationOIContext> dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}
