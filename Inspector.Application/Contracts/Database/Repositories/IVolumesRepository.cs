using Inspector.Domains.Entities;

namespace Inspector.Application.Contracts.Database.Repositories
{
    public interface IVolumesRepository : IBaseRepository<VolumesDb>
    {
        Task<List<VolumesDb>> GetAllIncludeForVolumesTree();
    }
}
