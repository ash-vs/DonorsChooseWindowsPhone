using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using DonorsChoose.WindowsPhone.ApplicationServices;
using DonorsChoose.WindowsPhone.Models;
using DonorsChoose.WindowsPhone.Services.Network;
using DonorsChoose.WindowsPhone.Services.Storage;


namespace DonorsChoose.WindowsPhone.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private IDonorsChooseApiService _donorsChooseApiService;
        private ILocalDataService _localDataService;
        private INavigationService _navigationService;


        #region Public Properties


        #region Projects Property


        public const string ProjectsPropertyName = "Projects";

        private List<Project> _projects;

        public List<Project> Projects
        {
            get { return _projects; }
            set
            {
                if (_projects != value)
                {
                    _projects = value;
                    RaisePropertyChanged(ProjectsPropertyName);
                }
            }
        }


        #endregion // Projects Property


        #endregion // Public Properties

        
        public MainViewModel(IDonorsChooseApiService donorsChooseApiService,
            ILocalDataService localDataService, INavigationService navigationService)
        {
            _donorsChooseApiService = donorsChooseApiService;
            _localDataService = localDataService;
            _navigationService = navigationService;
        }


        internal void LoadProjectList()
        {
            _donorsChooseApiService.GetProjects("Kindle", loadProjectListCallback);
        }


        private void loadProjectListCallback(List<Project> projects, Exception ex)
        {
            if (ex != null)
            {
                throw new NotImplementedException();
            }

            Projects = projects;
        }
    }
}
