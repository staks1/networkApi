using AutoMapper;
using NetworkApi.Models;
using NetworkApi.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkApi.NetworkMapper
{
    public class NetworkMappings:Profile
    {
        public NetworkMappings() {
            CreateMap<NationalNetwork, NationalNetworkDto>().ReverseMap();
        }

    }
}
