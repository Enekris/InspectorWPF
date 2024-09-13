using Inspector.Application.Contracts.Logic.Services.DocumentsRaspOVV.Models;

namespace Inspector.Application.Contracts.Logic.Services.DocumentsRaspOVV
{
    public interface IDocumentsRaspOVVService
    {
        Task<DocumentsRaspOVVDto> CreateAsync(DocumentsRaspOVVDto baseEntity);
        Task<DocumentsRaspOVVDto> UpdateAsync(DocumentsRaspOVVDto baseEntity);
        Task DeleteAsync(DocumentsRaspOVVDto baseEntity);
        Task<DocumentsRaspOVVDto> GetAsync(int Id);
        Task SaveChangesAsync();
        Task<List<DocumentsRaspOVVDto>> GetAll();

    }
}
