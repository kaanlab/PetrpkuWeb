using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _db;

        public PostService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Post post)
        {
            await _db.Posts.AddAsync(post);
            var created = await _db.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> Delete(int postId)
        {
            var post = await _db.Posts.SingleOrDefaultAsync(p => p.PostId == postId);

            if (post is null)
                return false;

            _db.Posts.Remove(post);
            var deleted = await _db.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<List<Post>> GetAll()
        {
            return await _db.Posts
                .Include(a => a.Attachments)
                .Include(a => a.AppUser)
                .Include(d => d.Department)
                .OrderByDescending(d => d.PublishDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Post> GetOne(int postId)
        {
            return await _db.Posts
                 .Include(a => a.Attachments)
                 .Include(a => a.AppUser)
                 .Include(d => d.Department)
                 .AsNoTracking()
                 .SingleOrDefaultAsync(p => p.PostId == postId);
        }

        public async Task<List<Post>> GetByDepartment(int departmentId)
        {
            return await _db.Posts
                .Include(a => a.Attachments)
                .Include(a => a.AppUser)
                .Include(d => d.Department)
                    .Where(d => d.Department.DepartmentId == departmentId)
                .OrderByDescending(d => d.PublishDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> Update(Post post)
        {
            _db.Attach(post).State = EntityState.Modified;
            var updated = await _db.SaveChangesAsync();
            return updated > 0;
        }
    }
}
