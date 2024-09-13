using AutoMapper;
using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Application.Contracts.Logic.Services.DocumentsRaspOVV;
using Inspector.Application.Contracts.Logic.Services.DocumentsRaspOVV.Models;
using Inspector.Domains.Entities;

namespace Inspector.Logic.Services
{
    public class DocumentsRaspOVVService : IDocumentsRaspOVVService
    {
        internal readonly IDocumentsRaspOVVRepository _documentsRaspOVVRepository;
        internal readonly IMapper _mapper;

        public DocumentsRaspOVVService(IDocumentsRaspOVVRepository documentsRaspOVVRepository, IMapper mapper)
        {
            _documentsRaspOVVRepository = documentsRaspOVVRepository;
            _mapper = mapper;
        }
        public async Task<DocumentsRaspOVVDto> CreateAsync(DocumentsRaspOVVDto docDto)
        {
            var docDb = _mapper.Map<DocumentsRaspOVVDb>(docDto);
            return _mapper.Map<DocumentsRaspOVVDto>(await _documentsRaspOVVRepository.CreateAsync(docDb));
        }

        public async Task DeleteAsync(DocumentsRaspOVVDto docDto)
        {
            await _documentsRaspOVVRepository.DeleteAsync(docDto.Id);
        }

        public async Task<List<DocumentsRaspOVVDto>> GetAll()
        {
            var list = _mapper.Map<List<DocumentsRaspOVVDto>>(await _documentsRaspOVVRepository.GetAll());
            return list;
        }

        public async Task<DocumentsRaspOVVDto> GetAsync(int Id)
        {
            return _mapper.Map<DocumentsRaspOVVDto>(await _documentsRaspOVVRepository.GetAsync(Id));
        }

        public async Task SaveChangesAsync()
        {
            await _documentsRaspOVVRepository.SaveChangesAsync();
        }

        public async Task<DocumentsRaspOVVDto> UpdateAsync(DocumentsRaspOVVDto cabDto)
        {
            var cabDb = await _documentsRaspOVVRepository.GetAsync(cabDto.Id);

            var dbProperties = typeof(DocumentsRaspOVVDb).GetProperties()
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            bool hasChanges = false;

            foreach (var property in typeof(DocumentsRaspOVVDto).GetProperties())
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

            return _mapper.Map<DocumentsRaspOVVDto>(await _documentsRaspOVVRepository.UpdateAsync(cabDb));
        }
    }
}
