using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DonorsChoose.WindowsPhone.Services.Network.DataTransferObjects;
using System.Collections.Generic;

namespace DonorsChoose.WindowsPhone.Services.Network.Helpers
{
    internal static class JsonDeserializer
    {
        internal static GeneralSearchResult DeserializeGeneralSearchResults(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            try
            {
                GeneralSearchResult searchResults =
                    JsonConvert.DeserializeObject<GeneralSearchResult>(json);
                return searchResults;
            }
            catch (JsonReaderException)
            {
                throw new ArgumentException("Not a valid JSON response", "json");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
