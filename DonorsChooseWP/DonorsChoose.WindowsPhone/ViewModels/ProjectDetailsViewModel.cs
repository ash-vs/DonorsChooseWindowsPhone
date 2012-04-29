using System;
using DonorsChoose.WindowsPhone.Services.Storage;
using DonorsChoose.WindowsPhone.ApplicationServices;

namespace DonorsChoose.WindowsPhone.ViewModels
{
    public class ProjectDetailsViewModel : DonorsChooseViewModelBase
    {
        public ProjectDetailsViewModel(ILocalDataService localDataService,
            INavigationService navigationService)
        {
            _localDataService = localDataService;
            _navigationService = navigationService;
        }
    }
}
