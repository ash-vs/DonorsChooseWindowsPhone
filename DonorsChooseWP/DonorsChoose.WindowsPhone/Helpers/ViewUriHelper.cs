using System;

namespace DonorsChoose.WindowsPhone.Helpers
{
    public static class ViewUriHelper
    {
        #region Query String Parameter Names

        public const string ProjectIdQueryStringParameter = "ProjectId";

        #endregion // Query String Parameter Names


        #region Project Details Page Methods

        private const string _projectDetailsPageUrl = "/Views/ProjectDetailsPage.xaml";

        public static Uri GetProjectDetailsPageUri(int projectId)
        {
            string completeProjectDetailsPageUrl = string.Format("{0}?{1}={2}",
                _projectDetailsPageUrl, ProjectIdQueryStringParameter, projectId);
            return new Uri(completeProjectDetailsPageUrl, UriKind.Relative);
        }

        #endregion // Project Details Page Methods


        #region Search Page Methods

        private const string _searchPageUrl = "/Views/SearchPage.xaml";

        public static Uri GetSearchPageUri()
        {
            return new Uri(_searchPageUrl, UriKind.Relative);
        }

        #endregion // Search Page Methods
    }
}
