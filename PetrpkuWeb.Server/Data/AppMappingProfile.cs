using AutoMapper;
using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Shared.ViewModels;
using PetrpkuWeb.Shared.ViewModels.CatalogRegion;

namespace PetrpkuWeb.Server.Data
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            //Model to ViewModel
            CreateMap<AppUser, AppUserViewModel>()
                .ForMember(dest => dest.BuildingViewModel, opt => opt.MapFrom(src => src.Building))
                .ForMember(dest => dest.DepartmentViewModel, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.DaysOfDutyViewModel, opt => opt.MapFrom(src => src.DaysOfDuty))
                .ForMember(dest => dest.NotesViewModel, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.PostsViewModel, opt => opt.MapFrom(src => src.Posts));



            CreateMap<Attachment, AttachmentViewModel>();

            CreateMap<SiteSubsection, SiteSubSectionViewModel>()
                .ForMember(dest => dest.SiteSectionViewModel, opt => opt.MapFrom(src => src.SiteSection));

            CreateMap<MilRequest, MilRequestViewModel>()
                .ForMember(dest => dest.AppUserViewModel, opt => opt.MapFrom(src => src.AppUser))
                .ForMember(dest => dest.AttachmentsViewModel, opt => opt.MapFrom(src => src.Attachments))
                .ForMember(dest => dest.ApprovedViewModel, opt => opt.MapFrom(src => src.Approved))
                .ForMember(dest => dest.CheckedViewModel, opt => opt.MapFrom(src => src.Checked))
                .ForMember(dest => dest.SentViewModel, opt => opt.MapFrom(src => src.Sent))
                .ForMember(dest => dest.PublishedViewModel, opt => opt.MapFrom(src => src.Published))
                .ForMember(dest => dest.SiteSectionViewModel, opt => opt.MapFrom(src => src.SiteSection))
                .ForMember(dest => dest.SiteSubSectionViewModel, opt => opt.MapFrom(src => src.SiteSubSection));



            //Viewmodel to Model
            CreateMap<CssTypeViewModel, CssType>();

            CreateMap<NoteViewModel, Note>()
                .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src.AppUserViewModel))
                .ForMember(dest => dest.CssType, opt => opt.MapFrom(src => src.CssTypeViewModel));

            CreateMap<SiteSubSectionViewModel, SiteSubsection>()
                .ForMember(dest => dest.SiteSection, opt => opt.MapFrom(src => src.SiteSectionViewModel));

            CreateMap<PostViewModel, Post>()
                .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src.AppUserViewModel))
                .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.AttachmentsViewModel))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.DepartmentViewModel));

            CreateMap<DutyViewModel, Duty>();

            //Mapping to both directions
            CreateMap<DepartmentViewModel, Department>()
                .ForMember(dest => dest.AppUsers, opt => opt.MapFrom(src => src.AppUsersViewModel))
                .ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src.PostsViewModel))
                .ReverseMap();

            CreateMap<AppUser, AppUserView>()
                .ReverseMap();

            CreateMap<AppUser, UserManagerView>()
                .ForMember(dest => dest.BuildingViewModel, opt => opt.MapFrom(src => src.Building))
                .ForMember(dest => dest.DepartmentViewModel, opt => opt.MapFrom(src => src.Department))
                .ReverseMap();

            CreateMap<DepartmentView, Department>()
                .ReverseMap();

        }
    }
}
