using Inspector.Application.Contracts.Logic.Services.Sertificates.Models;

namespace Inspector.Application.Contracts.Logic.Services.Sertificates
{
    public interface ISertificatesService
    {
        Task<SertificatesDto> CreateAsync(SertificatesDto baseEntity);
        Task<SertificatesDto> UpdateAsync(SertificatesDto baseEntity);
        Task DeleteAsync(SertificatesDto baseEntity);
        Task<SertificatesDto> GetAsync(int Id);
        Task SaveChangesAsync();
        Task<List<SertificatesDto>> GetAll();

    }
}
