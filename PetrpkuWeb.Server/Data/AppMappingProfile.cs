using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Shared.ViewModels;

namespace PetrpkuWeb.Server.Data
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<ArticleViewModel, Article>();
            CreateMap<PostViewModel, Post>();
            CreateMap<MessageViewModel, Message>();


            //Model to ViewModel
            CreateMap<AppUser, AppUserViewModel>()                
                .ForMember(dest => dest.Building, opt => opt.MapFrom(src => src.Building))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department));

            //Viewmodel to Model
            CreateMap<CssTypeViewModel, CssType>();

        }
    }
}
