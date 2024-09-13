using Inspector.Application.Contracts.Logic.Services.DocumentsThird.Models;

namespace Inspector.Application.Contracts.Logic.Services.DocumentsThird
{
    public interface IDocumentsThirdService
    {
        Task<DocumentsThirdDto> CreateAsync(DocumentsThirdDto baseEntity);
        Task<DocumentsThirdDto> UpdateAsync(DocumentsThirdDto baseEntity);
        Task DeleteAsync(DocumentsThirdDto baseEntity);
        Task<DocumentsThirdDto> GetAsync(int Id);
        Task SaveChangesAsync();
        Task<List<DocumentsThirdDto>> GetAll();

    }
}
