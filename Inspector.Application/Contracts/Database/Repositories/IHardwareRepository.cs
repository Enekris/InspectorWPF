using Inspector.Domains.Entities;

namespace Inspector.Application.Contracts.Database.Repositories
{
    public interface IHardwareRepository : IBaseRepository<HardwaresDb>
    {
        Task<List<HardwaresDb>> GetAllIncludeForCabinetTree();
    }
}
