using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237
using PhindProject.Common;
using PhindProject.Models;
using PhindProject.ViewModels;

namespace PhindProject.Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class SearchPhotos : PhindProject.Common.LayoutAwarePage
    {
        private ApplicationDataContainer settings = null;

        public SearchPhotos()
        {
            this.InitializeComponent();
            this.NavigationCacheMode =
                Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
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
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void PhotosListView_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var vm = this.MatchedPhotosList.DataContext as SearchPhotosViewModel;
            var photo = ((e.OriginalSource as Image).DataContext as PhotoModel);
            var photoIndex = (vm.MatchedPhotos as ObservableCollection<PhotoModel>).IndexOf(photo);
            (this.MatchedPhotosList.DataContext as SearchPhotosViewModel).SelectedPhoto = photoIndex;

            this.Frame.Navigate(typeof(DetailedPhotos));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            Application.Current.Resuming -= Current_Resuming;
            Application.Current.Suspending += Current_Suspending;

            DetailedPhotos details = e.Content as DetailedPhotos;
            if (details != null)
            {
                details.SearchPhotosVM = this.MatchedPhotosList.DataContext as SearchPhotosViewModel;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            Application.Current.Suspending += Current_Suspending;
            Application.Current.Resuming += Current_Resuming;
            settings = ApplicationData.Current.LocalSettings;
        }

        private void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            settings.Values["Title"] = this.TitleTb.Text;
        }

        private void Current_Resuming(object sender, object e)
        {
            this.TitleTb.Text = settings.Values["Title"].ToString();
        }
    }
}
