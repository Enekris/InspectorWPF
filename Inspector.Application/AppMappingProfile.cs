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
using Inspector.Domains.Entities;

namespace Inspector.Application
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<CabinetsDb, CabinetsDto>()
            .ForMember(dest => dest.SertificateDto, opt => opt.MapFrom(src => src.SertificateDb))
            .ForMember(dest => dest.DocumentRaspOVVDto, opt => opt.MapFrom(src => src.DocumentRaspOVVDb))
            .ForMember(dest => dest.DocumentActReportDto, opt => opt.MapFrom(src => src.DocumentActReportDb))
            .ForMember(dest => dest.HardwaresDto, opt => opt.MapFrom(src => src.HardwaresDb))
            .ReverseMap();
            CreateMap<DocumentsActReportDb, DocumentsActReportDto>().ReverseMap();
            CreateMap<DocumentsOthersDb, DocumentsOthersDto>().ReverseMap();
            CreateMap<DocumentsThirdDb, DocumentsThirdDto>().ReverseMap();
            CreateMap<DocumentsRaspOVVDb, DocumentsRaspOVVDto>().ReverseMap();
            CreateMap<DocumentsFirstDb, DocumentsFirstDto>().ReverseMap();
            CreateMap<DocumentsSecondDb, DocumentsSecondDto>().ReverseMap();
            CreateMap<HardwareFilterNameDb, HardwareFilterNameDto>().ReverseMap();
            CreateMap<HardwaresDb, HardwaresDto>()
            .ForMember(dest => dest.FilterDto, opt => opt.MapFrom(src => src.FilterDb))
            .ForMember(dest => dest.OVTDto, opt => opt.MapFrom(src => src.OVTDb))
            .ForMember(dest => dest.DocumentFirstDto, opt => opt.MapFrom(src => src.DocumentFirstDb))
            .ForMember(dest => dest.DocumentSecondDto, opt => opt.MapFrom(src => src.DocumentSecondDb))
            .ForMember(dest => dest.documentThirdDto, opt => opt.MapFrom(src => src.DocumentThirdDb))
            .ForMember(dest => dest.CabinetDto, opt => opt.MapFrom(src => src.CabinetDb))
            .ReverseMap();
            CreateMap<InvertoriesDb, InvertoriesDto>().ReverseMap();
            CreateMap<OVTsDb, OVTsDto>().ReverseMap();
            CreateMap<SertificatesDb, SertificatesDto>().ReverseMap();
            CreateMap<VolumesDb, VolumesDto>()
                 .ForMember(dest => dest.DocumentRaspOVVDto, opt => opt.MapFrom(src => src.DocumentRaspOVVDb))
                 .ForMember(dest => dest.DocumentActReportDto, opt => opt.MapFrom(src => src.DocumentActReportDb))
                 .ForMember(dest => dest.documentThirdDto, opt => opt.MapFrom(src => src.DocumentThirdDb))
                 .ForMember(dest => dest.DocumentFirstDto, opt => opt.MapFrom(src => src.DocumentFirstDb))
                 .ForMember(dest => dest.DocumentSecondDto, opt => opt.MapFrom(src => src.DocumentSecondDb))
                 .ForMember(dest => dest.SertificatesDto, opt => opt.MapFrom(src => src.SertificatesDb))
                 .ForMember(dest => dest.DocumentsOthersDto, opt => opt.MapFrom(src => src.DocumentsOthersDb))
                .ReverseMap();
        }
    }
}
