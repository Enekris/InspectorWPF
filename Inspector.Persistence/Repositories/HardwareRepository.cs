using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Domains.Entities;
using Inspector.Persistence.DbSettings;
using Microsoft.EntityFrameworkCore;

namespace Inspector.Persistence.Repositories
{
    public class HardwareRepository : BaseRepository<HardwaresDb>, IHardwareRepository
    {
        public HardwareRepository(DbContextOptions<RegistrationOIContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        public async Task<List<HardwaresDb>> GetAllIncludeForCabinetTree()
        {
            return await _db.Include(c => c.CabinetDb)
                .Include(c => c.FilterDb).
                Include(c => c.OVTDb)
                .Include(c => c.DocumentFirstDb)
                .Include(c => c.DocumentSecondDb)
                .Include(c => c.DocumentThirdDb)
                                .ToListAsync();

        }
    }
}
