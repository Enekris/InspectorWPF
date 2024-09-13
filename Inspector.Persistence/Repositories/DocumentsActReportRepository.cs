using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Domains.Entities;
using Inspector.Persistence.DbSettings;
using Microsoft.EntityFrameworkCore;

namespace Inspector.Persistence.Repositories
{
    public class DocumentsActReportRepository : BaseRepository<DocumentsActReportDb>, IDocumentsActReportRepository
    {
        public DocumentsActReportRepository(DbContextOptions<RegistrationOIContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public async Task<List<DocumentsActReportDb>> GetDocumentsForVolumeAsync(int volumeId)
        {
            return await _db
                .AsNoTracking()
                .Where(d => d.VolumeId == volumeId)
                .ToListAsync();
        }
    }
}
