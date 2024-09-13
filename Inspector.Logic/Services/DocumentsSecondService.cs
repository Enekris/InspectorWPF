using AutoMapper;
using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Application.Contracts.Logic.Services.DocumentsSecond;
using Inspector.Application.Contracts.Logic.Services.DocumentsSecond.Models;
using Inspector.Domains.Entities;

namespace Inspector.Logic.Services
{
    public class DocumentsSecondService : IDocumentsSecondService
    {
        internal readonly IDocumentsSecondRepository _DocumentsSecondRepository;
        internal readonly IMapper _mapper;

        public DocumentsSecondService(IDocumentsSecondRepository DocumentsSecondRepository, IMapper mapper)
        {
            _DocumentsSecondRepository = DocumentsSecondRepository;
            _mapper = mapper;
        }
        public async Task<DocumentsSecondDto> CreateAsync(DocumentsSecondDto docDto)
        {
            var docDb = _mapper.Map<DocumentsSecondDb>(docDto);
            return _mapper.Map<DocumentsSecondDto>(await _DocumentsSecondRepository.CreateAsync(docDb));
        }

        public async Task DeleteAsync(DocumentsSecondDto docDto)
        {
            await _DocumentsSecondRepository.DeleteAsync(docDto.Id);
        }

        public async Task<List<DocumentsSecondDto>> GetAll()
        {
            var list = _mapper.Map<List<DocumentsSecondDto>>(await _DocumentsSecondRepository.GetAll());
            return list;
        }

        public async Task<DocumentsSecondDto> GetAsync(int Id)
        {
            return _mapper.Map<DocumentsSecondDto>(await _DocumentsSecondRepository.GetAsync(Id));
        }

        public async Task SaveChangesAsync()
        {
            await _DocumentsSecondRepository.SaveChangesAsync();
        }

        public async Task<DocumentsSecondDto> UpdateAsync(DocumentsSecondDto cabDto)
        {
            var cabDb = await _DocumentsSecondRepository.GetAsync(cabDto.Id);

            var dbProperties = typeof(DocumentsSecondDb).GetProperties()
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            bool hasChanges = false;

            foreach (var property in typeof(DocumentsSecondDto).GetProperties())
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

            return _mapper.Map<DocumentsSecondDto>(await _DocumentsSecondRepository.UpdateAsync(cabDb));
        }
    }
}
