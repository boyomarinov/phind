using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Popups;
using PhindProject.Commands;
using PhindProject.Data;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using PhindProject.Models;


namespace PhindProject.ViewModels
{
    public class SearchPhotosViewModel : ViewModelBase
    {
        private string title;
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    this.OnPropertyChanged("Title");
                }
            }
        }

        private ObservableCollection<PhotoModel> matchedPhotos;
        public IEnumerable<PhotoModel> MatchedPhotos
        {
            get
            {
                if (this.matchedPhotos == null)
                {
                    this.matchedPhotos = new ObservableCollection<PhotoModel>();
                }
                return this.matchedPhotos;
            }
            set
            {
                if (this.matchedPhotos == null)
                {
                    this.matchedPhotos = new ObservableCollection<PhotoModel>();
                }
                this.SetObservableValues(this.matchedPhotos, value);
            }
        }

        private ICommand getPhotosByTitle;
        public ICommand GetPhotosByTitle
        {
            get
            {
                if (this.getPhotosByTitle == null)
                {
                    this.getPhotosByTitle = new RelayCommand(
                        this.HandleExecuteGetMatchingPhotosCommand);
                }

                return this.getPhotosByTitle;
            }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }
            set
            {
                if (this.isLoading != value)
                {
                    this.isLoading = value;
                    this.OnPropertyChanged("IsLoading");
                }
            }
        }

        public int SelectedPhoto { get; set; }

        private void HandleExecuteGetMatchingPhotosCommand(object obj)
        {
            //FlickrPersister flickr = new FlickrPersister();
            //PhotoCollection photos = flickr.GetPhotosByTitle(this.Title);
            //this.MatchedPhotos = new ObservableCollection<Photo>(photos);
            this.GetData(this.Title);
        }

        private async void GetData(string title)
        {
            this.IsLoading = true;
            try
            {
                //FlickrPersister flickr = new FlickrPersister();
                this.MatchedPhotos = new ObservableCollection<PhotoModel>(await FlickrAPIPersister.GetPhotosByTitle(title));
            }
            catch
            {
                //ToastTemplateType toastTemplate = ToastTemplateType.ToastText01;
                //XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                //XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                //toastTextElements[0].InnerText = "Cannot search with empty query!";
                ////IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
                ////XmlElement audio = toastXml.CreateElement("audio");
                ////audio.SetAttribute("src", "ms-winsoundevent:Notification.IM");
                //ToastNotification toast = new ToastNotification(toastXml);
                //ToastNotificationManager.CreateToastNotifier().Show(toast);
                var msg = new MessageDialog("Search query is blank or you are experiencing internet issues.");
                msg.ShowAsync();
            }
            this.IsLoading = false;
        }
    }
}
