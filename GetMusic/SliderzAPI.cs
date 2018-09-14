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
    class SliderzAPI
    {
        public SliderzResponse.Entry SearchTrack(string trackName)
        {
            HttpWebRequest req = WebRequest.Create(_GenerateURL(trackName)) as HttpWebRequest;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                if (resp.StatusCode != HttpStatusCode.OK)
                    throw new YouTubeClientException("BAD API Request!");
                using (Stream stream = resp.GetResponseStream())
                using (StreamReader str = new StreamReader(stream))
                {
                    SliderzResponse.RootObject results = JsonConvert.DeserializeObject<SliderzResponse.RootObject>(str.ReadToEnd());
                    int max = -1;
                    string url;
                    foreach (SliderzResponse.Entry entry in results.feed.Select(x => x.entry))
                    {

                    }
                }
            }
            return new SliderzResponse.Entry();
        }

        private int _CalcLevenshteinDistance(string a, string b)
        {
            if (String.IsNullOrEmpty(a) || String.IsNullOrEmpty(b)) return 0;

            int lengthA = a.Length;
            int lengthB = b.Length;
            var distances = new int[lengthA + 1, lengthB + 1];
            for (int i = 0; i <= lengthA; distances[i, 0] = i++) ;
            for (int j = 0; j <= lengthB; distances[0, j] = j++) ;

            for (int i = 1; i <= lengthA; i++)
                for (int j = 1; j <= lengthB; j++)
                {
                    int cost = b[j - 1] == a[i - 1] ? 0 : 1;
                    distances[i, j] = Math.Min
                        (
                        Math.Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1),
                        distances[i - 1, j - 1] + cost
                        );
                }
            return distances[lengthA, lengthB];
        }

        private string _GenerateURL(string trackName)
        {
            //http://slider.kz/new/include/vk_auth.php?act=source1&q=Alesso%20-%20I%20Wanna%20Know%20ft.%20Nico%20%20Vinz&page=0
            return string.Format("http://slider.kz/new/include/vk_auth.php?act=source1&q={0}&page=0", WebUtility.HtmlEncode(trackName));
        }
    }
}
