using AutoMapper;
using InternetStore.BLL.DataTransferObjects;
using InternetStore.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetStore.WEB.App_Start
{
    public class AutoMapperWEBConfig
    {
        public static IMapper Mapper;
        public static void RegisterMappings()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LoginModel, LoginDTO>();
                cfg.CreateMap<OrderItem, UpdateOrderItemDTO>();
                //cfg.CreateMap<RegistrationModel, RegistrationDTO>();
                //cfg.CreateMap<DialogMessageModel, DialogMessageDTO>();
                //cfg.CreateMap<ItemDTO, ItemInTheRequest>().ForMember("ItemId", opt => opt.MapFrom(c => c.Id));
            }).CreateMapper();
        }
    }
}