using Inspector.Application.Contracts.Logic.Services.Hardwares.Models;

namespace Inspector.Application.Contracts.Logic.Services.Hardwares
{
    public interface IHardwaresService
    {
        Task<HardwaresDto> CreateAsync(HardwaresDto baseEntity);
        Task<HardwaresDto> UpdateAsync(HardwaresDto baseEntity);
        Task DeleteAsync(HardwaresDto baseEntity);
        Task<HardwaresDto> GetAsync(int Id);
        Task SaveChangesAsync();
        Task<List<HardwaresDto>> GetAll();
        Task<List<HardwaresDto>> GetAllIncludeForCabinetTree();
    }
}
