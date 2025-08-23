
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Models;

namespace Utilities
{
    // Service for store lookup
    public class StoreLookupService
    {
        private readonly Dictionary<string, int> _storeLookup;
        public StoreLookupService(string jsonPath)
        {
            _storeLookup = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            if (File.Exists(jsonPath))
            {
                var json = File.ReadAllText(jsonPath);
                var storeObj = System.Text.Json.JsonDocument.Parse(json);
                foreach (var store in storeObj.RootElement.GetProperty("stores").EnumerateArray())
                {
                    var name = store.GetProperty("name").GetString();
                    var id = store.GetProperty("id").GetInt32();
                    if (!string.IsNullOrEmpty(name))
                        _storeLookup[name] = id;
                }
            }
        }
        public int GetStoreId(string? displayName)
        {
            if (!string.IsNullOrEmpty(displayName) && _storeLookup.TryGetValue(displayName, out int foundId))
                return foundId;
            return 0;
        }
    }
    // Service for packaging lookup
    public class PackagingLookupService
    {
        private readonly Dictionary<string, int> _packagingLookup;
        public PackagingLookupService(string jsonPath)
        {
            _packagingLookup = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            if (File.Exists(jsonPath))
            {
                var json = File.ReadAllText(jsonPath);
                var packagingObj = System.Text.Json.JsonDocument.Parse(json);
                foreach (var packaging in packagingObj.RootElement.GetProperty("packagings").EnumerateArray())
                {
                    var name = packaging.GetProperty("name").GetString();
                    var id = packaging.GetProperty("id").GetInt32();
                    if (!string.IsNullOrEmpty(name))
                        _packagingLookup[name] = id;
                }
            }
        }
        public int GetPackagingId(string? displayName)
        {
            if (!string.IsNullOrEmpty(displayName) && _packagingLookup.TryGetValue(displayName, out int foundId))
                return foundId;
            return 0;
        }
    }

    // Service for label lookup
    // Service for artist lookup
    public class ArtistLookupService
    {
        private readonly Dictionary<string, int> _artistLookup;
        public ArtistLookupService(string jsonPath)
        {
            _artistLookup = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            if (File.Exists(jsonPath))
            {
                var json = File.ReadAllText(jsonPath);
                var artistObj = System.Text.Json.JsonDocument.Parse(json);
                int id = 1;
                foreach (var artist in artistObj.RootElement.GetProperty("artists").EnumerateArray())
                {
                    var name = artist.GetProperty("name").GetString();
                    if (!string.IsNullOrEmpty(name) && !_artistLookup.ContainsKey(name))
                        _artistLookup[name] = id++;
                }
            }
        }
        public int GetArtistId(string? displayName)
        {
            if (!string.IsNullOrEmpty(displayName) && _artistLookup.TryGetValue(displayName, out int foundId))
                return foundId;
            return 0;
        }
    }
    public class LabelLookupService
    {
        private readonly Dictionary<string, int> _labelLookup;
        public LabelLookupService(string jsonPath)
        {
            _labelLookup = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            if (File.Exists(jsonPath))
            {
                var json = File.ReadAllText(jsonPath);
                var labelObj = System.Text.Json.JsonDocument.Parse(json);
                int id = 1;
                foreach (var label in labelObj.RootElement.GetProperty("labels").EnumerateArray())
                {
                    var name = label.GetProperty("name").GetString();
                    if (!string.IsNullOrEmpty(name) && !_labelLookup.ContainsKey(name))
                        _labelLookup[name] = id++;
                }
            }
        }
        public int GetLabelId(string? displayName)
        {
            if (!string.IsNullOrEmpty(displayName) && _labelLookup.TryGetValue(displayName, out int foundId))
                return foundId;
            return 0;
        }
    }

    // Service for genre lookup
    public class GenreLookupService
    {
        private readonly Dictionary<string, int> _genreLookup;
        public GenreLookupService(string jsonPath)
        {
            _genreLookup = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            if (File.Exists(jsonPath))
            {
                var json = File.ReadAllText(jsonPath);
                var genreObj = System.Text.Json.JsonDocument.Parse(json);
                foreach (var genre in genreObj.RootElement.GetProperty("genres").EnumerateArray())
                {
                    var name = genre.GetProperty("name").GetString();
                    var id = genre.GetProperty("id").GetInt32();
                    if (!string.IsNullOrEmpty(name))
                        _genreLookup[name] = id;
                }
            }
        }
        public int GetGenreId(string? displayName)
        {
            if (!string.IsNullOrEmpty(displayName) && _genreLookup.TryGetValue(displayName, out int foundId))
                return foundId;
            return 0;
        }
    }
    // Service for country lookup
    public class CountryLookupService
    {
        private readonly Dictionary<string, int> _countryLookup;
        public CountryLookupService(string jsonPath)
        {
            _countryLookup = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            if (File.Exists(jsonPath))
            {
                var json = File.ReadAllText(jsonPath);
                var countryObj = System.Text.Json.JsonDocument.Parse(json);
                foreach (var country in countryObj.RootElement.GetProperty("countrys").EnumerateArray())
                {
                    var name = country.GetProperty("name").GetString();
                    var id = country.GetProperty("id").GetInt32();
                    if (!string.IsNullOrEmpty(name))
                        _countryLookup[name] = id;
                }
            }
        }
        public int GetCountryId(string? displayName)
        {
            if (!string.IsNullOrEmpty(displayName) && _countryLookup.TryGetValue(displayName, out int foundId))
                return foundId;
            return 0;
        }
    }

    // Service for format lookup
    public class FormatLookupService
    {
        private readonly Dictionary<string, int> _formatLookup;
        public FormatLookupService(string jsonPath)
        {
            _formatLookup = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            if (File.Exists(jsonPath))
            {
                var json = File.ReadAllText(jsonPath);
                var formatObj = System.Text.Json.JsonDocument.Parse(json);
                foreach (var format in formatObj.RootElement.GetProperty("formats").EnumerateArray())
                {
                    var name = format.GetProperty("name").GetString();
                    var id = format.GetProperty("id").GetInt32();
                    if (!string.IsNullOrEmpty(name))
                        _formatLookup[name] = id;
                }
            }
        }
        public int GetFormatId(string? displayName)
        {
            if (!string.IsNullOrEmpty(displayName) && _formatLookup.TryGetValue(displayName, out int foundId))
                return foundId;
            return 0;
        }
    }

    // Parser for MusicAlbum from XML
    public class AlbumParser
    {
        private readonly CountryLookupService _countryService;
        private readonly FormatLookupService _formatService;
        private readonly GenreLookupService _genreService;
        private readonly LabelLookupService _labelService;
        private readonly ArtistLookupService _artistService;
        private readonly PackagingLookupService _packagingService;
        private readonly StoreLookupService _storeService;

        public AlbumParser(CountryLookupService countryService, FormatLookupService formatService, GenreLookupService genreService, LabelLookupService labelService, ArtistLookupService artistService, PackagingLookupService packagingService, StoreLookupService storeService)
        {
            _countryService = countryService;
            _formatService = formatService;
            _genreService = genreService;
            _labelService = labelService;
            _artistService = artistService;
            _packagingService = packagingService;
            _storeService = storeService;
        }

        public MusicAlbum ParseAlbum(XElement music, int albumId)
        {
            // PurchaseInfo
            PurchaseData? purchaseInfo = null;
            var storeElem = music.Element("store");
            var storeDisplay = (string?)storeElem?.Element("displayname") ?? string.Empty;
            int storeId = _storeService.GetStoreId(storeDisplay);
            var purchaseDateStr = (string?)music.Element("purchasedate")?.Element("date") ?? string.Empty;
            DateOnly purchaseDate = default;
            if (!string.IsNullOrWhiteSpace(purchaseDateStr))
                DateOnly.TryParseExact(purchaseDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out purchaseDate);
            var priceStr = (string?)music.Element("purchaseprice") ?? string.Empty;
            decimal price = 0;
            if (!string.IsNullOrWhiteSpace(priceStr))
            {
                // Remove any non-numeric, non-dot, non-comma characters (e.g., currency symbols)
                var cleaned = new string(priceStr.Where(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());
                decimal.TryParse(cleaned, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out price);
            }
            if (storeId != 0 || purchaseDate != default || price != 0)
            {
                purchaseInfo = new PurchaseData
                {
                    StoreID = storeId,
                    Date = purchaseDate,
                    Price = price
                };
            }
           // Artists
            List<int> artistIds = new List<int>();
            var artistsElem = music.Element("artists");
            if (artistsElem != null)
            {
                foreach (var artistElem in artistsElem.Elements("artist"))
                {
                    var displayName = (string?)artistElem.Element("displayname") ?? string.Empty;
                    int artistId = _artistService.GetArtistId(displayName);
                    if (artistId != 0)
                        artistIds.Add(artistId);
                }
            }

            // OrigReleaseYear
            DateOnly origReleaseYear = default;
            var origReleaseElem = music.Element("origreleasedate")?.Element("year")?.Element("displayname");
            if (origReleaseElem != null && DateOnly.TryParseExact(origReleaseElem.Value, "yyyy", out var yearVal))
            {
                origReleaseYear = yearVal;
            }

            // ReleaseYear
            DateOnly releaseYear = default;
            var releaseElem = music.Element("releasedate")?.Element("year")?.Element("displayname");
            if (releaseElem != null && DateOnly.TryParseExact(releaseElem.Value, "yyyy", out var relYearVal))
            {
                releaseYear = relYearVal;
            }

            // Country
            int countryId = 0;
            var countryElem = music.Element("country");
            if (countryElem != null)
            {
                var displayName = (string?)countryElem.Element("displayname") ?? string.Empty;
                countryId = _countryService.GetCountryId(displayName);
            }

            // Format
            int formatId = 0;
            var formatElem = music.Element("format");
            if (formatElem != null)
            {
                var formatDisplay = (string?)formatElem.Element("displayname") ?? string.Empty;
                formatId = _formatService.GetFormatId(formatDisplay);
            }

            // Label
            int labelId = 0;
            var labelElem = music.Element("label");
            if (labelElem != null)
            {
                var labelDisplay = (string?)labelElem.Element("displayname") ?? string.Empty;
                labelId = _labelService.GetLabelId(labelDisplay);
            }

            // Live
            bool isLive = false;
            var liveElem = music.Element("live");
            if (liveElem != null)
            {
                var boolAttr = liveElem.Attribute("boolvalue")?.Value;
                isLive = boolAttr == "1";
            }

            // Packaging
            int packagingId = 0;
            var packagingElem = music.Element("packaging");
            if (packagingElem != null)
            {
                var packagingDisplay = (string?)packagingElem.Element("displayname") ?? string.Empty;
                packagingId = _packagingService.GetPackagingId(packagingDisplay);
            }
            
             // Genres
            List<int> genreIds = new List<int>();
            var genresElem = music.Element("genres");
            if (genresElem != null)
            {
                foreach (var genreElem in genresElem.Elements("genre"))
                {
                    var displayName = (string?)genreElem.Element("displayname") ?? string.Empty;
                    int genreId = _genreService.GetGenreId(displayName);
                    if (genreId != 0)
                        genreIds.Add(genreId);
                }
            }

            // Links
            List<LinkData> links = new List<LinkData>();
            var linksElem = music.Element("links");
            if (linksElem != null)
            {
                foreach (var linkElem in linksElem.Elements("link"))
                {
                    var url = (string?)linkElem.Element("url") ?? string.Empty;
                    var description = (string?)linkElem.Element("description") ?? string.Empty;
                    var urltype = (string?)linkElem.Element("urltype") ?? string.Empty;
                    if (!string.IsNullOrEmpty(url) || !string.IsNullOrEmpty(description))
                        links.Add(new LinkData { Url = url, Description = description, UrlType = urltype });
                }
            }
        
            var album = new MusicAlbum
            {
                Id = albumId,
                Title = (string?)music.Element("title") ?? string.Empty,
                CountryId = countryId,
                FormatId = formatId,
                LabelId = labelId,
                Genres = genreIds.Count > 0 ? genreIds : null,
                Artists = artistIds.Count > 0 ? artistIds : null,
                LabelNumber = (string?)music.Element("labelnumber") ?? string.Empty,
                LengthInSeconds = (string?)music.Element("lengthsecs") ?? string.Empty,
                Links = links.Count > 0 ? links : null,
                Live = isLive,
                OrigReleaseYear = origReleaseYear,
                ReleaseYear = releaseYear,
                CoverFront = GetFileNameFromPath((string?)music.Element("coverfront")),
                DateAdded = ParseDate(music.Element("dateadded")?.Element("date")?.Value),
                LastModified = ParseDate(music.Element("lastmodified")?.Element("date")?.Value),
                PackagingId = packagingId,
                PurchaseInfo = purchaseInfo,
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
            return album;
        }

        private static DateTime ParseDate(string? dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr)) return default;
            if (DateTime.TryParseExact(dateStr, "dd/MM/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out var dt))
                return dt;
            if (DateTime.TryParse(dateStr, out dt))
                return dt;
            return default;
        }

        private static string? GetFileNameFromPath(string? path)
        {
            if (string.IsNullOrEmpty(path)) return null;
            var separators = new char[] { '\\', '/' };
            var parts = path.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return parts.Length > 0 ? parts[^1] : path;
        }
    }

    // AlbumLoader as coordinator
    public class AlbumLoader
    {
        public static List<MusicAlbum> LoadAlbums(string xmlFilePath)
        {
            var albums = new List<MusicAlbum>();
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
