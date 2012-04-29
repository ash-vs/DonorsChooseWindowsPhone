using System;
using DonorsChoose.WindowsPhone.Services.Storage;
using DonorsChoose.WindowsPhone.ApplicationServices;
using DonorsChoose.WindowsPhone.Services.Storage.IsolatedStorage;
using DonorsChoose.WindowsPhone.Models;

namespace DonorsChoose.WindowsPhone.ViewModels
{
    public class ProjectDetailsViewModel : DonorsChooseViewModelBase
    {
        internal string _projectId;


        #region SelectedProject Property

        public const string SelectedProjectPropertyName = "SelectedProject";

        private Project _selectedProject;

        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                if (value != _selectedProject)
                {
                    _selectedProject = value;
                    RaisePropertyChanged(SelectedProjectPropertyName);
                }
            }
        }

        #endregion // SelectedProject Property


        public ProjectDetailsViewModel(ILocalDataService localDataService,
            INavigationService navigationService)
        {
            _localDataService = localDataService;
            _navigationService = navigationService;
        }


        protected internal override void InitializeDataContext()
        {
            if (!string.IsNullOrWhiteSpace(_projectId))
            {
                if (AppCache.LastViewedProjects.Value.ContainsKey(_projectId))
                {
                    SelectedProject = AppCache.LastViewedProjects.Value[_projectId];
                }
            }

            base.InitializeDataContext();
        }
    }
}
