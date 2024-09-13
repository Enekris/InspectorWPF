using Inspector.Application.Contracts.Logic.Services.DocumentsSecond.Models;

namespace Inspector.Application.Contracts.Logic.Services.DocumentsSecond
{
    public interface IDocumentsSecondService
    {
        Task<DocumentsSecondDto> CreateAsync(DocumentsSecondDto baseEntity);
        Task<DocumentsSecondDto> UpdateAsync(DocumentsSecondDto baseEntity);
        Task DeleteAsync(DocumentsSecondDto baseEntity);
        Task<DocumentsSecondDto> GetAsync(int Id);
        Task SaveChangesAsync();
        Task<List<DocumentsSecondDto>> GetAll();

    }
}
