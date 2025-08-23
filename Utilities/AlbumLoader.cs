
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Models;

namespace Utilities
{


using Utilities;

    // AlbumLoader as coordinator
    public class AlbumLoader
    {
        public static List<MusicRelease> LoadAlbums(string xmlFilePath)
        {
            var albums = new List<MusicRelease>();
            var doc = XDocument.Load(xmlFilePath);
            var countryService = new CountryLookupService(Path.Combine("data", "countrys.json"));
            var formatService = new FormatLookupService(Path.Combine("data", "formats.json"));
            var genreService = new GenreLookupService(Path.Combine("data", "genres.json"));
            var labelService = new LabelLookupService(Path.Combine("data", "labels.json"));
            var artistService = new ArtistLookupService(Path.Combine("data", "artists.json"));
            var packagingService = new PackagingLookupService(Path.Combine("data", "packagings.json"));
            var storeService = new StoreLookupService(Path.Combine("data", "stores.json"));
            var parser = new AlbumParser(countryService, formatService, genreService, labelService, artistService, packagingService, storeService);
            var musicNodes = doc.Descendants("music");
            int albumId = 1;
            foreach (var music in musicNodes)
            {
                var album = parser.ParseAlbum(music, albumId++);
                albums.Add(album);
            }
            return albums;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("DEBUG: Main started");
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: AlbumLoader <path to MusicCollectorzExportExample.xml>");
                return;
            }
            string xmlFilePath = args[0];
            if (!File.Exists(xmlFilePath))
            {
                Console.WriteLine($"File not found: {xmlFilePath}");
                return;
            }
            var albums = LoadAlbums(xmlFilePath);
            Console.WriteLine($"Loaded {albums.Count} albums.");
            foreach (var album in albums)
            {
                Console.WriteLine($"Artist: {album.Artists[0]} | Album: {album.Title} (ID: {album.Id})");
                if (album.Media != null)
                {
                    foreach (var media in album.Media)
                    {
                        Console.WriteLine($"  Media: {media.Title}");
                        if (media.Tracks != null)
                        {
                            foreach (var track in media.Tracks)
                            {
                                Console.WriteLine($"    Track: {track.Title} (ID: {track.Id})");
                            }
                        }
                    }
                }
            }
        }
    }
}
