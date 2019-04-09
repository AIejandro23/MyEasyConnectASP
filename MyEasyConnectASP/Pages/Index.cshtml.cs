using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using MyEasyConnect.Models;

namespace MyEasyConnectASP.Pages
{
    public class IndexModel : PageModel
    {
        public GetWorkerRS Worker { get; set; }
        public GetCorreosRS CorreosResponse { get; set; }

        public GetReminderRS ReminderResponse { get; set; }
        public async Task OnGet()
        {
            await GetWorker();
            await GetCorreos();
            await GetReminders();           
        }

        public async Task GetReminders()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62114");

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("WorkerId", "1")
                });

                var result = await client.PostAsync("/getReminders", content);

                string resultContent = await result.Content.ReadAsStringAsync();

                ReminderResponse = JsonConvert.DeserializeObject<GetReminderRS>(resultContent);

            }
           
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

        public async Task GetCorreos()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62114");

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("WorkerId", "1")
                });

                var result = await client.PostAsync("/getCorreos", content);

                string resultContent = await result.Content.ReadAsStringAsync();

                CorreosResponse = JsonConvert.DeserializeObject<GetCorreosRS>(resultContent);

            }
        }
    }
}
