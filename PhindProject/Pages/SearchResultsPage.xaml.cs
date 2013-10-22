using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// The Search Contract item template is documented at http://go.microsoft.com/fwlink/?LinkId=234240
using PhindProject.Models;
using PhindProject.ViewModels;

namespace PhindProject.Pages
{
    /// <summary>
    /// This page displays search results when a global search is directed to this application.
    /// </summary>
    public sealed partial class SearchResultsPage : PhindProject.Common.LayoutAwarePage
    {
        public SearchResultsPage()
        {
            this.InitializeComponent();
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
            var queryText = navigationParameter as String;

            (this.DataContext as SearchViewModel).QueryText = queryText;
        }

        private void PhotosListView_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var vm = this.pageRoot.DataContext as SearchViewModel;
            var photo = ((e.OriginalSource as Image).DataContext as PhotoModel);
            var photoIndex = (vm.Results as ObservableCollection<PhotoModel>).IndexOf(photo);
            vm.SelectedPhoto = photoIndex;

            this.Frame.Navigate(typeof(DetailedPhotos));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            DetailedPhotos details = e.Content as DetailedPhotos;
            if (details != null)
            {
                details.SearchContractVM = this.pageRoot.DataContext as SearchViewModel;
            }
        }
    }
}
