using AutoMapper;
using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Application.Contracts.Logic.Services.Sertificates;
using Inspector.Application.Contracts.Logic.Services.Sertificates.Models;
using Inspector.Domains.Entities;

namespace Inspector.Logic.Services
{
    public class SertificatesService : ISertificatesService
    {
        internal readonly ISertificatesRepository _sertificatesRepository;
        internal readonly IMapper _mapper;

        public SertificatesService(ISertificatesRepository sertificatesRepository, IMapper mapper)
        {
            _sertificatesRepository = sertificatesRepository;
            _mapper = mapper;
        }
        public async Task<SertificatesDto> CreateAsync(SertificatesDto serDto)
        {
            var serDb = _mapper.Map<SertificatesDb>(serDto);
            return _mapper.Map<SertificatesDto>(await _sertificatesRepository.CreateAsync(serDb));
        }

        public async Task DeleteAsync(SertificatesDto serDto)
        {
            await _sertificatesRepository.DeleteAsync(serDto.Id);
        }

        public async Task<List<SertificatesDto>> GetAll()
        {
            var list = _mapper.Map<List<SertificatesDto>>(await _sertificatesRepository.GetAll());
            return list;
        }

        public async Task<SertificatesDto> GetAsync(int Id)
        {
            return _mapper.Map<SertificatesDto>(await _sertificatesRepository.GetAsync(Id));
        }

        public async Task SaveChangesAsync()
        {
            await _sertificatesRepository.SaveChangesAsync();
        }

        public async Task<SertificatesDto> UpdateAsync(SertificatesDto cabDto)
        {
            var cabDb = await _sertificatesRepository.GetAsync(cabDto.Id);

            var dbProperties = typeof(SertificatesDb).GetProperties()
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            bool hasChanges = false;

            foreach (var property in typeof(SertificatesDto).GetProperties())
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

            return _mapper.Map<SertificatesDto>(await _sertificatesRepository.UpdateAsync(cabDb));
        }
    }
}
