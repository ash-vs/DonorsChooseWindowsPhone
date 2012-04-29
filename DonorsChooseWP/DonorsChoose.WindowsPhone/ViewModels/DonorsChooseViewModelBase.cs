using System;
using GalaSoft.MvvmLight;
using DonorsChoose.WindowsPhone.Services.Storage;
using DonorsChoose.WindowsPhone.ApplicationServices;
using DonorsChoose.WindowsPhone.Services.Network;
using DonorsChoose.WindowsPhone.Resources;
using System.Collections.Generic;

namespace DonorsChoose.WindowsPhone.ViewModels
{
    public abstract class DonorsChooseViewModelBase : ViewModelBase
    {
        #region Protected Instance Variables

        // These items don't need to be persisted 
        // in page state since they're Singletons
        // that are obtained via Inversion of Control
        protected ILocalDataService _localDataService;
        protected INavigationService _navigationService;
        protected IDonorsChooseApiService _donorsChooseApiService;

        #endregion // Protected Instance Variables


        #region Public Properties

        // This property shouldn't be persisted in 
        // page state since network requests should
        // be canceled when navigating away from a page
        #region IsProgressBarVisible Property

        public const string IsProgressBarVisiblePropertyName = "IsProgressBarVisible";

        private bool _isProgressBarVisible;

        public bool IsProgressBarVisible
        {
            get { return _isProgressBarVisible; }
            set
            {
                if (value != _isProgressBarVisible)
                {
                    _isProgressBarVisible = value;
                    RaisePropertyChanged(IsProgressBarVisiblePropertyName);
                }
            }
        }

        #endregion // IsProgressBarVisible Property


        // We don't need to save this property in page state either since
        // (1) It's usually going to be the default value defined in
        //     in the localized strings file
        // (2) The only other possibility is that it was overridden by a 
        //     subclass, and in those cases we'll leave it to the subclass
        //     to persist the property itself
        #region ProgressBarText Property

        public const string ProgressBarTextPropertyName = "ProgressBarText";

        private string _progressBarText;

        public virtual string ProgressBarText
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_progressBarText))
                {
                    return AppResources.ProgressBarText;
                }
                else
                {
                    return _progressBarText;
                }
            }
            set
            {
                if (value != _progressBarText)
                {
                    _progressBarText = value;
                    RaisePropertyChanged(ProgressBarTextPropertyName);
                }
            }
        }

        #endregion // ProgressBarText Property

        #endregion // Public Properties


        #region Progress Bar Methods

        protected internal virtual void ShowProgressBar()
        {
            IsProgressBarVisible = true;
        }

        protected internal virtual void HideProgressBar()
        {
            IsProgressBarVisible = false;
        }

        #endregion // Progress Bar Methods


        #region TombStoning Methods

        protected virtual void SaveViewModelState(IDictionary<string, object> state)
        {
            // No base class implementation needed, but we won't
            // make this abstract in order to avoid forcing an implementation
        }

        protected virtual void RestoreViewModelState(IDictionary<string, object> state)
        {
            // No base class implementation needed, but we won't
            // make this abstract in order to avoid forcing an implementation
        }

        #endregion // Tombstoning Methods


        #region Page Initialization Methods

        protected internal virtual void InitializeDataContext()
        {
            // No base class implementation needed, but we won't
            // make this abstract in order to avoid forcing an implementation
        }

        protected internal virtual void RefreshDataContext()
        {
            // No base class implementation needed, but we won't
            // make this abstract in order to avoid forcing an implementation
        }

        protected internal virtual void ResetDataContext()
        {
            // No base class implementation needed, but we won't
            // make this abstract in order to avoid forcing an implementation
        }

        #endregion // Page Initialization Methods
    }
}
