using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public class CssTypeService : ICssTypeService
    {
        private readonly AppDbContext _db;
        public CssTypeService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(CssType cssType)
        {
            _db.CssTypes.Add(cssType);
            var created = await _db.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<CssType>> GetAll()
        {
            return await _db.CssTypes.AsNoTracking().ToListAsync();
        }

        public async Task<CssType> GetCssType(int cssTypeId)
        {
           return await _db.CssTypes.AsNoTracking().SingleOrDefaultAsync(ct => ct.CssTypeId == cssTypeId);
        }

        public async Task<bool> Update(CssType cssType)
        {
            _db.Attach(cssType).State = EntityState.Modified;
            var updated = await _db.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> Delete(int cssTypeId)
        {
            var cssType = await _db.CssTypes.SingleOrDefaultAsync(cs => cs.CssTypeId == cssTypeId);

            if (cssType is null)
                return false;

            _db.CssTypes.Remove(cssType);
            var deleted = await _db.SaveChangesAsync();

            return deleted > 0;
        }
    }
}
