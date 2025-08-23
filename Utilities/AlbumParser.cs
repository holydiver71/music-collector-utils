using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Models;

namespace Utilities
{
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

        public MusicRelease ParseAlbum(XElement music, int albumId)
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
        
            var album = new MusicRelease
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
                Images = new MediaImages {
                    CoverFront = GetFileNameFromPath((string?)music.Element("coverfront")),
                    CoverBack = GetFileNameFromPath((string?)music.Element("coverback")),
                    Thumbnail = GetFileNameFromPath((string?)music.Element("thumbfilepath"))
                },
                DateAdded = ParseDate(music.Element("dateadded")?.Element("date")?.Value),
                LastModified = ParseDate(music.Element("lastmodified")?.Element("date")?.Value),
                PackagingId = packagingId,
                PurchaseInfo = purchaseInfo,
           };

            // Parse media/discs
            var mediaList = new List<MusicMedia>();
            var detailsNode = music.Element("details");
            if (detailsNode != null)
            {
                var mediaIndex = 1;
                foreach (var disc in detailsNode.Elements("detail").Where(d => (string?)d.Attribute("type") == "disc"))
                {
                    var media = new MusicMedia
                    {
                        Index = mediaIndex++,
                        Title = (string?)disc.Element("title") ?? string.Empty,
                        FormatId = album.FormatId
                    };

                    // Parse tracks for this media
                    var tracks = new List<MusicTrack>();
                    var trackDetails = disc.Element("details");
                    if (trackDetails != null)
                    {
                        foreach (var track in trackDetails.Elements("detail").Where(t => (string?)t.Attribute("type") == "track"))
                        {
                            // Extract artist names for the track
                            var trackArtistNames = new List<string>();
                            var trackArtistIds = new List<string>();
                            var trackArtistsElem = track.Element("artists");
                            if (trackArtistsElem != null)
                            {
                                foreach (var artistElem in trackArtistsElem.Elements("artist"))
                                {
                                    var artistName = (string?)artistElem.Element("displayname") ?? string.Empty;
                                    if (!string.IsNullOrWhiteSpace(artistName))
                                    {
                                        trackArtistNames.Add(artistName);
                                        var artistId = _artistService.GetArtistId(artistName);
                                        if (artistId != 0)
                                            trackArtistIds.Add(artistId.ToString());
                                    }
                                }
                            }

                            // Extract genres for the track
                            var trackGenreIds = new List<string>();
                            var trackGenresElem = track.Element("genres");
                            if (trackGenresElem != null)
                            {
                                foreach (var genreElem in trackGenresElem.Elements("genre"))
                                {
                                    var genreName = (string?)genreElem.Element("displayname") ?? string.Empty;
                                    if (!string.IsNullOrWhiteSpace(genreName))
                                    {
                                        var genreId = _genreService.GetGenreId(genreName);
                                        if (genreId != 0)
                                            trackGenreIds.Add(genreId.ToString());
                                    }
                                }
                            }

                            var musicTrack = new MusicTrack
                            {
                                Title = (string?)track.Element("title") ?? string.Empty,
                                // Parse other track fields as needed...
                                ReleaseYear = DateOnly.TryParseExact(track.Element("releasedate")?.Element("year")?.Element("displayname")?.Value,
                                    "yyyy",
                                    out var trackReleaseYear) ? trackReleaseYear : default,
                                Index = (int?)track.Element("position") ?? 0,   // Track number on the disc
                                LengthSecs = (int?)track.Element("lengthsecs") ?? 0,
                                Live = ((string?)track.Element("live")?.Attribute("boolvalue") == "1"),
                                Artists = trackArtistIds.Count > 0 ? trackArtistIds : null,
                                Genres = trackGenreIds.Count > 0 ? trackGenreIds : null
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
}
