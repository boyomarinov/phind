using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using PhindProject.Data;
using PhindProject.Models;


namespace PhindProject.ViewModels
{
    public class SearchViewModel : Common.BindableBase
    {
        public SearchViewModel()
        {
            
        }

        public string queryText;
        public string QueryText
        {
            get
            {
                return this.queryText;
            }
            set
            {
                if (this.queryText != value)
                {
                    this.queryText = value;
                    this.OnPropertyChanged("QueryText");
                    this.LoadResults(this.queryText);
                }
            }
        }

        private ObservableCollection<PhotoModel> results;
        public IEnumerable<PhotoModel> Results
        {
            get
            {
                if (this.results == null)
                {
                    this.results = new ObservableCollection<PhotoModel>();
                }
                return this.results;
            }
            set
            {
                if (this.results == null)
                {
                    this.results = new ObservableCollection<PhotoModel>();
                }
                this.SetObservableValues(this.results, value);
            }
        }

        public int SelectedPhoto { get; set; }

        private async void LoadResults(string title)
        {
            try
            {
                //FlickrPersister flickr = new FlickrPersister();
                this.Results = new ObservableCollection<PhotoModel>(await FlickrAPIPersister.GetPhotosByTitle(title));
            }
            catch
            {
                var msg = new MessageDialog("Cannot search with empty query!");
                msg.ShowAsync();
            }
        }

        private void SetObservableValues<T>(ObservableCollection<T> observableCollection, IEnumerable<T> values)
        {
            if (observableCollection != values)
            {
                observableCollection.Clear();
                foreach (var item in values)
                {
                    observableCollection.Add(item);
                }
            }
        }
    }
}
