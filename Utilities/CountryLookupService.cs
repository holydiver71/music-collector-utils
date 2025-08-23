using System;
using System.Collections.Generic;
using System.IO;

namespace Utilities
{
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
}
