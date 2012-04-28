using System;
using System.Collections.Generic;
using DonorsChoose.WindowsPhone.Models;
using DonorsChoose.WindowsPhone.Services.Network.DataTransferObjects;

namespace DonorsChoose.WindowsPhone.Services.Network
{
    public interface IDonorsChooseApiService
    {
        void GetProjects(string searchTerms, Action<List<Project>, Exception> viewModelCallback);
    }
}
