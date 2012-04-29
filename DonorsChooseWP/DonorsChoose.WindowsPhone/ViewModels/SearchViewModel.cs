using System;
using DonorsChoose.WindowsPhone.Services.Storage;
using DonorsChoose.WindowsPhone.ApplicationServices;
using DonorsChoose.WindowsPhone.Services.Network;

namespace DonorsChoose.WindowsPhone.ViewModels
{
    public class SearchViewModel : DonorsChooseViewModelBase
    {
        public SearchViewModel(IDonorsChooseApiService donorsChooseApiService,
            ILocalDataService localDataService,
            INavigationService navigationService)
        {
            _localDataService = localDataService;
            _navigationService = navigationService;
            _donorsChooseApiService = donorsChooseApiService;
        }
    }
}
