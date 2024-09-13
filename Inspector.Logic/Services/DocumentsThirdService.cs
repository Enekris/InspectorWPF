using AutoMapper;
using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Application.Contracts.Logic.Services.DocumentsThird;
using Inspector.Application.Contracts.Logic.Services.DocumentsThird.Models;
using Inspector.Domains.Entities;

namespace Inspector.Logic.Services
{
    public class DocumentsThirdService : IDocumentsThirdService
    {
        internal readonly IDocumentsThirdRepository _DocumentsThirdRepository;
        internal readonly IMapper _mapper;

        public DocumentsThirdService(IDocumentsThirdRepository DocumentsThirdRepository, IMapper mapper)
        {
            _DocumentsThirdRepository = DocumentsThirdRepository;
            _mapper = mapper;
        }
        public async Task<DocumentsThirdDto> CreateAsync(DocumentsThirdDto docDto)
        {
            var docDb = _mapper.Map<DocumentsThirdDb>(docDto);
            return _mapper.Map<DocumentsThirdDto>(await _DocumentsThirdRepository.CreateAsync(docDb));
        }

        public async Task DeleteAsync(DocumentsThirdDto docDto)
        {
            await _DocumentsThirdRepository.DeleteAsync(docDto.Id);
        }

        public async Task<List<DocumentsThirdDto>> GetAll()
        {
            var list = _mapper.Map<List<DocumentsThirdDto>>(await _DocumentsThirdRepository.GetAll());
            return list;
        }

        public async Task<DocumentsThirdDto> GetAsync(int Id)
        {
            return _mapper.Map<DocumentsThirdDto>(await _DocumentsThirdRepository.GetAsync(Id));
        }

        public async Task SaveChangesAsync()
        {
            await _DocumentsThirdRepository.SaveChangesAsync();
        }

        public async Task<DocumentsThirdDto> UpdateAsync(DocumentsThirdDto cabDto)
        {
            var cabDb = await _DocumentsThirdRepository.GetAsync(cabDto.Id);

            var dbProperties = typeof(DocumentsThirdDb).GetProperties()
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            bool hasChanges = false;

            foreach (var property in typeof(DocumentsThirdDto).GetProperties())
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

            return _mapper.Map<DocumentsThirdDto>(await _DocumentsThirdRepository.UpdateAsync(cabDb));
        }
    }
}
