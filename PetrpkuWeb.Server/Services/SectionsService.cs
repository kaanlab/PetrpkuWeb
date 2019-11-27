using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public class SectionsServices : ISectionsService
    {
        private readonly AppDbContext _db;
        public SectionsServices(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<SiteSection>> GetSiteSections()
        {
            return await _db.SiteSections.AsNoTracking().ToListAsync();
        }

        public async Task<List<SiteSection>> GetSiteSectionsIncludeSubSections()
        {
            return await _db.SiteSections
                .Include(ss => ss.SiteSubsections)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<SiteSection> GetSiteSection(int siteSectionId)
        {
           return await _db.SiteSections
                 .AsNoTracking()
                 .SingleOrDefaultAsync(u => u.SiteSectionId == siteSectionId);
        }

        public async Task<bool> Create(SiteSection siteSection)
        {
            _db.SiteSections.Add(siteSection);
            var created = await _db.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> Update(SiteSection siteSection)
        {
            _db.Attach(siteSection).State = EntityState.Modified;
            var updated = await _db.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> Delete(int siteSectionId)
        {
            var siteSection = await _db.SiteSections
                    .SingleOrDefaultAsync(s => s.SiteSectionId == siteSectionId);

            if (siteSection is null)
                return false;

            _db.SiteSections.Remove(siteSection);
            var deleted = await _db.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<List<SiteSubsection>> GetSubSectionsIncludeSections()
        {
            return await _db.SiteSubsections
                .Include(s => s.SiteSection)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<SiteSubsection>> GetSubsectionsForSection(int siteSectionId)
        {
            return await _db.SiteSubsections
                .Include(s => s.SiteSection)
                .Where(s => s.SiteSection.SiteSectionId == siteSectionId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> CreateSubSections(SiteSubsection siteSubSection)
        {
            _db.SiteSections.Update(siteSubSection.SiteSection);
            _db.SiteSubsections.Add(siteSubSection);
            var created = await _db.SaveChangesAsync();

            return created > 0;
        }

        public async Task<SiteSubsection> GetSiteSubSection(int siteSubSectionId)
        {
            return await _db.SiteSubsections
                 .AsNoTracking()
                 .SingleOrDefaultAsync(u => u.SiteSubsectionId == siteSubSectionId);
        }

        public async Task<bool> UpdateSubSection(SiteSubsection siteSubSection)
        {
            _db.Attach(siteSubSection).State = EntityState.Modified;
            var updated = await _db.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteSubSection(int siteSubSectionId)
        {
            var siteSubSection = await _db.SiteSubsections
                    .SingleOrDefaultAsync(s => s.SiteSubsectionId == siteSubSectionId);

            if (siteSubSection is null)
                return false;

            _db.SiteSubsections.Remove(siteSubSection);
            var deleted = await _db.SaveChangesAsync();

            return deleted > 0;
        }
    }
}
