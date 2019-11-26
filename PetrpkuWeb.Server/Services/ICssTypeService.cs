using PetrpkuWeb.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public interface ICssTypeService
    {
        Task<List<CssType>> GetAll();
        Task<CssType> GetCssType(int cssTypeId);
        Task<bool> Create(CssType cssType);
        Task<bool> Update(CssType cssType);
        Task<bool> Delete(int cssTypeId);
    }
}
