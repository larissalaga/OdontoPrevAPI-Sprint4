using AutoMapper;
using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Models;

namespace OdontoPrevAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamento de entidades para Dtos
            CreateMap<AnaliseRaioX, AnaliseRaioXDtos>();
            CreateMap<AnaliseRaioXDtos, AnaliseRaioX>();
            CreateMap<CheckIn, CheckInDtos>();
            CreateMap<CheckInDtos, CheckIn>();
            CreateMap<Dentista, DentistaDtos>();
            CreateMap<DentistaDtos, Dentista>();
            CreateMap<ExtratoPontos, ExtratoPontosDtos>();
            CreateMap<ExtratoPontosDtos, ExtratoPontos>();
            CreateMap<Paciente, PacienteDtos>();
            CreateMap<PacienteDtos, Paciente>();
            CreateMap<PacienteDentista, PacienteDentistaDtos>();
            CreateMap<PacienteDentistaDtos, PacienteDentista>();
            CreateMap<Perguntas, PerguntasDtos>();
            CreateMap<PerguntasDtos, Perguntas>();
            CreateMap<Plano, PlanoDtos>();
            CreateMap<PlanoDtos, Plano>();
            CreateMap<RaioX, RaioXDtos>();
            CreateMap<RaioXDtos, RaioX>();
            CreateMap<Respostas, RespostasDtos>();
            CreateMap<RespostasDtos, Respostas>();
        }
    }
}
