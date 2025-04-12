using AutoMapper;
using ToDoListApp.DTO;
using ToDoListApp.Models;

namespace ToDoListApp.Mapping
{
    public class EntityMapper:Profile
    {
        public EntityMapper()
        {

            //User Manangement
            CreateMap<AppUser, UserDTO>()
                .ForMember(dest=> dest.Items,opt=> opt.MapFrom(src=>src.Items));

            CreateMap<RegistrationRequest, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username));

            CreateMap<UpdateRequest, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

            //Items Manangement

            CreateMap<ToDoItems, ItemsDTO>()
            .ForMember(dest => dest.UserName, src => src.MapFrom(opt => opt.AppUser.UserName));

            CreateMap<CreateItemDTO, ToDoItems>();

            CreateMap<UpdateItemRequest, ToDoItems>();

        }
    }
}
