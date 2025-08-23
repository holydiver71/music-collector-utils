using System;
using System.Collections.Generic;
using System.IO;

namespace Utilities
{
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
}
