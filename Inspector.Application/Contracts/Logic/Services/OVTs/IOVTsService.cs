using Inspector.Application.Contracts.Logic.Services.OVTs.Models;

namespace Inspector.Application.Contracts.Logic.Services.OVTs
{
    public interface IOVTsService
    {
        Task<OVTsDto> CreateAsync(OVTsDto baseEntity);
        Task<OVTsDto> UpdateAsync(OVTsDto baseEntity);
        Task DeleteAsync(OVTsDto baseEntity);
        Task<OVTsDto> GetAsync(int Id);
        Task SaveChangesAsync();
        Task<List<OVTsDto>> GetAll();

    }
}
