using AutoMapper;
using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Application.Contracts.Logic.Services.DocumentsFirst;
using Inspector.Application.Contracts.Logic.Services.DocumentsFirst.Models;
using Inspector.Domains.Entities;

namespace Inspector.Logic.Services
{
    public class DocumentsFirstService : IDocumentsFirstService
    {
        internal readonly IDocumentsFirstRepository _DocumentsFirstRepository;
        internal readonly IMapper _mapper;

        public DocumentsFirstService(IDocumentsFirstRepository DocumentsFirstRepository, IMapper mapper)
        {
            _DocumentsFirstRepository = DocumentsFirstRepository;
            _mapper = mapper;
        }
        public async Task<DocumentsFirstDto> CreateAsync(DocumentsFirstDto docDto)
        {
            var docDb = _mapper.Map<DocumentsFirstDb>(docDto);
            return _mapper.Map<DocumentsFirstDto>(await _DocumentsFirstRepository.CreateAsync(docDb));
        }

        public async Task DeleteAsync(DocumentsFirstDto docDto)
        {
            await _DocumentsFirstRepository.DeleteAsync(docDto.Id);
        }

        public async Task<List<DocumentsFirstDto>> GetAll()
        {
            var list = _mapper.Map<List<DocumentsFirstDto>>(await _DocumentsFirstRepository.GetAll());
            return list;
        }

        public async Task<DocumentsFirstDto> GetAsync(int Id)
        {
            return _mapper.Map<DocumentsFirstDto>(await _DocumentsFirstRepository.GetAsync(Id));
        }

        public async Task SaveChangesAsync()
        {
            await _DocumentsFirstRepository.SaveChangesAsync();
        }

        public async Task<DocumentsFirstDto> UpdateAsync(DocumentsFirstDto cabDto)
        {
            var cabDb = await _DocumentsFirstRepository.GetAsync(cabDto.Id);

            var dbProperties = typeof(DocumentsFirstDb).GetProperties()
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            bool hasChanges = false;

            foreach (var property in typeof(DocumentsFirstDto).GetProperties())
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

            return _mapper.Map<DocumentsFirstDto>(await _DocumentsFirstRepository.UpdateAsync(cabDb));
        }
    }
}
