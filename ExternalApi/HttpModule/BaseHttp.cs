using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExternalApi.HttpModule
{
    public static class BaseHttp
    {
        public static async Task<T> Post<T, TContent>(Dictionary<string, string> headers, string url, TContent content)
        {
            using (var client = new HttpClient())
            {
                if (headers != null)
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                var jsonContent = JsonConvert.SerializeObject(content);
                var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var responce = await client.PostAsync(url, stringContent);
                //responce.EnsureSuccessStatusCode();
                string responceString = await responce.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(responceString);
                return resultContent;
            }
        }

        public static async Task<T> Get<T>(Dictionary<string, string> headers, string url, string suffixUrl)
        {
            using (var client = new HttpClient())
            {
                if (headers != null)
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                var responce = await client.GetAsync(url + suffixUrl);
                string responceString = await responce.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(responceString);
                return resultContent;
            }
        }
        public static async Task<T> Get<T>(Dictionary<string, string> headers, string url)
        {
            using (var client = new HttpClient())
            {
                if (headers != null)
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                var responce = await client.GetAsync(url);
                string responceString = await responce.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(responceString);
                return resultContent;
            }
        }
    }
}
