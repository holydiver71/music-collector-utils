using System;
using System.Collections.Generic;
using System.IO;

namespace Utilities
{
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
}
