using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237
using Bing.Maps;
using Bing.Maps.VenueMaps;
using PhindProject.Common;
using PhindProject.Data;
using PhindProject.Models;
using PhindProject.ViewModels;

namespace PhindProject.Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class LocationSearch : PhindProject.Common.LayoutAwarePage
    {
        public Geolocator Locator { get; set; }

        public List<PhotoModel> PhotosInRadius { get; set; }

        public Location CurrentPosition { get; set; }

        private ApplicationDataContainer settings;

        private IAsyncOperation<IUICommand> asyncMessage = null;

        public LocationSearch()
        {
            this.InitializeComponent();

            this.HandleMapInit();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            //if (settings.Values.ContainsKey("LastLat"))
            //{
            //    this.CenterLatitude.Text = settings.Values["LastLat"] as string;
            //}

            //if (settings.Values.ContainsKey("LastLng"))
            //{
            //    this.CenterLongitude.Text = settings.Values["LastLng"] as string;
            //}
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            //settings.Values["LastLat"] = this.CenterLatitude.Text;
            //settings.Values["LastLng"] = this.CenterLongitude.Text;
        }

        private async void HandleMapInit()
        {
            //Init map and get current location
            //TODO: Get saved values for location if any
            if (this.CenterLatitude.Text != "" && this.CenterLongitude.Text != "")
            {
                this.CurrentPosition = new Location(double.Parse(this.CenterLatitude.Text),
                                               double.Parse(this.CenterLongitude.Text));
            }
            else
            {
                try
                {
                    this.Locator = new Geolocator();
                    var ctokenSource = new CancellationTokenSource();
                    CancellationToken token = ctokenSource.Token;
                    Geoposition pos = await Locator.GetGeopositionAsync().AsTask(token);
                    this.CurrentPosition = new Location(pos.Coordinate.Latitude, pos.Coordinate.Longitude);
                }
                catch
                {
                    var msg = new MessageDialog("Something went wrong while trying to get your location.");
                    if (asyncMessage != null)
                    {
                        asyncMessage.Cancel();
                        asyncMessage = null;
                    }
                    asyncMessage = msg.ShowAsync();
                }
            }

            var context = this.DataContext as LocationSearchViewModel;
            if (CurrentPosition == null)
            {
                var msg = new MessageDialog("Map was not centered properly, because location is not allowed.");
                if (asyncMessage != null)
                {
                    asyncMessage.Cancel();
                    asyncMessage = null;
                }
                asyncMessage = msg.ShowAsync();
            }
            else
            {
                this.CenterLatitude.Text = CurrentPosition.Latitude.ToString();
                this.CenterLongitude.Text = CurrentPosition.Longitude.ToString();
            }

            context.SelectedRadius = "Country";

            var myLocation = new Pushpin();
            myLocation.SetValue(MapLayer.PositionProperty, this.CurrentPosition);
            //snappedBingMapContainer.Children.Add(myLocation);
            BingMapContainer.Children.Add(myLocation);

            if (this.CurrentPosition != null)
            {
                //snappedBingMapContainer.Center = this.CurrentPosition;
                BingMapContainer.Center = this.CurrentPosition;
            }

            //snappedBingMapContainer.ZoomLevel = 4;
            BingMapContainer.ZoomLevel = 4;

            this.CreateMap("Country");
        }

        private void CreateMap(string type)
        {
            try
            {
                var places = FlickrAPIPersister.GetTopPlaces(type).GetAwaiter().GetResult();

                foreach (var place in places)
                {
                    var pin = new Pushpin();
                    pin.Style = this.Resources["PushPinStyle"] as Style;
                    pin.SetValue(MapLayer.PositionProperty, new Location(place.Latitude, place.Longitude));
                    //pin.SetValue(MapLayer.NameProperty, place.PhotoCount);
                    pin.Text = place.PhotoCount.ToString();
                    //pin.SetValue();
                    string placeStr = "Place: " + place.Description;
                    string latStr = "Latitude: " + place.Latitude;
                    string lngStr = "Longitude: " + place.Longitude;
                    string countStr = "Photo count: " + place.PhotoCount;
                    string timezoneStr = "Timezone: " + place.Timezone;
                    string tooltipContent = string.Join(Environment.NewLine,
                                                        new string[] { placeStr, latStr, lngStr, countStr, timezoneStr });

                    ToolTipService.SetToolTip(pin, tooltipContent);
                    this.BingMapContainer.Children.Add(pin);
                }
            }
            catch
            {
                var msg = new MessageDialog("Check your internet connection and try again.");
                //msg.ShowAsync();
                //this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => msg.ShowAsync());
                if (asyncMessage != null)
                {
                    asyncMessage.Cancel();
                    asyncMessage = null;
                }
                //asyncMessage = msg.ShowAsync();
            }
        }

        private void LocationSearch_OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        //private void PopulatePhotos()
        //{
        //    if (this.CenterLatitude.Text != "" && this.CenterLongitude.Text != "")
        //    {
        //        double lat = double.Parse(this.CenterLatitude.Text);
        //        double lng = double.Parse(this.CenterLongitude.Text);
        //        string radius = this.RadiusComboBox.SelectedValue.ToString();

        //        this.GetData(lat, lng, radius);
        //    }
        //}

        //private async void GetData(double latitude, double longitude, string radius)
        //{
        //    try
        //    {
        //        FlickrPersister flickr = new FlickrPersister();
        //        this.PhotosInRadius = new List<Photo>(await flickr.GetPhotosByLocationAsync(latitude, longitude, radius));
        //    }
        //    catch
        //    {
        //        var msg = new MessageDialog("Check your internet connection and try again.");
        //        msg.ShowAsync();
        //    }
        //}

        private void ChangeCenter()
        {
            if (this.CenterLatitude.Text != "" && this.CenterLongitude.Text != "")
            {
                double lat = -91, lng = -91;
                double.TryParse(this.CenterLatitude.Text, out lat);
                double.TryParse(this.CenterLongitude.Text, out lng);
                if (lat < -90 || lat > 90 || lng < -90 || lng > 90)
                {

                }
                else
                {
                    var loc = new Location(lat, lng);
                    this.BingMapContainer.Center = loc;

                    var myLocation = new Pushpin();
                    myLocation.SetValue(MapLayer.PositionProperty, loc);
                    BingMapContainer.Children.Add(myLocation);
                }
            }
        }

        private void SearchOptions_OnInputChange(object sender, TextChangedEventArgs e)
        {
            ChangeCenter();
        }

        private void RadiusComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var type = this.RadiusComboBox.SelectedItem.ToString();
            //PlaceType type = new PlaceType();
            //switch (option)
            //{
            //    case "Continent":
            //        type = PlaceType.Continent;
            //        break;
            //    case "Country":
            //        type = PlaceType.Country;
            //        break;
            //    case "Region":
            //        type = PlaceType.Region;
            //        break;
            //    case "Locality":
            //        type = PlaceType.Locality;
            //        break;
            //    default:
            //        type = PlaceType.Country;
            //        break;
            //}

            this.CreateMap(type);
        }
    }
}
