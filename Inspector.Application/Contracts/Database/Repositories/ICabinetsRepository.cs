using Inspector.Domains.Entities;

namespace Inspector.Application.Contracts.Database.Repositories
{
    public interface ICabinetsRepository : IBaseRepository<CabinetsDb>
    {
        Task<List<CabinetsDb>> GetAllIncludeForCabinetTree();
    }
}
