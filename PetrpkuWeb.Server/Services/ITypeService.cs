using PetrpkuWeb.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public interface ITypeService<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetOne(int Id);
        Task<bool> Create(T Type);
        Task<bool> Update(T Type);
        Task<bool> Delete(int Id);
    }
}
