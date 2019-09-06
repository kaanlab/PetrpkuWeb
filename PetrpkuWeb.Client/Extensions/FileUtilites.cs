using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Client.Extensions
{
    public static class FileUtilites
    {
        public static async ValueTask<object> SaveAs(this IJSRuntime js, string filename, byte[] data)
        { 
            return await js.InvokeAsync<ValueTask<object>>(
                "saveAsFile",
                filename,
                Convert.ToBase64String(data));
        }
    }
}
