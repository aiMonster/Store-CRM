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
            CreateMap<Entities.StoreTask, TaskDTO>();
            CreateMap<Entities.Stock, StockDTO>();
            CreateMap<Entities.Posting, PostingDTO>();
            CreateMap<Entities.PostingProduct, PostingItemDTO>();

            CreateMap<CreateTaskDTO, Entities.StoreTask>();
            CreateMap<CreateStockDTO, Entities.Stock>();
            CreateMap<PostingNewItemDTO, Entities.PostingProduct>();
            CreateMap<CreateProductDTO, Entities.Product>()
                .ForMember(x => x.Attachments, opt => opt.Ignore());
        }
    }
}

