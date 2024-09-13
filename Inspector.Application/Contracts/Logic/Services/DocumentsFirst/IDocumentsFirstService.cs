using Inspector.Application.Contracts.Logic.Services.DocumentsFirst.Models;

namespace Inspector.Application.Contracts.Logic.Services.DocumentsFirst
{
    public interface IDocumentsFirstService
    {
        Task<DocumentsFirstDto> CreateAsync(DocumentsFirstDto baseEntity);
        Task<DocumentsFirstDto> UpdateAsync(DocumentsFirstDto baseEntity);
        Task DeleteAsync(DocumentsFirstDto baseEntity);
        Task<DocumentsFirstDto> GetAsync(int Id);
        Task SaveChangesAsync();
        Task<List<DocumentsFirstDto>> GetAll();

    }
}
