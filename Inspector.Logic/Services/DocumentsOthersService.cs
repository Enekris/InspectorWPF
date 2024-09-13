using AutoMapper;
using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Application.Contracts.Logic.Services.DocumentsOthers;
using Inspector.Application.Contracts.Logic.Services.DocumentsOthers.Models;
using Inspector.Domains.Entities;

namespace Inspector.Logic.Services
{
    public class DocumentsOthersService : IDocumentsOthersService
    {
        internal readonly IDocumentsOthersRepository _documentsOthersRepository;
        internal readonly IMapper _mapper;

        public DocumentsOthersService(IDocumentsOthersRepository documentsOthersRepository, IMapper mapper)
        {
            _documentsOthersRepository = documentsOthersRepository;
            _mapper = mapper;
        }
        public async Task<DocumentsOthersDto> CreateAsync(DocumentsOthersDto docDto)
        {
            var docDb = _mapper.Map<DocumentsOthersDb>(docDto);
            return _mapper.Map<DocumentsOthersDto>(await _documentsOthersRepository.CreateAsync(docDb));
        }

        public async Task DeleteAsync(DocumentsOthersDto docDto)
        {
            await _documentsOthersRepository.DeleteAsync(docDto.Id);
        }

        public async Task<List<DocumentsOthersDto>> GetAll()
        {
            var list = _mapper.Map<List<DocumentsOthersDto>>(await _documentsOthersRepository.GetAll());
            return list;
        }

        public async Task<DocumentsOthersDto> GetAsync(int Id)
        {
            return _mapper.Map<DocumentsOthersDto>(await _documentsOthersRepository.GetAsync(Id));
        }

        public async Task SaveChangesAsync()
        {
            await _documentsOthersRepository.SaveChangesAsync();
        }

        public async Task<DocumentsOthersDto> UpdateAsync(DocumentsOthersDto cabDto)
        {
            var cabDb = await _documentsOthersRepository.GetAsync(cabDto.Id);

            var dbProperties = typeof(DocumentsOthersDb).GetProperties()
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            bool hasChanges = false;

            foreach (var property in typeof(DocumentsOthersDto).GetProperties())
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

            return _mapper.Map<DocumentsOthersDto>(await _documentsOthersRepository.UpdateAsync(cabDb));
        }
    }
}
