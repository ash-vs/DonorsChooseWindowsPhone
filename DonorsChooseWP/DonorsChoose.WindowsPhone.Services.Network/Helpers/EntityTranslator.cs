using System;
using System.Collections.Generic;
using DonorsChoose.WindowsPhone.Models;
using DonorsChoose.WindowsPhone.Services.Network.DataTransferObjects;

namespace DonorsChoose.WindowsPhone.Services.Network.Helpers
{
    internal static class EntityTranslator
    {
        internal static List<Project> TranslateGenericSearchResultsToProjects(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            List<GeneralSearchResult> searchResults = JsonDeserializer.DeserializeGeneralSearchResults(json);
            return convertGeneralSearchResultsToProjects(searchResults);
        }


        private static List<Project> convertGeneralSearchResultsToProjects(List<GeneralSearchResult> searchResults)
        {
            return null;
        }
    }
}
