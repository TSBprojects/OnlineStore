using AutoMapper;
using Internet_store.DAL.Entities;
using InternetStore.BLL.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.BLL.Infrastructure
{
    public class AutoMapperBLLConfig
    {
        public static IMapper Mapper;
        public static void RegisterMappings()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>().MaxDepth(1);
                cfg.CreateMap<ProductImage, ProductImageDTO>().MaxDepth(1);
                cfg.CreateMap<Category, CategoryDTO>().MaxDepth(1);
                cfg.CreateMap<Tag, TagDTO>();
                cfg.CreateMap<User, UserDTO>().MaxDepth(1);
                cfg.CreateMap<TagItem, TagItemDTO>().MaxDepth(1);
                cfg.CreateMap<Order, OrderDTO>();
                cfg.CreateMap<OrderItem, OrderItemDTO>();
                cfg.CreateMap<ProductReview, ProductReviewDTO>().MaxDepth(1); 
                cfg.CreateMap<Role, RoleDTO>();
                cfg.CreateMap<Subscriber, SubscriberDTO>();
                cfg.CreateMap<ProductVote, ProductVoteDTO>();
            }).CreateMapper();
        }
    }
}
