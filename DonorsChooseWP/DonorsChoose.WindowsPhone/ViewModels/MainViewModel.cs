using System;
using System.Linq;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using DonorsChoose.WindowsPhone.ApplicationServices;
using DonorsChoose.WindowsPhone.Models;
using DonorsChoose.WindowsPhone.Services.Network;
using DonorsChoose.WindowsPhone.Services.Storage;
using DonorsChoose.WindowsPhone.Helpers;
using DonorsChoose.WindowsPhone.Services.Storage.IsolatedStorage;


namespace DonorsChoose.WindowsPhone.ViewModels
{
    public class MainViewModel : DonorsChooseViewModelBase
    {
        #region Public Properties


        #region MostUrgentProjects Property

        public const string MostUrgentProjectsPropertyName = "MostUrgentProjects";

        private List<Project> _mostUrgentProjects;

        public List<Project> MostUrgentProjects
        {
            get { return _mostUrgentProjects; }
            set
            {
                if ( value != _mostUrgentProjects)
                {
                    _mostUrgentProjects = value;
                    RaisePropertyChanged(MostUrgentProjectsPropertyName);
                }
            }
        }

        #endregion // MostUrgentProjects Property


        #region LastViewedProjects Property

        public const string LastViewedProjectsPropertyName = "LastViewedProjects";

        private List<Project> _lastViewedProjects;

        public List<Project> LastViewedProjects
        {
            get { return _lastViewedProjects; }
            set
            {
                if (value != _lastViewedProjects)
                {
                    _lastViewedProjects = value;
                    RaisePropertyChanged(LastViewedProjectsPropertyName);
                }
            }
        }

        #endregion // LastViewedProjects Property


        #endregion // Public Properties


        public MainViewModel(IDonorsChooseApiService donorsChooseApiService,
            ILocalDataService localDataService, INavigationService navigationService)
        {
            _donorsChooseApiService = donorsChooseApiService;
            _localDataService = localDataService;
            _navigationService = navigationService;
        }

        protected internal override void InitializeDataContext()
        {
            loadLastViewedProjectsList();
            loadMostUrgentProjectsList();
            
            base.InitializeDataContext();
        }


        private void loadLastViewedProjectsList()
        {
            Dictionary<string, Project> lastViewedProjectsCache = 
                AppCache.LastViewedProjects.Value;
            
            if (lastViewedProjectsCache != null)
            {
                // Order the cache by the date each time was viewed
                var orderedLastViewedProjectsCache = lastViewedProjectsCache.OrderByDescending(kvp => kvp.Value.ExpirationDate);

                // Update the local value from the ordered version of the cache
                LastViewedProjects = (from projectNameValuePair in orderedLastViewedProjectsCache
                                      select projectNameValuePair.Value).ToList<Project>();
            }
        }


        private void loadMostUrgentProjectsList()
        {
            // Call the web service asynchronously
            ShowProgressBar();
            _donorsChooseApiService.GetMostUrgentProjects(loadMostUrgentProjectsListCallback);
        }


        private void loadMostUrgentProjectsListCallback(List<Project> projects, Exception ex)
        {
            HideProgressBar();

            if (ex != null)
            {
                System.Windows.MessageBox.Show("Sorry, there was an error accessing the network",
                    "Network Error", System.Windows.MessageBoxButton.OK);
                return;
            }

            MostUrgentProjects = projects;
        }


        internal void NavigateToProjectDetailsPage(Project project)
        {
            updateLastViewedProjectsCache(project);
            Uri projectDetailsPageUri = ViewUriHelper.GetProjectDetailsPageUri(project.Id);
            _navigationService.NavigateTo(projectDetailsPageUri);
        }


        private void updateLastViewedProjectsCache(Project selectedProject)
        {
            Dictionary<string, Project> lastViewedProjectsCache = 
                AppCache.LastViewedProjects.Value;
            
            // Lazily create and store the Last Viewed projects cache
            if (lastViewedProjectsCache == null)
            {
                lastViewedProjectsCache = new Dictionary<string, Project>();
                AppCache.LastViewedProjects.Value = lastViewedProjectsCache;
            }

            if (lastViewedProjectsCache.ContainsKey(selectedProject.Id.ToString()))
            {
                lastViewedProjectsCache[selectedProject.Id.ToString()] = selectedProject;
            }
            else
            {
                lastViewedProjectsCache.Add(selectedProject.Id.ToString(), selectedProject);
            }
        }


        internal void NavigateToSearchPage()
        {
            Uri searchPageUri = ViewUriHelper.GetSearchPageUri();
            _navigationService.NavigateTo(searchPageUri);
        }
    }
}
