using Inspector.Application.Contracts.Logic.Services.HardwareFilterName.Models;

namespace Inspector.Application.Contracts.Logic.Services.HardwareFilterName
{
    public interface IHardwareFilterNameService
    {
        Task<HardwareFilterNameDto> CreateAsync(HardwareFilterNameDto baseEntity);
        Task<HardwareFilterNameDto> UpdateAsync(HardwareFilterNameDto baseEntity);
        Task DeleteAsync(HardwareFilterNameDto baseEntity);
        Task<HardwareFilterNameDto> GetAsync(int Id);
        Task SaveChangesAsync();
        Task<List<HardwareFilterNameDto>> GetAll();

    }
}
