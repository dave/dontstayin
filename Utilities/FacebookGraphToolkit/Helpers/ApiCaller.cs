using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookGraphToolkit.GraphApi;
using JSON;
using FacebookGraphToolkit.FacebookObjects;

namespace FacebookGraphToolkit.Helpers {
    class ApiCaller {
        internal static IList<Photo> GetPhotos(string ID, string AccessToken) {
            IList<Photo> Photos = new List<Photo>();

            string URL = string.Format("https://graph.facebook.com/{0}/photos?access_token={1}", ID, AccessToken);
            JsonObject JO = new JsonObject(Helpers.WebResponseHelper.GetWebResponse(URL));
            JsonArray dataArray = (JsonArray)JO["data"];
            foreach (JsonObject PhotoJO in dataArray.JsonObjects) {
                Photos.Add(new Photo(PhotoJO));
            }
            return Photos;
        }
        internal static IList<Album> GetAlbums(string ID,string AccessToken) {
            IList<Album> Albums = new List<Album>();

            string URL = string.Format("https://graph.facebook.com/{0}/albums?access_token={1}", ID, AccessToken);
            JsonObject JO = new JsonObject(Helpers.WebResponseHelper.GetWebResponse(URL));
            JsonArray dataArray = (JsonArray)JO["data"];
            foreach (JsonObject AlbumJO in dataArray.JsonObjects) {
                Albums.Add(new Album(AlbumJO));
            }
            return Albums;
        }
        internal static IList<Post> GetPosts(string ID, string AccessToken) {
            IList<Post> Posts = new List<Post>();

            string URL = string.Format("https://graph.facebook.com/{0}/posts?access_token={1}", ID, AccessToken);
            JsonObject JO = new JsonObject(Helpers.WebResponseHelper.GetWebResponse(URL));
            JsonArray dataArray = (JsonArray)JO["data"];
            foreach (JsonObject PhotoJO in dataArray.JsonObjects) {
                Posts.Add(new Post(PhotoJO));
            }
            return Posts;
        }
        internal static IList<Post> GetFeed(string ID, string AccessToken) {
            IList<Post> Posts = new List<Post>();

            string URL = string.Format("https://graph.facebook.com/{0}/feed?access_token={1}", ID, AccessToken);
            JsonObject JO = new JsonObject(Helpers.WebResponseHelper.GetWebResponse(URL));
            JsonArray dataArray = (JsonArray)JO["data"];
            foreach (JsonObject PhotoJO in dataArray.JsonObjects) {
                Posts.Add(new Post(PhotoJO));
            }
            return Posts;
        }
        internal static IList<NameIDPair> GetFriends(string ID, string AccessToken) {
            IList<NameIDPair> Friends = new List<NameIDPair>();

            string URL = string.Format("https://graph.facebook.com/{0}/friends?access_token={1}", ID, AccessToken);
            JsonObject JO = new JsonObject(Helpers.WebResponseHelper.GetWebResponse(URL));
            JsonArray dataArray = (JsonArray)JO["data"];
            foreach (JsonObject FriendJO in dataArray.JsonObjects) {
                Friends.Add(new NameIDPair(FriendJO));
            }
            return Friends;
        }
        internal static IList<Post> GetHome(string ID, string AccessToken) {
            IList<Post> Posts = new List<Post>();

            string URL = string.Format("https://graph.facebook.com/{0}/home?access_token={1}", ID, AccessToken);
            JsonObject JO = new JsonObject(Helpers.WebResponseHelper.GetWebResponse(URL));
            JsonArray dataArray = (JsonArray)JO["data"];
            foreach (JsonObject PhotoJO in dataArray.JsonObjects) {
                Posts.Add(new Post(PhotoJO));
            }
            return Posts;
        }
        internal static IList<Link> GetLinks(string ID, string AccessToken) {
            IList<Link> Links = new List<Link>();

            string URL = string.Format("https://graph.facebook.com/{0}/links?access_token={1}", ID, AccessToken);
            JsonObject JO = new JsonObject(Helpers.WebResponseHelper.GetWebResponse(URL));
            JsonArray dataArray = (JsonArray)JO["data"];
            foreach (JsonObject LinkJO in dataArray.JsonObjects) {
                Links.Add(new Link(LinkJO));
            }
            return Links;
        }
        internal static IList<StatusMessage> GetStatusMessages(string ID, string AccessToken) {
            IList<StatusMessage> StatusMessages = new List<StatusMessage>();

            string URL = string.Format("https://graph.facebook.com/{0}/statuses?access_token={1}", ID, AccessToken);
            JsonObject JO = new JsonObject(Helpers.WebResponseHelper.GetWebResponse(URL));
            JsonArray dataArray = (JsonArray)JO["data"];
            foreach (JsonObject _JO in dataArray.JsonObjects) {
                StatusMessages.Add(new StatusMessage(_JO));
            }
            return StatusMessages;
        }
        internal static IList<Note> GetNotes(string ID, string AccessToken) {
            IList<Note> _List = new List<Note>();

            string URL = string.Format("https://graph.facebook.com/{0}/notes?access_token={1}", ID, AccessToken);
            JsonObject JO = new JsonObject(Helpers.WebResponseHelper.GetWebResponse(URL));
            JsonArray dataArray = (JsonArray)JO["data"];
            foreach (JsonObject _JO in dataArray.JsonObjects) {
                _List.Add(new Note(_JO));
            }
            return _List;
        }
        internal static IList<LikedPage> GetLikedPages(string ID, string AccessToken) {
            IList<LikedPage> _List = new List<LikedPage>();

            string URL = string.Format("https://graph.facebook.com/{0}/likes?access_token={1}", ID, AccessToken);
            JsonObject JO = new JsonObject(Helpers.WebResponseHelper.GetWebResponse(URL));
            JsonArray dataArray = (JsonArray)JO["data"];
            foreach (JsonObject _JO in dataArray.JsonObjects) {
                _List.Add(new LikedPage(_JO));
            }
            return _List;
        }
        internal static IList<NameIDPair> GetGroups(string ID, string AccessToken) {
            IList<NameIDPair> _List = new List<NameIDPair>();

            string URL = string.Format("https://graph.facebook.com/{0}/groups?access_token={1}", ID, AccessToken);
            JsonObject JO = new JsonObject(Helpers.WebResponseHelper.GetWebResponse(URL));
            JsonArray dataArray = (JsonArray)JO["data"];
            foreach (JsonObject _JO in dataArray.JsonObjects) {
                _List.Add(new NameIDPair(_JO));
            }
            return _List;
        }
        internal static IList<UserEvent> GetEvents(string ID, string AccessToken) {
            IList<UserEvent> _List = new List<UserEvent>();

            string URL = string.Format("https://graph.facebook.com/{0}/events?access_token={1}", ID, AccessToken);
            JsonObject JO = new JsonObject(Helpers.WebResponseHelper.GetWebResponse(URL));
            JsonArray dataArray = (JsonArray)JO["data"];
            foreach (JsonObject _JO in dataArray.JsonObjects) {
                _List.Add(new UserEvent(_JO));
            }
            return _List;
        }
    }
}
