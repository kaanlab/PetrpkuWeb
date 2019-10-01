using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Client.Extensions
{
    public static  class JsGallery
    {
        public static async ValueTask<object> Show(this IJSRuntime js, string elementId)
        {
            return await js.InvokeAsync<ValueTask<object>>("gallery", elementId);
        }
    }
}
