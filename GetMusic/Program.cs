using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetMusic
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 1. enable youtube api
             * 2. figure out how to get list of files from youtube playlist (already did it at the past)
             * OR simply paste the youtube playlist here
             * 3. parse the playlist, create a list of {song name, track length}
             * 3.1 display a list of songs and length with checkboxes, prompt the user for which songs he wants to download
             * 4. try searching for the track name and search for the closest result length-wise, get only 320kbps
             * 5. if didn't find results, output log for better algorithm for next time
             * 6. download the file. don't push it, use only 2 threads.
             * 7. save it all at a dir called [playlist name]+[date]
             */

            YouTubeClient youtubeClient = new YouTubeClient("AIzaSyB4LxrwY8neRJ5x3uoQpDRmCyD4J-P1vN4");


            YouTubePlaylistsResponse.RootObject playlists = youtubeClient.GetUsersPlaylists("UCePh7tbI8ZRatctu9GWuuKQ");
            Console.WriteLine("Choose the desired playlist number:");
            for (int i = 0; i < playlists.items.Count; i++)
            {
                Console.WriteLine(i + ": " + playlists.items[i].snippet.title);
            }
            int selectedPlaylist;
            if (!_ValidateUserInput(s => Int32.Parse(s), Console.ReadLine(), out selectedPlaylist))
                Environment.Exit(1);


            YouTubePlaylistItemsResponse.RootObject playlistItems = youtubeClient.GetPlaylistItems(playlists.items[selectedPlaylist].id);
            Console.WriteLine("Choose the desired track number, 0 for all:");
            for (int i = 0; i < playlistItems.items.Count; i++)
            {
                Console.WriteLine("{0}: {1} [{2}]", i, playlistItems.items[i].snippet.title, playlistItems.items[i].snippet);
            }
            int selectedTrack;
            if (!_ValidateUserInput(s => Int32.Parse(s), Console.ReadLine(), out selectedTrack))
                Environment.Exit(1);
            





            Console.ReadLine();

        }

        private static bool _ValidateUserInput<T>(Func<string, T> check, string userInput, out T output)
        {
            output = default(T);
            try
            {
                output = check(userInput);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Bad input, don't have time for this shit, bye bye");
                return false;
            }
        }

    }
}
