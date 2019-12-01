using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public class DepartmentsService : ITypeService<Department>
    {
        private readonly AppDbContext _db;

        public DepartmentsService(AppDbContext db)
        {
            _db = db;
        }
        
        public async Task<bool> Create(Department department)
        {
            _db.Departments.Add(department);
            var created = await _db.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> Delete(int departmentId)
        {
            var department = await _db.Departments.SingleOrDefaultAsync(d => d.DepartmentId == departmentId);

            if (department is null)
                return false;

            _db.Departments.Remove(department);
            var deleted = await _db.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<List<Department>> GetAll()
        {
            return await _db.Departments.AsNoTracking().ToListAsync();
        }

        public async Task<Department> GetOne(int departmentId)
        {
            return await _db.Departments.AsNoTracking().SingleOrDefaultAsync(d => d.DepartmentId == departmentId);
        }

        public async Task<bool> Update(Department department)
        {
            //_db.Attach(department).State = EntityState.Modified;
            _db.Update(department);
            var updated = await _db.SaveChangesAsync();
            return updated > 0;
        }
    }
}
