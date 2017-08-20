namespace SoundCloud
{
    using SoundCloud.Models;
    using SoundCloud.Extensions;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;

    public class DatabaseClient
    {
        string favTemplate = "https://api.soundcloud.com/me/favorites?oauth_token={0}";

        public AuthCredentialModel Credentials { get; set; }
        public List<FavoriteModel> Favorites { get; set; }

        public event EventHandler CollectionRetrieved;

        public DatabaseClient(AuthCredentialModel credentials)
        {
            Credentials = credentials;
        }

        public void RetrieveFavorites()
        {
            string url = string.Format(favTemplate, Credentials.AccessToken);
            Task.Run(() => GetFavorites(url));
        }

        internal void GetFavorites(string url)
        {
            string page = url.GetSource();
            CollectionModel data = null;

            if (page != null)
                data = JsonConvert.DeserializeObject<CollectionModel>(page);

            if (data != null)
                Favorites.AddRange(data.Collection);
            else
                throw new Exception(string.Format("{0} - There was an error retrieving your favorites", DateTime.Now.ToString()));

            if (data.NextPage != null)
                Task.Run(() => GetFavorites(data.NextPage));
            else
                CollectionRetrieved(this, new EventArgs());
        }
    }
}
