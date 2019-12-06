using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Shared.Views;

namespace PetrpkuWeb.Server.Data
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            //Model to ViewModel
            CreateMap<AppUser, AppUserDutiesNotesPostsMilRequestsView>()
                .ForMember(dest => dest.BuildingView, opt => opt.MapFrom(src => src.Building))
                .ForMember(dest => dest.DepartmentView, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.DutiesView, opt => opt.MapFrom(src => src.Duties))
                .ForMember(dest => dest.NotesView, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.PostsView, opt => opt.MapFrom(src => src.Posts));



            CreateMap<Attachment, AttachmentView>();

            CreateMap<SiteSubsection, SiteSubSectionView>()
                .ForMember(dest => dest.SiteSectionView, opt => opt.MapFrom(src => src.SiteSection));

            CreateMap<MilRequest, MilRequestView>()
                .ForMember(dest => dest.AppUserView, opt => opt.MapFrom(src => src.AppUser))
                .ForMember(dest => dest.AttachmentsView, opt => opt.MapFrom(src => src.Attachments))
                .ForMember(dest => dest.ApprovedView, opt => opt.MapFrom(src => src.Approved))
                .ForMember(dest => dest.CheckedView, opt => opt.MapFrom(src => src.Checked))
                .ForMember(dest => dest.SentView, opt => opt.MapFrom(src => src.Sent))
                .ForMember(dest => dest.PublishedView, opt => opt.MapFrom(src => src.Published))
                .ForMember(dest => dest.SiteSectionView, opt => opt.MapFrom(src => src.SiteSection))
                .ForMember(dest => dest.SiteSubSectionView, opt => opt.MapFrom(src => src.SiteSubSection));

            CreateMap<IdentityRole, RoleView>();

            //Viewmodel to Model
            CreateMap<CssTypeView, CssType>();

            CreateMap<NoteAppUserCssTypeView, Note>()
                .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src.AppUserView))
                .ForMember(dest => dest.CssType, opt => opt.MapFrom(src => src.CssTypeView));

            CreateMap<SiteSubSectionView, SiteSubsection>()
                .ForMember(dest => dest.SiteSection, opt => opt.MapFrom(src => src.SiteSectionView));

            CreateMap<PostAppUserDepartmentAttachmentsView, Post>()
                .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src.AppUserView))
                .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.AttachmentsView))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.DepartmentView));

            CreateMap<DutyView, Duty>();

            //Mapping to both directions
            CreateMap<DepartmentAppUsersPostsView, Department>()
                .ForMember(dest => dest.AppUsers, opt => opt.MapFrom(src => src.AppUsersView))
                .ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src.PostsView))
                .ReverseMap();

            CreateMap<AppUser, AppUserView>()
                .ReverseMap();

            CreateMap<AppUser, AppUserDepartmentBuildingView>()
                .ForMember(dest => dest.BuildingView, opt => opt.MapFrom(src => src.Building))
                .ForMember(dest => dest.DepartmentView, opt => opt.MapFrom(src => src.Department))
                .ReverseMap();

            CreateMap<DepartmentView, Department>()
                .ReverseMap();

            CreateMap<BuildingView, Building>()
                .ReverseMap();


        }
    }
}
