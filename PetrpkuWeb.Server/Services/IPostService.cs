using PetrpkuWeb.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public interface IPostService
    {
        Task<bool> Create(Post post);
        Task<bool> Delete(int postId);
        Task<List<Post>> GetAll();
        Task<Post> GetOne(int postId);
        Task<bool> Update(Post department);
        Task<List<Post>> GetByDepartment(int departmentId);
    }
}
