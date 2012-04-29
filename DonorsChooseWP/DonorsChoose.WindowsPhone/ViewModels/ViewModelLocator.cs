using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using DonorsChoose.WindowsPhone.ApplicationServices;
using DonorsChoose.WindowsPhone.Services.Storage;
using DonorsChoose.WindowsPhone.Services.Network;

namespace DonorsChoose.WindowsPhone.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            registerServices();
        }


        private static void registerServices()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                //If there's ever a need to register "mock" services for
                //design-related purposes, then here's where we'd do it

                SimpleIoc.Default.Register<IDonorsChooseApiService, DonorsChooseApiService>();
                SimpleIoc.Default.Register<ILocalDataService, LocalDataService>();
                SimpleIoc.Default.Register<INavigationService, NavigationService>();

            }
            else
            {
                SimpleIoc.Default.Register<IDonorsChooseApiService, DonorsChooseApiService>();
                SimpleIoc.Default.Register<ILocalDataService, LocalDataService>();
                SimpleIoc.Default.Register<INavigationService, NavigationService>();
            }

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
                return new MainViewModel(DonorsChooseApiService, 
                    LocalDataService, NavigationService);
            }
        }

        public static ProjectDetailsViewModel ProjectDetails
        {
            get
            {
                return new ProjectDetailsViewModel(LocalDataService, 
                    NavigationService);
            }
        }

        public static SearchViewModel Search
        {
            get
            {
                return new SearchViewModel(DonorsChooseApiService,
                    LocalDataService, NavigationService);
            }
        }


        #endregion // Public ViewModel Properties

    }
}
