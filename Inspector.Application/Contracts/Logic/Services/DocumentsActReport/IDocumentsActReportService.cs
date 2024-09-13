using Inspector.Application.Contracts.Logic.Services.DocumentsActReport.Models;

namespace Inspector.Application.Contracts.Logic.Services.DocumentsActReport
{
    public interface IDocumentsActReportService
    {
        Task<DocumentsActReportDto> CreateAsync(DocumentsActReportDto baseEntity);
        Task<DocumentsActReportDto> UpdateAsync(DocumentsActReportDto baseEntity);
        Task DeleteAsync(DocumentsActReportDto baseEntity);
        Task<DocumentsActReportDto> GetAsync(int Id);
        Task SaveChangesAsync();
        Task<List<DocumentsActReportDto>> GetAll();
    }
}
