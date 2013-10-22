using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.DataTransfer;
using System.Text;
using Windows.Storage.Streams;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237
using PhindProject.Common;
using PhindProject.Models;
using PhindProject.ViewModels;

namespace PhindProject.Pages
{

    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class DetailedPhotos : PhindProject.Common.LayoutAwarePage
    {
        public SearchUserPhotosViewModel SearchUserVM { get; set; }

        public SearchPhotosViewModel SearchPhotosVM { get; set; }

        public SearchViewModel SearchContractVM { get; set; }

        public DetailedPhotos()
        {
            this.InitializeComponent();
        }

        private DataTransferManager dataTransfer = null;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            dataTransfer = DataTransferManager.GetForCurrentView();

            dataTransfer.DataRequested -= this.OnDataRequested;
            dataTransfer.DataRequested += this.OnDataRequested;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            dataTransfer.DataRequested -= OnDataRequested;
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            //var request = args.Request;

            //var selectedIndex = this.DetailedFlipView.SelectedIndex;
            //Photo item = new Photo();
            //if (this.SearchUserVM != null)
            //{
            //    item = GetPhotoAtIndex(selectedIndex, this.SearchUserVM.UserPhotosCollection);
            //}
            //else
            //{
            //    if (this.SearchPhotosVM != null)
            //    {
            //        item = GetPhotoAtIndex(selectedIndex, this.SearchPhotosVM.MatchedPhotos);
            //    }
            //}

            DataPackage data = args.Request.Data;
            //data.Properties.Title = item.Title;
            data.Properties.Title = "Some title";
            //data.Properties.Description = item.Description;
            //data.SetUri(new Uri(item.LargeUrl));

            data.Properties.Thumbnail = RandomAccessStreamReference.CreateFromUri(new Uri("www.google.com"));
            data.SetText("Some random text");

            DataTransferManager.ShowShareUI();

            //request.Data.Properties.Title = item.Title;
            //if (item.Description != null)
            //{
            //    request.Data.Properties.Description = "No description";
            //}
            //else
            //{
            //    request.Data.Properties.Description = item.Description;
            //}

            //string textToShare = "The photo: " + item.LargeUrl;
            //request.Data.SetText(textToShare);
            //var reference = RandomAccessStreamReference.CreateFromUri(new Uri(item.Small320Url));
            //request.Data.Properties.Thumbnail = reference;
            //request.Data.SetBitmap(reference);
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

        private void DetailedPhotos_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (SearchUserVM != null)
            {
                this.DetailedFlipView.ItemsSource = SearchUserVM.UserPhotosCollection;
                this.DetailedFlipView.SelectedIndex = SearchUserVM.SelectedPhoto;
            }
            else
            {
                if (SearchContractVM != null)
                {
                    this.DetailedFlipView.ItemsSource = SearchContractVM.Results;
                    this.DetailedFlipView.SelectedIndex = SearchContractVM.SelectedPhoto;
                }
                else
                {
                    this.DetailedFlipView.ItemsSource = SearchPhotosVM.MatchedPhotos;
                    this.DetailedFlipView.SelectedIndex = SearchPhotosVM.SelectedPhoto;
                }
            }
        }

        private async Task<byte[]> DownloadImageFromWebsiteAsync(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                using (WebResponse response = await request.GetResponseAsync())
                using (var result = new MemoryStream())
                {
                    Stream imageStream = response.GetResponseStream();
                    await imageStream.CopyToAsync(result);
                    return result.ToArray();
                }
            }
            catch (WebException ex)
            {
                return null;
            }
        }

        private PhotoModel GetPhotoAtIndex(int index, IEnumerable<PhotoModel> collection)
        {
            int count = 0;
            foreach (var photo in collection)
            {
                if (count == index)
                {
                    return photo;
                }

                count++;
            }

            return null;
        }

        private async void SaveImageToFile(object sender, RoutedEventArgs e)
        {
            var selectedIndex = this.DetailedFlipView.SelectedIndex;
            PhotoModel selectedPhoto = new PhotoModel();
            if (this.SearchUserVM != null)
            {
                selectedPhoto = GetPhotoAtIndex(selectedIndex, this.SearchUserVM.UserPhotosCollection);
            }
            else
            {
                if (this.SearchPhotosVM != null)
                {
                    selectedPhoto = GetPhotoAtIndex(selectedIndex, this.SearchPhotosVM.MatchedPhotos);
                }
                else
                {
                    if (this.SearchContractVM != null)
                    {
                        selectedPhoto = GetPhotoAtIndex(selectedIndex, this.SearchContractVM.Results);
                    }
                }
            }

            if (selectedPhoto == null)
            {
                //TODO: handle error
            }
            //Byte[] imageToSave;
            var imageToSave = await DownloadImageFromWebsiteAsync(selectedPhoto.LargeUrl);

            var savePicker = new Windows.Storage.Pickers.FileSavePicker();

            var imageFileTypes = new List<string>(new string[] { ".jpeg", ".png" });

            savePicker.FileTypeChoices.Add(
                new KeyValuePair<string, IList<string>>("Images", imageFileTypes)
                );

            var saveFile = await savePicker.PickSaveFileAsync();

            if (saveFile != null)
            {
                await Windows.Storage.FileIO.WriteBytesAsync(saveFile, imageToSave);
                await new Windows.UI.Popups.MessageDialog("File Saved!").ShowAsync();
            }
        }
    }
}
