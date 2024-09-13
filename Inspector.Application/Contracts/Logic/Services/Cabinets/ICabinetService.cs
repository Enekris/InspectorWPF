using Inspector.Application.Contracts.Logic.Services.Cabinets.Models;

namespace Inspector.Application.Contracts.Logic.Services.Cabinets
{
    public interface ICabinetService
    {
        Task<CabinetsDto> CreateAsync(CabinetsDto baseEntity);
        Task<CabinetsDto> UpdateAsync(CabinetsDto baseEntity);
        Task DeleteAsync(CabinetsDto baseEntity);
        Task<CabinetsDto> GetAsync(int Id);
        Task SaveChangesAsync();
        Task<List<CabinetsDto>> GetAll();
        Task<List<CabinetsDto>> GetAllIncludeForCabinetTree();
    }
}
