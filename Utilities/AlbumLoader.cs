using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Models;

namespace Utilities
{
    public class AlbumLoader
    {
        public static List<MusicAlbum> LoadAlbums(string xmlFilePath)
        {
            var albums = new List<MusicAlbum>();
            var doc = XDocument.Load(xmlFilePath);

            // Load country lookup from JSON
            var countryLookup = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var countryJsonPath = Path.Combine("data", "countrys.json");
            if (File.Exists(countryJsonPath))
            {
                var json = File.ReadAllText(countryJsonPath);
                var countryObj = System.Text.Json.JsonDocument.Parse(json);
                foreach (var country in countryObj.RootElement.GetProperty("countrys").EnumerateArray())
                {
                    var name = country.GetProperty("name").GetString();
                    var id = country.GetProperty("id").GetInt32();
                    if (!string.IsNullOrEmpty(name))
                        countryLookup[name] = id;
                }
            }

            var musicNodes = doc.Descendants("music");
            foreach (var music in musicNodes)
            {
                // Get country displayname
                int countryId = 0;
                var countryElem = music.Element("country");
                if (countryElem != null)
                {
                    var displayName = (string?)countryElem.Element("displayname") ?? string.Empty;
                    if (!string.IsNullOrEmpty(displayName) && countryLookup.TryGetValue(displayName, out int foundId))
                        countryId = foundId;
                }

                // Parse album-level data

                var album = new MusicAlbum
                {
                    Id = albums.Count + 1,
                    Title = (string?)music.Element("title") ?? string.Empty,
                    CountryId = countryId,
                    CoverFront = GetFileNameFromPath((string?)music.Element("coverfront")),
                    DateAdded = ParseDateAdded(music.Element("dateadded")?.Element("date")?.Value),
                    LastModified = ParseDateAdded(music.Element("lastmodified")?.Element("date")?.Value),
                    // Parse other fields as needed...
                };

                // Parse media/discs
                var mediaList = new List<MusicMedia>();
                var detailsNode = music.Element("details");
                if (detailsNode != null)
                {
                    foreach (var disc in detailsNode.Elements("detail").Where(d => (string?)d.Attribute("type") == "disc"))
                    {
                        var media = new MusicMedia
                        {
                            Title = (string?)disc.Element("title") ?? string.Empty,
                            // Parse other media fields as needed...
                        };

                        // Parse tracks for this media
                        var tracks = new List<MusicTrack>();
                        var trackDetails = disc.Element("details");
                        if (trackDetails != null)
                        {
                            foreach (var track in trackDetails.Elements("detail").Where(t => (string?)t.Attribute("type") == "track"))
                            {
                                var musicTrack = new MusicTrack
                                {
                                    Id = (int?)track.Element("id") ?? 0,
                                    Title = (string?)track.Element("title") ?? string.Empty,
                                    // Parse other track fields as needed...
                                };
                                tracks.Add(musicTrack);
                            }
                        }
                        media.Tracks = tracks;
                        mediaList.Add(media);
                    }
                }
                album.Media = mediaList;
                albums.Add(album);
            }
            return albums;
        }

        private static DateTime ParseDateAdded(string? dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr)) return default;
            // Try parsing with UK format (dd/MM/yyyy HH:mm:ss) and fallback to other formats
            if (DateTime.TryParseExact(dateStr, "dd/MM/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out var dt))
                return dt;
            if (DateTime.TryParse(dateStr, out dt))
                return dt;
            return default;
        }
        private static string? GetFileNameFromPath(string? path)
        {
            if (string.IsNullOrEmpty(path)) return null;

            // Replace both types of slashes with a single separator for splitting
            var separators = new char[] { '\\', '/' };
            var parts = path.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return parts.Length > 0 ? parts[^1] : path;
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
                Console.WriteLine($"Album: {album.Title} (ID: {album.Id})");
                if (album.Media != null)
                {
                    foreach (var media in album.Media)
                    {
                        Console.WriteLine($"  Media: {media.Title})");
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
