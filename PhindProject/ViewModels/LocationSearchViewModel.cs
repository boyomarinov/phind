using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using PhindProject.Data;
using PhindProject.Models;


namespace PhindProject.ViewModels
{
    public class LocationSearchViewModel : ViewModelBase
    {
        private double centerLatitude;
        public double CenterLatitude
        {
            get
            {
                return this.centerLatitude;
            }
            set
            {
                if (this.centerLatitude != value)
                {
                    this.centerLatitude = value;
                    this.OnPropertyChanged("CenterLatitude");
                }
            }
        }

        private double centerLongitude;
        public double CenterLongitude
        {
            get
            {
                return this.centerLongitude;
            }
            set
            {
                if (this.centerLongitude != value)
                {
                    this.centerLongitude = value;
                    this.OnPropertyChanged("CenterLongitude");
                }
            }
        }

        private string selectedRadius;
        public string SelectedRadius
        {
            get
            {
                return this.selectedRadius;
            }
            set
            {
                if (this.selectedRadius != value)
                {
                    this.selectedRadius = value;
                    this.OnPropertyChanged("SelectedRadius");
                }
            }
        }

        public IEnumerable<string> RadiusTypes { get; set; }

        private ObservableCollection<PlaceModel> mapPushPins;
        public IEnumerable<PlaceModel> MapPushPins
        {
            get
            {
                if (this.mapPushPins == null)
                {
                    this.mapPushPins = new ObservableCollection<PlaceModel>();
                }
                return this.mapPushPins;
            }
            set
            {
                if (this.mapPushPins == null)
                {
                    this.mapPushPins = new ObservableCollection<PlaceModel>();
                }
                this.SetObservableValues(this.mapPushPins, value);
            }
        }

        private async void GetData(string type)
        {
            try
            {
                //FlickrPersister flickr = new FlickrPersister();
                this.MapPushPins = new ObservableCollection<PlaceModel>(await FlickrAPIPersister.GetTopPlaces(type));
            }
            catch
            {
                //var msg = new MessageDialog("Check your internet connection and try again.");
                //msg.ShowAsync();
            }
        }

        public LocationSearchViewModel()
        {
            //this.SetCurrentLocation();
            RadiusTypes = new string[]{ "World", "Country", "Region", "Locality" };
            SelectedRadius = "City";
            this.GetData("Country");
        }
    }
}