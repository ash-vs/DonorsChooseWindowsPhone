using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using DonorsChoose.WindowsPhone.ApplicationServices;
using DonorsChoose.WindowsPhone.Models;
using DonorsChoose.WindowsPhone.Services.Network;
using DonorsChoose.WindowsPhone.Services.Storage;


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
                if (_mostUrgentProjects != value)
                {
                    _mostUrgentProjects = value;
                    RaisePropertyChanged(MostUrgentProjectsPropertyName);
                }
            }
        }

        #endregion // MostUrgentProjects Property


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
            // TBA - Retrieve from storage
        }


        private void loadMostUrgentProjectsList()
        {
            // Call the web service asynchronously
            ShowProgressBar();
            _donorsChooseApiService.GetProjects(null, loadMostUrgentProjectsListCallback);
        }


        private void loadMostUrgentProjectsListCallback(List<Project> projects, Exception ex)
        {
            HideProgressBar();

            if (ex != null)
            {
                throw new NotImplementedException();
            }

            MostUrgentProjects = projects;
        }
    }
}
