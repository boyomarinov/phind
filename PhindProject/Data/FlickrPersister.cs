using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace PhindProject.Data
{
    public class FlickrPersister
    {
        //public Flickr Context { get; set; }

        //public FlickrPersister()
        //{
        //    this.Context = new Flickr("0c9642199bdc3afa4bc037439fe09c71", "e62b33c2ea0a4f93");
        //}

        //public Task<PhotoCollection> GetUserPhotosAsync(FoundUser user)
        //{
        //    return Task.Run(() => this.GetUserPhotos(user));
        //}

        //public FoundUser GetUser(string username)
        //{
        //    if (!String.IsNullOrEmpty(username))
        //    {
        //        try
        //        {
        //            return this.Context.PeopleFindByUserName(username);
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }

        //    return null;
        //}

        //public PhotoCollection GetUserPhotos(FoundUser user)
        //{
        //    //FoundUser user = null;
        //    //try
        //    //{
        //    //    user = this.Context.PeopleFindByUserName(username);
        //    //}
        //    //catch
        //    //{
        //    //    var msg = new MessageDialog("Flickr cannot find information about this user.");
        //    //    msg.ShowAsync();
        //    //}

        //    var photos = this.Context.PhotosSearch(new PhotoSearchOptions()
        //    {
        //        UserId = user.UserId,
        //        Page = 1,
        //        PerPage = 100
        //    });

        //    return photos;
        //}

        //public Task<PhotoCollection> GetPhotosByTitleAsync(string title)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(title))
        //        {
        //            return Task.Run(() => this.GetPhotosByTitle(title));
        //        }
        //    }
        //    catch
        //    {
        //        var msg = new MessageDialog("There is a problem with your internet connection!");
        //        msg.ShowAsync();
        //    }

        //    return null;
        //}

        //public PhotoCollection GetPhotosByTitle(string title)
        //{

        //    var photos = this.Context.PhotosSearch(new PhotoSearchOptions()
        //    {
        //        Text = title,
        //        SortOrder = PhotoSearchSortOrder.Relevance,
        //        Page = 1,
        //        PerPage = 100
        //    });

        //    return photos;

        //    return null;
        //}

        //public Task<PhotoCollection> GetPhotosByLocationAsync(double latitude, double longitude, string radius)
        //{
        //    try
        //    {
        //        return Task.Run(() => this.GetPhotosByLocation(latitude, longitude, radius));
        //    }
        //    catch
        //    {
        //        var msg = new MessageDialog("There is a problem with your internet connection!");
        //        msg.ShowAsync();
        //    }

        //    return null;
        //}

        //public PhotoCollection GetPhotosByLocation(double latitude, double longitude, string radius)
        //{
        //    GeoAccuracy type = new GeoAccuracy();
        //    int resultsCount = 0;
        //    switch (radius)
        //    {
        //        case "World":
        //            type = GeoAccuracy.World;
        //            resultsCount = 50;
        //            break;
        //        case "Country":
        //            type = GeoAccuracy.Country;
        //            resultsCount = 40;
        //            break;
        //        case "Region":
        //            type = GeoAccuracy.Region;
        //            resultsCount = 30;
        //            break;
        //        case "City":
        //            type = GeoAccuracy.City;
        //            resultsCount = 20;
        //            break;
        //        case "Street":
        //            type = GeoAccuracy.Street;
        //            resultsCount = 10;
        //            break;
        //        default:
        //            type = GeoAccuracy.City;
        //            resultsCount = 20;
        //            break;
        //    }

        //    var photos = this.Context.PhotosGeoPhotosForLocation(latitude, longitude, type, new PhotoSearchExtras() { }, resultsCount, 1);

        //    return photos;
        //}

        //public Task<PlaceCollection> GetTopPlacesAsync(PlaceType type)
        //{
        //    try
        //    {
        //        return Task.Run(() => this.GetTopPlaces(type));
        //    }
        //    catch
        //    {
        //        var msg = new MessageDialog("There is a problem with your internet connection!");
        //        msg.ShowAsync();
        //    }

        //    return null;
        //}

        //public PlaceCollection GetTopPlaces(PlaceType type)
        //{
        //    var places = this.Context.PlacesGetTopPlacesList(type);
        //    return places;
        //}

        //public PlaceCollection GetPlacesForRegion(string radius)
        //{
        //    GeoAccuracy type = new GeoAccuracy();
        //    int resultsCount = 0;
        //    switch (radius)
        //    {
        //        case "World":
        //            type = GeoAccuracy.World;
        //            resultsCount = 50;
        //            break;
        //        case "Country":
        //            type = GeoAccuracy.Country;
        //            resultsCount = 40;
        //            break;
        //        case "Region":
        //            type = GeoAccuracy.Region;
        //            resultsCount = 30;
        //            break;
        //        case "City":
        //            type = GeoAccuracy.City;
        //            resultsCount = 20;
        //            break;
        //        case "Street":
        //            type = GeoAccuracy.Street;
        //            resultsCount = 10;
        //            break;
        //        default:
        //            type = GeoAccuracy.City;
        //            resultsCount = 20;
        //            break;
        //    }

        //    PlaceCollection places = new PlaceCollection();


        //    places = this.Context.PlacesPlacesForBoundingBox(PlaceType.Region, "0", "0", new BoundaryBox(GeoAccuracy.World));

        //    return places;
        //}

        //public static bool ExecuteWithTimeLimit(TimeSpan timeSpan, Action codeBlock)
        //{
        //    try
        //    {
        //        Task task = Task.Factory.StartNew(() => codeBlock());
        //        task.Wait(timeSpan);
        //        return task.IsCompleted;
        //    }
        //    catch (AggregateException ae)
        //    {
        //        throw ae.InnerExceptions[0];
        //    }
        //}
    }
}
