using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetrpkuWeb.Shared.Models;
using PetrpkuWeb.Shared.ViewModels;

namespace PetrpkuWeb.Server.Data
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            //CreateMap<List<AttachmentViewModel>, List<Attachment>>();
            CreateMap<ArticleViewModel, Article>();
            //.ForMember(dest => dest.Attachments, act => act.MapFrom(src => src.Attachments));

        }
    }
}
