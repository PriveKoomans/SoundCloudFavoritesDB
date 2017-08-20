using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SoundCloud.Extensions
{
    public static class Extensions
    {
        public static string GetSource(this string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(url);

                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    return response.Result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
