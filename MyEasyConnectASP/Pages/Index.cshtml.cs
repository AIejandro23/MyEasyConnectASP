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

namespace MyEasyConnectASP.Pages
{
    public class IndexModel : PageModel
    {
        public GetWorkerRS Worker { get; set; }
        public string Reminder { get; set; }
        public GetCorreosRS CorreosResponse { get; set; }

        public async Task OnGet()
        {
            string data = "{ WorkerId: '1'}";
            string contentType = "application/json";
            await GetWorker();
            await GetCorreos();
            Reminder = GetReminders("http://localhost:62114/GetReminders",data,contentType);           
        }

        public string GetReminders(string uri, string data, string contentType, string method = "POST")
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = contentType;
            request.Method = method;

            using (Stream requestBody = request.GetRequestStream())
            {
                requestBody.Write(dataBytes, 0, dataBytes.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
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
