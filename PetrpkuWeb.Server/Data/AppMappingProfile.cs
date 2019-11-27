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
            //Model to ViewModel
            CreateMap<AppUser, AppUserViewModel>()                
                .ForMember(dest => dest.Building, opt => opt.MapFrom(src => src.Building))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department));

            CreateMap<Attachment, AttachmentViewModel>();

            CreateMap<SiteSubsection, SiteSubSectionViewModel>()
                .ForMember(dest => dest.SiteSection, opt => opt.MapFrom(src => src.SiteSection));

            //Viewmodel to Model
            CreateMap<CssTypeViewModel, CssType>();

            CreateMap<ArticleViewModel, Article>();

            CreateMap<PostViewModel, Post>();

            CreateMap<MessageViewModel, Message>();

            CreateMap<SiteSubSectionViewModel, SiteSubsection>()
                .ForMember(dest => dest.SiteSection, opt => opt.MapFrom(src => src.SiteSection));
        }
    }
}
