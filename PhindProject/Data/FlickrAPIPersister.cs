using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Newtonsoft.Json;
using PhindProject.Models;


namespace PhindProject.Data
{
    public static class FlickrAPIPersister
    {
        private const string BASE_URL = "http://phindapi.apphb.com/api/";
        private static readonly HttpClient client = new HttpClient();

        public static async Task<IEnumerable<PhotoModel>> GetPhotosByUser(string username)
        {
            string userPhotosUrl = BASE_URL + "photos/user-photos?username=" + username;
            HttpResponseMessage response = await client.GetAsync(userPhotosUrl);
            IEnumerable<PhotoModel> photos = new List<PhotoModel>();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                photos = JsonConvert.DeserializeObject<IEnumerable<PhotoModel>>(responseString);
            }
            else
            {
                var msg = new MessageDialog("There is a problem with your internet connection!");
                msg.ShowAsync();
            }

            return photos;
        }

        public static async Task<IEnumerable<PhotoModel>> GetPhotosByTitle(string title)
        {
            string titlePhotosUrl = BASE_URL + "photos/title-photos?title=" + title;
            HttpResponseMessage response = await client.GetAsync(titlePhotosUrl);
            IEnumerable<PhotoModel> photos = new List<PhotoModel>();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                photos = JsonConvert.DeserializeObject<IEnumerable<PhotoModel>>(responseString);
            }
            else
            {
                var msg = new MessageDialog("There is a problem with your internet connection!");
                msg.ShowAsync();
            }

            return photos;
        }

        public static async Task<IEnumerable<PlaceModel>> GetTopPlaces(string type)
        {
            string topPlacesUrl = BASE_URL + "places/top?type=" + type;
            //HttpResponseMessage response = await client.GetAsync(topPlacesUrl);
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = client.GetAsync(topPlacesUrl).GetAwaiter().GetResult();
            }
            catch
            {
                var msg = new MessageDialog("There is a problem with your internet connection!");
                msg.ShowAsync();
            }
            IEnumerable<PlaceModel> places = new List<PlaceModel>();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                places = JsonConvert.DeserializeObject<IEnumerable<PlaceModel>>(responseString);
            }
            else
            {
                var msg = new MessageDialog("Something went wrong while getting places!");
                msg.ShowAsync();
            }

            return places;
        }
    }
}
