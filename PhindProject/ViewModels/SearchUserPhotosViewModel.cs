using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Popups;
using PhindProject.Commands;
using PhindProject.Data;
using PhindProject.Models;

namespace PhindProject.ViewModels
{
    public class SearchUserPhotosViewModel : ViewModelBase
    {
        private string username;
        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                if (this.username != value)
                {
                    this.username = value;
                    this.OnPropertyChanged("Username");
                }
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

        private ObservableCollection<PhotoModel> userPhotosCollection;
        public IEnumerable<PhotoModel> UserPhotosCollection
        {
            get
            {
                if (this.userPhotosCollection == null)
                {
                    this.userPhotosCollection = new ObservableCollection<PhotoModel>();
                }
                return this.userPhotosCollection;
            }
            set
            {
                if (this.userPhotosCollection == null)
                {
                    this.userPhotosCollection = new ObservableCollection<PhotoModel>();
                }
                this.SetObservableValues(this.userPhotosCollection, value);
            }
        }

        private ICommand getPhotosOfUser;
        public ICommand GetPhotosOfUser
        {
            get
            {
                if (this.getPhotosOfUser == null)
                {
                    this.getPhotosOfUser = new RelayCommand(
                        this.HandleExecuteGetPhotosOfUserCommand);
                }
                
                return this.getPhotosOfUser;
            }
        }

        private void HandleExecuteGetPhotosOfUserCommand(object obj)
        {
            this.GetData(this.Username);
        }

        private async void GetData(string name)
        {
            this.IsLoading = true;
            
            try
            {
                this.UserPhotosCollection =
                    new ObservableCollection<PhotoModel>(await FlickrAPIPersister.GetPhotosByUser(name));
            }
            catch
            {
                var msg = new MessageDialog("Flickr cannot find information about this user or you experience an internet problem.");
                msg.ShowAsync();
            }

            this.IsLoading = false;
        }

        public SearchUserPhotosViewModel()
        {
            //this.Username = "Tasha1A";
        }
    }
}
