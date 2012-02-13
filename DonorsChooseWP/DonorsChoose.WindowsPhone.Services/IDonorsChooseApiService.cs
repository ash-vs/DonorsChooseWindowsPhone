using System;
using System.Collections.Generic;
using DonorsChoose.WindowsPhone.Services.Models;

namespace DonorsChoose.WindowsPhone.Services
{
    public interface IDonorsChooseApiService
    {
        void GetProjects(Action<string, Exception, Action<List<Project>, Exception>> modelCallback,
            Action<List<Project>, Exception> viewModelCallback);
    }
}
