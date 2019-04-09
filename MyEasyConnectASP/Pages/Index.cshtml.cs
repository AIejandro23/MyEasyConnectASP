using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MyEasyConnectASP.Pages
{
    public class IndexModel : PageModel
    {
        public GetWorkerRS Worker { get; set; }
        public async Task OnGetAsync()
        {
            await GetWorker();
        }

        public async Task GetWorker()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62114");

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("WorkerId", "1")
                });

                var result = await client.PostAsync("/getWorker", content);

                string resultContent = await result.Content.ReadAsStringAsync();

                Worker = JsonConvert.DeserializeObject<GetWorkerRS>(resultContent);

            }
        }
    }
}
