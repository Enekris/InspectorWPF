using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Domains.Entities;
using Inspector.Persistence.DbSettings;
using Microsoft.EntityFrameworkCore;

namespace Inspector.Persistence.Repositories
{
    public class CabinetsRepository : BaseRepository<CabinetsDb>, ICabinetsRepository
    {
        public CabinetsRepository(DbContextOptions<RegistrationOIContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public async Task<List<CabinetsDb>> GetAllIncludeForCabinetTree()
        {
            var cabinets = _db.Include(c => c.HardwaresDb)
                 .ThenInclude(h => h.FilterDb)
                  .Include(c => c.SertificateDb)
                  .Include(c => c.DocumentRaspOVVDb)
                    .Include(c => c.DocumentActReportDb)
                     .AsSplitQuery();
            return await cabinets.ToListAsync();

        }
    }
}
