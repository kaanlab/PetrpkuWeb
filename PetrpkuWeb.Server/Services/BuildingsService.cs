using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public class BuildingsService : ITypeService<Building>
    {
        private readonly AppDbContext _db;

        public BuildingsService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Building building)
        {
            _db.Buildings.Add(building);
            var created = await _db.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> Delete(int buildingId)
        {
            var building = await _db.Buildings.SingleOrDefaultAsync(b => b.BuildingId == buildingId);

            if (building is null)
                return false;

            _db.Buildings.Remove(building);
            var deleted = await _db.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<List<Building>> GetAll()
        {
            return await _db.Buildings.AsNoTracking().ToListAsync();
        }

        public async Task<Building> GetOne(int buildingId)
        {
            return await _db.Buildings.AsNoTracking().SingleOrDefaultAsync(b => b.BuildingId == buildingId);
        }

        public async Task<bool> Update(Building building)
        {
            _db.Attach(building).State = EntityState.Modified;
            var updated = await _db.SaveChangesAsync();
            return updated > 0;
        }
    }
}
