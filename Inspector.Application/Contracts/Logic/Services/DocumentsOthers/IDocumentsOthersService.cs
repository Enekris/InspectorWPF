using Inspector.Application.Contracts.Logic.Services.DocumentsOthers.Models;

namespace Inspector.Application.Contracts.Logic.Services.DocumentsOthers
{
    public interface IDocumentsOthersService
    {
        Task<DocumentsOthersDto> CreateAsync(DocumentsOthersDto baseEntity);
        Task<DocumentsOthersDto> UpdateAsync(DocumentsOthersDto baseEntity);
        Task DeleteAsync(DocumentsOthersDto baseEntity);
        Task<DocumentsOthersDto> GetAsync(int Id);
        Task SaveChangesAsync();
        Task<List<DocumentsOthersDto>> GetAll();

    }
}
