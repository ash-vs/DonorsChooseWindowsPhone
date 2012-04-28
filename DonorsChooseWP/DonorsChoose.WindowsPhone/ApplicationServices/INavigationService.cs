using System;
using System.Windows.Navigation;

namespace DonorsChoose.WindowsPhone.ApplicationServices
{
    public interface INavigationService
    {
        event NavigatingCancelEventHandler Navigating;
        void NavigateTo(Uri uri);
        void GoBack();
    }
}
