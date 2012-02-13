using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using DonorsChoose.WindowsPhone.Services;

namespace DonorsChoose.WindowsPhone.ViewModels
{
    public static class ViewModelLocator
    {
        static ViewModelLocator()
        {
            registerServices();
        }


        private static void registerServices()
        {
            //
            // If there's ever a need to register "mock" services for
            // design-related purposes, then here's where we'd do it
            //
            //if (ViewModelBase.IsInDesignModeStatic)
            //{
            //}
            //else
            //{
            //}

            SimpleIoc.Default.Register<IDonorsChooseApiService, DonorsChooseApiService>();
            SimpleIoc.Default.Register<ILocalDataService, LocalDataService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
        }


        #region Private Service Properties


        private static IDonorsChooseApiService DonorsChooseApiService
        {
            get 
            { 
                return SimpleIoc.Default.GetInstance<IDonorsChooseApiService>(); 
            }
        }


        private static ILocalDataService LocalDataService
        {
            get
            {
                return SimpleIoc.Default.GetInstance<ILocalDataService>();
            }
        }


        private static INavigationService NavigationService
        {
            get
            {
                return SimpleIoc.Default.GetInstance<INavigationService>();
            }
        }


        #endregion // Public Service Properties


        #region Public ViewModel Properties

        public static MainViewModel Main
        {
            get
            {
                return new MainViewModel(NavigationService);
            }
        }


        #endregion // Public ViewModel Properties

    }
}
