using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;


namespace DonorsChoose.WindowsPhone.Services.Models
{
    public class Project
    {
        private static IDonorsChooseApiService _donorsChooseApiService;


        public string TestData { get; set; }



        static Project()
        {
            _donorsChooseApiService = SimpleIoc.Default.GetInstance<IDonorsChooseApiService>();
        }


        public static void LoadProjects(Action<List<Project>, Exception> viewModelCallback)
        {
            _donorsChooseApiService.GetProjects(loadProjectsCallback, viewModelCallback);
        }


        private static void loadProjectsCallback(string jsonResult, Exception ex,
            Action<List<Project>, Exception> viewModelCallback)
        {
            if (ex != null)
            {
                throw new NotImplementedException();
            }

            List<Project> projects = new List<Project>()
            {
                new Project { TestData = jsonResult }
            };

            DispatcherHelper.CheckBeginInvokeOnUI(() => viewModelCallback(projects, null));
        }
    }
}
