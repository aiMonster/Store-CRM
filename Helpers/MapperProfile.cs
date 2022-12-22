using System;
using AutoMapper;
using StoreCRM.DTOs;

namespace StoreCRM.Helpers
{
	public class MapperProfile : Profile
	{
        public MapperProfile()
        {
            CreateMap<Entities.Product, ProductDTO>();
        }
    }
}

