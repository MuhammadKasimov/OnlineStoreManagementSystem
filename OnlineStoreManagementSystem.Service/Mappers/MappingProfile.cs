using AutoMapper;
using OnlineStoreManagementSystem.Domain.Entitties.Attachments;
using OnlineStoreManagementSystem.Domain.Entitties.Carts;
using OnlineStoreManagementSystem.Domain.Entitties.Products;
using OnlineStoreManagementSystem.Domain.Entitties.Users;
using OnlineStoreManagementSystem.Service.DTOs.Attachments;
using OnlineStoreManagementSystem.Service.DTOs.Carts;
using OnlineStoreManagementSystem.Service.DTOs.Products;
using OnlineStoreManagementSystem.Service.DTOs.Users;

namespace OnlineStoreManagementSystem.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // users
            CreateMap<User, UserForCreationDTO>().ReverseMap();
            CreateMap<User, UserForUpdateDTO>().ReverseMap();
            CreateMap<User, UserForViewDTO>().ReverseMap();
            CreateMap<UserProduct, UserProductForCreationDTO>().ReverseMap();

            // products
            CreateMap<Product, ProductForCreationDTO>().ReverseMap();

            // carts
            CreateMap<Cart, CartForCreationDTO>();

            // attachmnets
            CreateMap<Attachment, AttachmentForCreationDTO>();
        }
    }
}
