using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.Cabinets.Models;
using Inspector.Application.Contracts.Logic.Services.DocumentsActReport.Models;
using Inspector.Application.Contracts.Logic.Services.DocumentsFirst.Models;
using Inspector.Application.Contracts.Logic.Services.DocumentsOthers.Models;
using Inspector.Application.Contracts.Logic.Services.DocumentsRaspOVV.Models;
using Inspector.Application.Contracts.Logic.Services.DocumentsSecond.Models;
using Inspector.Application.Contracts.Logic.Services.DocumentsThird.Models;
using Inspector.Application.Contracts.Logic.Services.HardwareFilterName.Models;
using Inspector.Application.Contracts.Logic.Services.Hardwares.Models;
using Inspector.Application.Contracts.Logic.Services.Invertories.Models;
using Inspector.Application.Contracts.Logic.Services.OVTs.Models;
using Inspector.Application.Contracts.Logic.Services.Sertificates.Models;
using Inspector.Application.Contracts.Logic.Services.Volumes.Models;
using Inspector.Models;

namespace Inspector.Config
{
    public class WpfMappingProfile : Profile
    {
        public WpfMappingProfile()
        {
            CreateMap<CabinetsWpf, CabinetsDto>()
            .ForMember(dest => dest.SertificateDto, opt => opt.MapFrom(src => src.SertificateWpf))
            .ForMember(dest => dest.DocumentRaspOVVDto, opt => opt.MapFrom(src => src.DocumentRaspOVVWpf))
            .ForMember(dest => dest.DocumentActReportDto, opt => opt.MapFrom(src => src.DocumentActReportWpf))
            .ForMember(dest => dest.HardwaresDto, opt => opt.MapFrom(src => src.HardwaresWpf))
            .ReverseMap();
            CreateMap<DocumentsActReportWpf, DocumentsActReportDto>().ReverseMap();
            CreateMap<DocumentsOthersWpf, DocumentsOthersDto>().ReverseMap();
            CreateMap<DocumentsThirdWpf, DocumentsThirdDto>().ReverseMap();
            CreateMap<DocumentsRaspOVVWpf, DocumentsRaspOVVDto>().ReverseMap();
            CreateMap<DocumentsFirstWpf, DocumentsFirstDto>().ReverseMap();
            CreateMap<DocumentsSecondWpf, DocumentsSecondDto>().ReverseMap();
            CreateMap<HardwareFilterNameWpf, HardwareFilterNameDto>().ReverseMap();
            CreateMap<HardwaresWpf, HardwaresDto>()
            .ForMember(dest => dest.FilterDto, opt => opt.MapFrom(src => src.FilterWpf))
            .ForMember(dest => dest.OVTDto, opt => opt.MapFrom(src => src.OVTWpf))
            .ForMember(dest => dest.DocumentFirstDto, opt => opt.MapFrom(src => src.DocumentFirstWpf))
            .ForMember(dest => dest.DocumentSecondDto, opt => opt.MapFrom(src => src.DocumentSecondWpf))
            .ForMember(dest => dest.documentThirdDto, opt => opt.MapFrom(src => src.documentThirdWpf))
            .ForMember(dest => dest.CabinetDto, opt => opt.MapFrom(src => src.CabinetWpf))
            .ReverseMap();
            CreateMap<InvertoriesWpf, InvertoriesDto>().ReverseMap();
            CreateMap<OVTsWpf, OVTsDto>().ReverseMap();
            CreateMap<SertificatesWpf, SertificatesDto>().ReverseMap();
            CreateMap<VolumesWpf, VolumesDto>()
               .ForMember(dest => dest.DocumentRaspOVVDto, opt => opt.MapFrom(src => src.DocumentRaspOVVWpf))
               .ForMember(dest => dest.DocumentActReportDto, opt => opt.MapFrom(src => src.DocumentActReportWpf))
               .ForMember(dest => dest.documentThirdDto, opt => opt.MapFrom(src => src.documentThirdWpf))
               .ForMember(dest => dest.DocumentFirstDto, opt => opt.MapFrom(src => src.DocumentFirstWpf))
               .ForMember(dest => dest.DocumentSecondDto, opt => opt.MapFrom(src => src.DocumentSecondWpf))
               .ForMember(dest => dest.SertificatesDto, opt => opt.MapFrom(src => src.SertificatesWpf))
               .ForMember(dest => dest.DocumentsOthersDto, opt => opt.MapFrom(src => src.DocumentsOthersWpf))
            .ReverseMap();
        }
    }
}
