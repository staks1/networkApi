using AutoMapper;
using NetworkApi.Models;
using NetworkApi.Models.Dtos;

namespace NetworkApi.LineMapper
{
    public class LineMappings : Profile
    {
        public LineMappings()
        {
            CreateMap<Line, LineDto>().ReverseMap();
        }

    }
}
