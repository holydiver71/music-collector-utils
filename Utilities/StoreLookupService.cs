using System;
using System.Collections.Generic;
using System.IO;

namespace Utilities
{
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
}
