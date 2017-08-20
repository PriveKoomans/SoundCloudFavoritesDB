namespace SoundCloud.Models
{
    using Newtonsoft.Json;
    using System.Text.RegularExpressions;

    public class FavoriteModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("stream_url")]
        public string StreamUrl { get; set; }

        [JsonProperty("artwork_url")]
        public string ArtworkUrl { get; set; }

        [JsonProperty("user")]
        public UserModel User { get; set; }

        public string[] SplitTitle()
        {
            Regex rgx = new Regex(@"\s-\s");

            if (!rgx.Match(Title).Success)
                return null;

            return rgx.Split(Title);
        }
    }
}
