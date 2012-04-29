using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using DonorsChoose.WindowsPhone.ViewModels;
using DonorsChoose.WindowsPhone.Helpers;
using DonorsChoose.WindowsPhone.Resources;
using DonorsChoose.WindowsPhone.Models;

namespace DonorsChoose.WindowsPhone.Views
{
    public partial class MainPage : DonorsChoosePageBase
    {
        private MainViewModel ViewModel
        {
            get { return DataContext as MainViewModel; }
        }


        public MainPage()
        {
            InitializeComponent();
        }


        protected override void InitializeAppBar()
        {
            // Create the app bar
            ApplicationBar = ApplicationBarHelper.CreateApplicationBar(false, true);

            // Create a localized "Search" button
            var appBarSearchButton = ApplicationBarHelper.CreateApplicationBarButton(ApplicationBar,
                "/Images/ApplicationBar/appbar.feature.search.rest.png",
                AppResources.ApplicationBarSearchButtonText,
                true);

            // Subscribe to button click events
            appBarSearchButton.Click += appBarSearchButton_Click;
            
            base.InitializeAppBar();
        }


        private void appBarSearchButton_Click(object sender, EventArgs e)
        {
            ViewModel.NavigateToSearchPage();
        }


        private void MostUrgentProjectsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ignore cases where the SelectionChanged event 
            // is raised because the SelectedItem is set to null
            if (MostUrgentProjectsListBox.SelectedItem == null)
            {
                return;
            }

            // Navigate to the project details page for the selected project
            Project selectedProject = MostUrgentProjectsListBox.SelectedItem as Project;
            ViewModel.NavigateToProjectDetailsPage(selectedProject);

            // Reset the ListBox so that the user can re-select the same
            // item if/when they navigate back to this page
            MostUrgentProjectsListBox.SelectedItem = null;
        }


        private void LastViewedProjectsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ignore cases where the SelectionChanged event 
            // is raised because the SelectedItem is set to null
            if (LastViewedProjectsListBox.SelectedItem == null)
            {
                return;
            }

            // Navigate to the project details page for the selected project
            Project selectedProject = LastViewedProjectsListBox.SelectedItem as Project;
            ViewModel.NavigateToProjectDetailsPage(selectedProject);

            // Reset the ListBox so that the user can re-select the same
            // item if/when they navigate back to this page
            LastViewedProjectsListBox.SelectedItem = null;
        }
    }
}