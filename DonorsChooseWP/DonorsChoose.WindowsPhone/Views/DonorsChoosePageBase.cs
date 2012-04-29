using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using DonorsChoose.WindowsPhone.ViewModels;

namespace DonorsChoose.WindowsPhone.Views
{
    public abstract class DonorsChoosePageBase : PhoneApplicationPage
    {
        /// <summary>
        /// Set within the OnNavigatedTo event handler so that we
        /// can utilize that information in other parts of the page's
        /// life cycle when this information is not readily available
        /// </summary>
        private bool _appNavigatedBackwardToThisPage;

        /// <summary>
        /// Indicates that the page was just navigated to for the
        /// first time, and not after having been tombstonined
        /// </summary>
        private bool _pageNeedsToBeInitialized;
        
        /// <summary>
        /// Indicates that the page's state neeeds to be restored
        /// since everything else about the object was disposed of
        /// automatically by the operating system when tomstoning
        /// </summary>
        private bool _pageNeedsToBeRestored;


        private DonorsChooseViewModelBase ViewModel
        {
            get { return DataContext as DonorsChooseViewModelBase; }
        }


        public DonorsChoosePageBase()
        {
            // Subscribe a handler to the page's Loaded event in the constructor,
            // and then unsubscribe the hander from this event when navigating 
            // away from the page so that it doesn't fire during backward navigation
            // (unless the page has been tombstoned)
            Loaded += OnPageLoaded;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode != NavigationMode.Back)
            {
                // If we aren't navigating backward, then the page
                // is being created for the first time, and it simply
                // needs to be initialized

                _pageNeedsToBeInitialized = true;
                _pageNeedsToBeRestored = false;

                // Reset the flag to record that we didn't just 
                // navigate backward to this page
                _appNavigatedBackwardToThisPage = false;
            }
            else if (e.NavigationMode == NavigationMode.Back
                     && App.WasTombstoned)
            {
                // If we are navigating backward and the app was
                // tombstoned by the operating system, then we do
                // *NOT* need to initialize the page object, but 
                // we do need to restore its persisted state
                _pageNeedsToBeInitialized = false;
                _pageNeedsToBeRestored = true;

                // Set the flag to record that we just navigated 
                // backward to this page
                _appNavigatedBackwardToThisPage = true;
            }
            else if (e.NavigationMode == NavigationMode.Back
                     && !App.WasTombstoned)
            {
                // If we are navigating backward to this page, but
                // the app was *NOT* tombstoned by the operating
                // system, then everything is still in memory, so
                // we don't need to initialize or restore the page
                _pageNeedsToBeInitialized = false;
                _pageNeedsToBeRestored = false;

                // Set the flag to record that we just navigated 
                // backward to this page
                _appNavigatedBackwardToThisPage = true;
            }


            base.OnNavigatedTo(e);
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Unsubscribe the associated event handler from the page's
            // Loaded event whenever navigating away from the page (see
            // comments in contructor and event handler for more details)
            Loaded -= OnPageLoaded;

            // Store the page's state unless we're disposing 
            // of the page object by navigating backward 
            if (e.NavigationMode != NavigationMode.Back)
            {
                StorePageState();
            }

            base.OnNavigatedFrom(e);
        }


        protected virtual void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            // This event handler will only execute when:
            //   (1) The page object is first instantiated
            //   (2) The page object is re-instantiated after 
            //       the app has been tombstoned


            // Parse query string data prior to performing any other
            // actions, including the initialization of the app bar
            // (since the presence and/or state of app bar buttons like 
            //  the "pin" button might depend on the query string data)
            ParseQueryStringParameters();

            
            // Initialize the App Bar regardless of whether or not
            // the page is being initialized or restored
            InitializeAppBar();


            if (_pageNeedsToBeInitialized)
            {
                InitializePage();
            }

            if (_pageNeedsToBeRestored)
            {
                RestorePageStateOfBindableControls();
            }

            if (_appNavigatedBackwardToThisPage)
            {
                // Allow each page's ViewModel to optionally refresh 
                // its data at this point
                ViewModel.RefreshDataContext();
            }

            // Restore properties that are non-bindable 
            // (e.g., a localized App Bar)
            RestorePageStateOfNonBindableControlProperties();
        }


        protected virtual void ParseQueryStringParameters()
        {
            // No base class implementation
        }

        protected virtual void InitializeAppBar()
        {
            // No base class implementation
        }

        protected virtual void InitializePage()
        {
            // Call the corresponding ViewModel's implementation
            // of the InitializeDataContext() method to initialize
            // the page itself
            ViewModel.InitializeDataContext();
        }

        protected virtual void StorePageState()
        {
            // Call the corresponding ViewModel object's 
            // method and pass in the Page's state dictionary
            ViewModel.SaveViewModelState(this.State);
        }

        protected virtual void RestorePageStateOfBindableControls()
        {
            // Call the corresponding ViewModel object's 
            // method and pass in the Page's state dictionary
            ViewModel.RestoreViewModelState(this.State);
        }

        protected virtual void RestorePageStateOfNonBindableControlProperties()
        {
            // No base class implementation
        }
    }
}
