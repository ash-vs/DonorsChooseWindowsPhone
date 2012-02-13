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

namespace DonorsChoose.WindowsPhone.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        private MainViewModel ViewModel
        {
            get { return DataContext as MainViewModel; }
            set
            {
                DataContext = value;
            }
        }


        public MainPage()
        {
            InitializeComponent();

            ViewModel = ViewModelLocator.Main;

            // Quick test
            ViewModel.LoadProjectList();
        }

    }
}