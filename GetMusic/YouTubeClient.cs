using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GetMusic
{
    class YouTubeClient
    {
        private class YouTubeAPIOP
        {
            public string OP { get; set; }
            public string IDDesc { get; set; }
        }

        private string _APIKey;
        private YouTubeAPIOP _GetPlaylistsOP;
        private YouTubeAPIOP _GetPlaylistItemsOP;

        public YouTubeClient(string APIKey)
        {
            _APIKey = APIKey;
            _GetPlaylistsOP = new YouTubeAPIOP()
            {
                OP = "playlists",
                IDDesc = "channelId"
            };
            _GetPlaylistItemsOP = new YouTubeAPIOP()
            {
                OP = "playlistItems",
                IDDesc = "playlistId"
            };
        }

        public YouTubePlaylistsResponse.RootObject GetUsersPlaylists(string ID)
        {
            //https://www.googleapis.com/youtube/v3/playlists?part=snippet&channelId=UCePh7tbI8ZRatctu9GWuuKQ&key=AIzaSyB4LxrwY8neRJ5x3uoQpDRmCyD4J-P1vN4
            HttpWebRequest req = WebRequest.Create(_GenerateRequestURL(_GetPlaylistsOP, ID)) as HttpWebRequest;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                if (resp.StatusCode != HttpStatusCode.OK)
                    throw new YouTubeClientException("BAD API Request!");
                using (Stream stream = resp.GetResponseStream())
                using (StreamReader str = new StreamReader(stream))
                {
                    return JsonConvert.DeserializeObject<YouTubePlaylistsResponse.RootObject>(str.ReadToEnd());
                }
            }
        }
        
        public YouTubePlaylistItemsResponse.RootObject GetPlaylistItems(string ID/*, int pageNumber*/)
        {
            //https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&playlistId=PL1M2KefVP8WrtYIPxVija9nnC2bqEESzR&key=AIzaSyB4LxrwY8neRJ5x3uoQpDRmCyD4J-P1vN4&maxResults=50
            HttpWebRequest req = WebRequest.Create(_GenerateRequestURL(_GetPlaylistItemsOP, ID)) as HttpWebRequest;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                if (resp.StatusCode != HttpStatusCode.OK)
                    throw new YouTubeClientException("BAD API Request!");
                using (Stream stream = resp.GetResponseStream())
                using (StreamReader str = new StreamReader(stream))
                {
                    return JsonConvert.DeserializeObject<YouTubePlaylistItemsResponse.RootObject>(str.ReadToEnd());
                }
            }
        }


        private string _GenerateRequestURL(YouTubeAPIOP op, string ID)
        {
            return string.Format("https://www.googleapis.com/youtube/v3/{0}?part=snippet&{1}={2}&key={3}&maxResults=50",
                op.OP, op.IDDesc, ID, _APIKey);
        }
    }
}
