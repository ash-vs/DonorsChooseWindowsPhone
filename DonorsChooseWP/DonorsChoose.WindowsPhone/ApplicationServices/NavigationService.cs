using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows;

namespace DonorsChoose.WindowsPhone.ApplicationServices
{
    public class NavigationService : INavigationService
    {
        private PhoneApplicationFrame _mainPhoneAppFrame;


        public event NavigatingCancelEventHandler Navigating;


        public void NavigateTo(Uri uri)
        {
            if (mainPhoneAppFrameIsInitialized())
            {
                _mainPhoneAppFrame.Navigate(uri);
            }
        }


        public void GoBack()
        {
            if (mainPhoneAppFrameIsInitialized()
                && _mainPhoneAppFrame.CanGoBack)
            {
                _mainPhoneAppFrame.GoBack();
            }
        }


        private bool mainPhoneAppFrameIsInitialized()
        {
            if (_mainPhoneAppFrame != null)
            {
                return true;
            }
            else
            {
                // Lazily attempt to initialize the main phone app frame field
                _mainPhoneAppFrame = Application.Current.RootVisual as PhoneApplicationFrame;

                if (_mainPhoneAppFrame != null)
                {
                    // The first time we initialize the main phone app  
                    // frame field we also attach an event handler to
                    // the field's Navigating event so that consumers
                    // of this class will be able to subscribe to
                    // that event indirectly
                    _mainPhoneAppFrame.Navigating += (s, e) =>
                    {
                        if (Navigating != null)
                        {
                            Navigating(s, e);
                        }
                    };

                    return true;
                }
                else
                {
                    // The main phone app frame could still be null if the
                    // app is running inside of a design tool
                    return false;
                }
            }
        }
    }
}
