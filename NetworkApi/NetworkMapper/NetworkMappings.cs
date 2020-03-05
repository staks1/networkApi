using AutoMapper;
using NetworkApi.Models;
using NetworkApi.Models.Dtos;

namespace NetworkApi.NetworkMapper
{
    public class NetworkMappings : Profile
    {
        public NetworkMappings()
        {
            CreateMap<NationalNetwork, NationalNetworkDto>().ReverseMap();
        }

    }
}
