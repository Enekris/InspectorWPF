using Inspector.Application.Contracts.Logic.Services.Volumes.Models;

namespace Inspector.Application.Contracts.Logic.Services.Volumes
{
    public interface IVolumesService
    {
        Task<VolumesDto> CreateAsync(VolumesDto baseEntity);
        Task<VolumesDto> UpdateAsync(VolumesDto baseEntity);
        Task DeleteAsync(VolumesDto baseEntity);
        Task<VolumesDto> GetAsync(int Id);
        Task SaveChangesAsync();
        Task<List<VolumesDto>> GetAll();
        Task<List<VolumesDto>> GetAllIncludeForVolumesTree();

    }
}
