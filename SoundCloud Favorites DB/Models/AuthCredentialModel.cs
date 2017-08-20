namespace SoundCloud.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;

    public class AuthCredentialModel
    {
        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Password")]
        public byte[] Password { get; set; }

        [JsonProperty("AccessToken")]
        public string AccessToken { get; set; }

        public void RequestAccessToken(string clientId, string clientSecret, string scope)
        {
            Task.Run(() => GetToken(clientId, clientSecret, scope));
        }

        internal void GetToken(string clientId, string clientSecret, string scope)
        {
            using (HttpClient client = new HttpClient())
            {
                FormUrlEncodedContent form = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", Username),
                    new KeyValuePair<string, string>("password", Encoding.UTF32.GetString(Password)),
                    new KeyValuePair<string, string>("scope", scope)
                });

                client.BaseAddress = new System.Uri("https://api.soundcloud.com");

                var response = client.PostAsync("oauth2/token", form);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    dynamic accesstokenResponse = JsonConvert.DeserializeObject(response.Result.Content.ReadAsStringAsync().Result);
                    AccessToken = accesstokenResponse.access_token;
                }
                else
                {
                    throw new System.Exception("Can't log in, please check your credentials");
                }
            }
        }

        public void Save(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(JsonConvert.SerializeObject(this));
            }
        }

        public AuthCredentialModel Load(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                return JsonConvert.DeserializeObject<AuthCredentialModel>(reader.ReadToEnd());
            }
        }
    }
}
