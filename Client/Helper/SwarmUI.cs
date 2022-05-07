using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SWARM.Client.Helper
{
    public class SwarmUI : ComponentBase
    {
        public JsonSerializerOptions options = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            PropertyNameCaseInsensitive = true
        };

        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        protected IJSRuntime Js { get; set; }

        [Inject]
        protected NavigationManager NavManager { get; set; }

        protected bool IsLoading { get; set; }
        protected int Total { get; set; } = 0;
        protected bool ExportAllPages { get; set; }

    }
}
