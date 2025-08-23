using System;
using System.Collections.Generic;
using System.IO;

namespace Utilities
{
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
}
