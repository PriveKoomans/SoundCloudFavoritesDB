namespace SoundCloud.Models
{
    using Newtonsoft.Json;

    public class UserModel
    {
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
