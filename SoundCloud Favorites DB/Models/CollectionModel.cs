namespace SoundCloud.Models
{
    using Newtonsoft.Json;

    public class CollectionModel
    {
        [JsonProperty("collection")]
        public FavoriteModel[] Collection { get; set; }

        [JsonProperty("next_href")]
        public string NextPage { get; set; }
    }
}
