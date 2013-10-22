using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PhindProject.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        public AppViewModel()
        {
            //this.searchProviders = new ObservableCollection<SearchProviderViewModel>()
            //{
            //    new SearchProviderViewModel("Google"),
            //    new SearchProviderViewModel("Bing"),
            //    new SearchProviderViewModel("Other"),
            //};
            this.SearchProviders = new string[]{"Google", "Bing", "Other" };
            this.Sizes = new string[]{"big", "medium", "small"};
            this.Colors = new string[]{"red", "green", "blue"};
            this.Types = new string[]{"people", "clipart", "landscapes"};
        }

        public string searchQuery;
        public string SearchQuery
        {
            get
            {
                return this.searchQuery;
            }
            set
            {
                if (this.searchQuery != value)
                {
                    this.searchQuery = value;
                    this.OnPropertyChanged("SeachQuery");
                }
            }
        }


        //private ObservableCollection<SearchProviderViewModel> searchProviders;
        //public IEnumerable<SearchProviderViewModel> SearchProviders
        //{
        //    get
        //    {
        //        if (this.searchProviders == null)
        //        {
        //            this.searchProviders = new ObservableCollection<SearchProviderViewModel>();
        //        }
        //        return this.searchProviders;
        //    }
        //    set
        //    {
        //        if (this.searchProviders == null)
        //        {
        //            this.searchProviders = new ObservableCollection<SearchProviderViewModel>();
        //        }
        //        this.SetObservableValues(this.searchProviders, value);
        //    }
        //}

        public IEnumerable<string> SearchProviders { get; set; } 
        public IEnumerable<string> Sizes { get; set; } 
        public IEnumerable<string> Colors { get; set; } 
        public IEnumerable<string> Types { get; set; } 

        //private ObservableCollection<string> sizes;
        //public IEnumerable<string> Sizes
        //{
        //    get
        //    {
        //        if (this.sizes == null)
        //        {
        //            this.sizes = new ObservableCollection<string>();
        //        }
        //        return this.sizes;
        //    }
        //    set
        //    {
        //        if (this.sizes == null)
        //        {
        //            this.sizes = new ObservableCollection<string>();
        //        }
        //        this.SetObservableValues(this.sizes, value);
        //    }
        //}

        //private ObservableCollection<string> colors;
        //public IEnumerable<string> Colors
        //{
        //    get
        //    {
        //        if (this.colors == null)
        //        {
        //            this.colors = new ObservableCollection<string>();
        //        }
        //        return this.colors;
        //    }
        //    set
        //    {
        //        if (this.colors == null)
        //        {
        //            this.colors = new ObservableCollection<string>();
        //        }
        //        this.SetObservableValues(this.colors, value);
        //    }
        //}

        //private ObservableCollection<string> types;
        //public IEnumerable<string> Types
        //{
        //    get
        //    {
        //        if (this.types == null)
        //        {
        //            this.types = new ObservableCollection<string>();
        //        }
        //        return this.types;
        //    }
        //    set
        //    {
        //        if (this.types == null)
        //        {
        //            this.types = new ObservableCollection<string>();
        //        }
        //        this.SetObservableValues(this.types, value);
        //    }
        //}


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
