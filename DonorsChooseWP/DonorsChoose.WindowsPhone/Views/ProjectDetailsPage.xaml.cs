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
using DonorsChoose.WindowsPhone.Helpers;
using DonorsChoose.WindowsPhone.ViewModels;

namespace DonorsChoose.WindowsPhone.Views
{
    public partial class ProjectDetailsPage : DonorsChoosePageBase 
    {
        public ProjectDetailsViewModel ViewModel
        {
            get { return DataContext as ProjectDetailsViewModel; }
        }
        
        public ProjectDetailsPage()
        {
            InitializeComponent();
        }


        protected override void ParseQueryStringParameters()
        {
            // Retrieve the collection of query string parameters
            IDictionary<string, string> queryStrings = NavigationContext.QueryString;
            if (queryStrings != null)
            {
                // Try to retreive the Project Id
                if (queryStrings.ContainsKey(ViewUriHelper.ProjectIdQueryStringParameter))
                {
                    ViewModel._projectId = queryStrings[ViewUriHelper.ProjectIdQueryStringParameter];
                }
            }


            base.ParseQueryStringParameters();
        }
    }
}