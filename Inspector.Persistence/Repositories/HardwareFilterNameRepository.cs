using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Domains.Entities;
using Inspector.Persistence.DbSettings;
using Microsoft.EntityFrameworkCore;

namespace Inspector.Persistence.Repositories
{
    public class HardwareFilterNameRepository : BaseRepository<HardwareFilterNameDb>, IHardwareFilterNameRepository
    {
        public HardwareFilterNameRepository(DbContextOptions<RegistrationOIContext> dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}
