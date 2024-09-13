using AutoMapper;
using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Application.Contracts.Logic.Services.DocumentsActReport;
using Inspector.Application.Contracts.Logic.Services.DocumentsActReport.Models;
using Inspector.Domains.Entities;

namespace Inspector.Logic.Services
{
    public class DocumentsActReportService : IDocumentsActReportService
    {
        internal readonly IDocumentsActReportRepository _documentsActReportRepository;
        internal readonly IMapper _mapper;

        public DocumentsActReportService(IDocumentsActReportRepository documentsActReportRepository, IMapper mapper)
        {
            _documentsActReportRepository = documentsActReportRepository;
            _mapper = mapper;
        }
        public async Task<DocumentsActReportDto> CreateAsync(DocumentsActReportDto docDto)
        {
            var docDb = _mapper.Map<DocumentsActReportDb>(docDto);
            return _mapper.Map<DocumentsActReportDto>(await _documentsActReportRepository.CreateAsync(docDb));
        }

        public async Task DeleteAsync(DocumentsActReportDto docDto)
        {
            await _documentsActReportRepository.DeleteAsync(docDto.Id);
        }

        public async Task<List<DocumentsActReportDto>> GetAll()
        {
            var list = _mapper.Map<List<DocumentsActReportDto>>(await _documentsActReportRepository.GetAll());
            return list;
        }

        public async Task<DocumentsActReportDto> GetAsync(int Id)
        {
            return _mapper.Map<DocumentsActReportDto>(await _documentsActReportRepository.GetAsync(Id));
        }

        public async Task SaveChangesAsync()
        {
            await _documentsActReportRepository.SaveChangesAsync();
        }

        public async Task<DocumentsActReportDto> UpdateAsync(DocumentsActReportDto cabDto)
        {
            var cabDb = await _documentsActReportRepository.GetAsync(cabDto.Id);

            var dbProperties = typeof(DocumentsActReportDb).GetProperties()
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            bool hasChanges = false;

            foreach (var property in typeof(DocumentsActReportDto).GetProperties())
            {
                if (property.Name == "Id")
                {
                    continue;
                }

                var newValue = property.GetValue(cabDto);

                if (newValue is string str)
                {
                    newValue = string.IsNullOrEmpty(str) ? null : newValue;
                }
                else if (newValue is int integer)
                {
                    newValue = integer == 0 ? null : newValue;
                }

                if (dbProperties.TryGetValue(property.Name, out var dbProperty))
                {
                    var currentValue = dbProperty.GetValue(cabDb);

                    if (!Equals(newValue, currentValue))
                    {
                        hasChanges = true;
                        dbProperty.SetValue(cabDb, newValue);
                    }
                }
            }

            if (!hasChanges)
            {
                return cabDto;
            }

            return _mapper.Map<DocumentsActReportDto>(await _documentsActReportRepository.UpdateAsync(cabDb));
        }
    }
}
