using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using DonorsChoose.WindowsPhone.Services;
using DonorsChoose.WindowsPhone.Services.Models;


namespace DonorsChoose.WindowsPhone.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
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


        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        internal void LoadProjectList()
        {
            Project.LoadProjects(loadProjectListCallback);
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
