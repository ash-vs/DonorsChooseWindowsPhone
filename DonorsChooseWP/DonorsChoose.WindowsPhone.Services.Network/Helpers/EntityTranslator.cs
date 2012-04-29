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

            GeneralSearchResult searchResults = JsonDeserializer.DeserializeGeneralSearchResults(json);
            return convertGeneralSearchResultsToProjects(searchResults);
        }


        private static List<Project> convertGeneralSearchResultsToProjects(GeneralSearchResult searchResults)
        {
            if (searchResults == null
                || searchResults.Proposals == null
                || searchResults.Proposals.Length == 0)
            {
                return null;
            }

            List<Project> projects = new List<Project>();

            foreach (var proposal in searchResults.Proposals)
            {
                // Create the new project from a proposal
                var project = new Project();
                project.Id = proposal.Id;
                project.ProposalUrl = proposal.ProposalUrl;
                project.FundUrl = proposal.FundUrl;
                project.ImageUrl = proposal.ImageUrl;
                project.Title = proposal.Title;
                project.ShortDescription = proposal.ShortDescription;
                project.FulfillmentTrailer = proposal.FulfillmentTrailer;
                project.PercentFunded = proposal.PercentFunded;
                project.CostToComplete = proposal.CostToComplete;
                project.TotalPrice = proposal.TotalPrice;
                project.TeacherName = proposal.TeacherName;
                project.GradeLevelName = (proposal.GradeLevel != null) 
                                         ? proposal.GradeLevel.Name
                                         : "Not Specified";
                project.PovertyLevel = proposal.PovertyLevel;
                project.SchoolName = proposal.SchoolName;
                project.City = proposal.City;
                project.Zip = proposal.Zip;
                project.State = proposal.State;
                project.Latitude = proposal.Latitude;
                project.Longitude = proposal.Longitude;
                project.SubjectName = (proposal.Subject != null) 
                                      ? proposal.Subject.Name
                                      : "Not Specified";
                project.ResourceName = (proposal.Resource != null)
                                       ? proposal.Resource.Name
                                       : "Not Specified";
                project.ExpirationDate = proposal.ExpirationDate;
                project.FundingStatus = proposal.FundingStatus;

                // Add the project to the collection
                projects.Add(project);
            }

            return projects;
        }
    }
}
