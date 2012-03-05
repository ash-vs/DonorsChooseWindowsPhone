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

namespace DonorsChoose.WindowsPhone.Views
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);


            // If we navigated to this page from the MainPage, the DefaultTitle parameter will be "FromMain".
            // If we naviaged here when the toast notice was tapped, the parameter will be "From Toast Notification".
            textBlockFrom.Text = "Navigated here from " + this.NavigationContext.QueryString["NavigatedFrom"];
        }
    }
}