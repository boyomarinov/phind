// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using PhindProject.Common;
using PhindProject.Data;
using PhindProject.Models;
using PhindProject.ViewModels;

namespace PhindProject.Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class UserPhotos : PhindProject.Common.LayoutAwarePage
    {
        public UserPhotos()
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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            DetailedPhotos details = e.Content as DetailedPhotos;
            if (details != null)
            {
                details.SearchUserVM = this.UserPhotosMainGrid.DataContext as SearchUserPhotosViewModel;
            }
        }

        private void UserPhotos_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            //var photoId = ((e.OriginalSource as Image).DataContext as Photo).PhotoId;
            var vm = this.UserPhotosMainGrid.DataContext as SearchUserPhotosViewModel;
            var photo = ((e.OriginalSource as Image).DataContext as PhotoModel);
            var photoIndex = (vm.UserPhotosCollection as ObservableCollection<PhotoModel>).IndexOf(photo);
            (this.UserPhotosMainGrid.DataContext as SearchUserPhotosViewModel).SelectedPhoto = photoIndex;

            this.Frame.Navigate(typeof(DetailedPhotos));
        }
    }
}