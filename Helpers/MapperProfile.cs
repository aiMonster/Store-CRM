using System;
using AutoMapper;
using StoreCRM.DTOs;

namespace StoreCRM.Helpers
{
	public class MapperProfile : Profile
	{
        public MapperProfile()
        {
            CreateMap<Entities.User, UserDTO>();
            CreateMap<Entities.Product, ProductDTO>();
            CreateMap<Entities.Category, CategoryInfoDTO>();
            CreateMap<Entities.Attachment, AttachmentDTO>();

            CreateMap<CreateProductDTO, Entities.Product>()
                .ForMember(x => x.Attachments, opt => opt.Ignore());
        }
    }
}

