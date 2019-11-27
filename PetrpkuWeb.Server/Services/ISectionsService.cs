using PetrpkuWeb.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public interface ISectionsService
    {
        Task<List<SiteSection>> GetSiteSections();
        Task<List<SiteSection>> GetSiteSectionsIncludeSubSections();
        Task<SiteSection> GetSiteSection(int siteSectionId);
        Task<bool> Create(SiteSection siteSection);
        Task<bool> Update(SiteSection siteSection);
        Task<bool> Delete(int siteSectionId);
        Task<List<SiteSubsection>> GetSubSectionsIncludeSections();
        Task<List<SiteSubsection>> GetSubsectionsForSection(int siteSectionId);
        Task<bool> CreateSubSections(SiteSubsection siteSubSection);
        Task<SiteSubsection> GetSiteSubSection(int siteSubSectionId);
        Task<bool> UpdateSubSection(SiteSubsection siteSubSection);
        Task<bool> DeleteSubSection(int siteSubSectionId);
    }
}
