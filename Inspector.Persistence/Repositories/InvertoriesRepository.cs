using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Domains.Entities;
using Inspector.Persistence.DbSettings;
using Microsoft.EntityFrameworkCore;

namespace Inspector.Persistence.Repositories
{
    public class InvertoriesRepository : BaseRepository<InvertoriesDb>, IInvertoriesRepository
    {
        public InvertoriesRepository(DbContextOptions<RegistrationOIContext> dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}
