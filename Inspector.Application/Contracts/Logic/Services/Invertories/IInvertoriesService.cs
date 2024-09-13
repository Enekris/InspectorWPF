using Inspector.Application.Contracts.Logic.Services.Invertories.Models;

namespace Inspector.Application.Contracts.Logic.Services.Invertories
{
    public interface IInvertoriesService
    {
        Task<InvertoriesDto> CreateAsync(InvertoriesDto baseEntity);
        Task<InvertoriesDto> UpdateAsync(InvertoriesDto baseEntity);
        Task DeleteAsync(InvertoriesDto baseEntity);
        Task<InvertoriesDto> GetAsync(int Id);
        Task SaveChangesAsync();
        Task<List<InvertoriesDto>> GetAll();

    }
}
