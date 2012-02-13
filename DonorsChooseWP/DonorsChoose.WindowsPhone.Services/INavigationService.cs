using System;
using System.Windows.Navigation;

namespace DonorsChoose.WindowsPhone.Services
{
    public interface INavigationService
    {
        event NavigatingCancelEventHandler Navigating;
        void NavigateTo(Uri uri);
        void GoBack();
    }
}
