using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Domains.Entities;
using Inspector.Persistence.DbSettings;
using Microsoft.EntityFrameworkCore;

namespace Inspector.Persistence.Repositories
{
    public class VolumesRepository : BaseRepository<VolumesDb>, IVolumesRepository
    {
        public VolumesRepository(DbContextOptions<RegistrationOIContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public async Task<List<VolumesDb>> GetAllIncludeForVolumesTree()
        {

            var volumes2 = _db
               .Include(c => c.DocumentRaspOVVDb)
                  .ThenInclude(s => s.OVTsDb)
               .Include(c => c.DocumentRaspOVVDb)
                  .ThenInclude(s => s.CabinetsDb)
               .Include(c => c.DocumentRaspOVVDb)
                  .ThenInclude(s => s.InventoryDb)

               .Include(c => c.DocumentActReportDb)
                  .ThenInclude(s => s.CabinetsDb)
               .Include(c => c.DocumentActReportDb)
                  .ThenInclude(s => s.InventoryDb)

               .Include(c => c.DocumentThirdDb)
                 .ThenInclude(s => s.HardwaresDb)
                .Include(c => c.DocumentThirdDb)
                  .ThenInclude(s => s.InventoryDb)

               .Include(c => c.DocumentFirstDb)
                  .ThenInclude(s => s.HardwaresDb)
               .Include(c => c.DocumentFirstDb)
                  .ThenInclude(s => s.InventoryDb)

               .Include(c => c.DocumentSecondDb)
                  .ThenInclude(s => s.HardwaresDb)
               .Include(c => c.DocumentSecondDb)
                  .ThenInclude(s => s.InventoryDb)

               .Include(c => c.SertificatesDb)
                  .ThenInclude(s => s.CabinetsDb)
               .Include(c => c.SertificatesDb)
                  .ThenInclude(s => s.OVTDb)
               .Include(c => c.SertificatesDb)
                  .ThenInclude(s => s.InventoryDb)

               .Include(c => c.DocumentsOthersDb)
                 .ThenInclude(s => s.InventoryDb)

            .AsSplitQuery();

            var query = volumes2.ToQueryString();
            return await volumes2.ToListAsync();


        }

    }
}

