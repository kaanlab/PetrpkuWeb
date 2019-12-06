using AutoMapper;
using PetrpkuWeb.Shared.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Client
{
    public class WebClientMappingProfile : Profile
    {
        public WebClientMappingProfile()
        {
            CreateMap<AppUserDepartmentBuildingView, AppUserRoleView>().ReverseMap();
        }
    }
}
