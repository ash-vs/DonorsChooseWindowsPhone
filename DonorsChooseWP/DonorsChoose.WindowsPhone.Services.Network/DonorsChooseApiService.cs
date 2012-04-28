using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
using DonorsChoose.WindowsPhone.Models;
using DonorsChoose.WindowsPhone.Services.Network.DataTransferObjects;
using DonorsChoose.WindowsPhone.Services.Network.Helpers;

namespace DonorsChoose.WindowsPhone.Services.Network
{
    public class DonorsChooseApiService : IDonorsChooseApiService
    {
        private Random _random;
        

        public DonorsChooseApiService()
        {
            _random = new Random(DateTime.Now.Millisecond);
        }


        private HttpWebRequest createHttpGetRequest(string requestUrl)
        {
            // Append a random number query string parameter to
            // each GET request to evade client-side caching
            int randomNumber = _random.Next();
            string randomNumberQueryStringParameter = "&someRandomNumber=" + randomNumber;

            // Assemble the entire URL for the request
            string completeUrl = string.Format("{0}{1}{2}", Constants.BaseDonorsChooseApiUrl,
                requestUrl, randomNumberQueryStringParameter);

            HttpWebRequest request = WebRequest.Create(completeUrl) as HttpWebRequest;

            return request;
        }


        public void GetProjects(string searchTerms, Action<List<Project>, Exception> viewModelCallback)
        {
            try
            {
                string requestUrl = string.Format("common/json_feed.html?APIKey={0}",
                        Constants.DonorsChooseApiKey);
                HttpWebRequest request = createHttpGetRequest(requestUrl);

                var state = new GetProjectsAsyncState
                {
                    Request = request,
                    ViewModelCallback = viewModelCallback
                };

                request.BeginGetResponse(getProjectsCallback, state);
            }
            catch (Exception ex)
            {
                viewModelCallback(null, ex);
            }
        }


        private void getProjectsCallback(IAsyncResult asyncResult)
        {
            GetProjectsAsyncState asyncState =
                asyncResult.AsyncState as GetProjectsAsyncState;
            WebRequest request = asyncState.Request;
            Action<List<Project>, Exception> viewModelCallback = asyncState.ViewModelCallback;

            try
            {
                WebResponse response = request.EndGetResponse(asyncResult);
                List<Project> projects = null;
                string json = null;
                
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        json = reader.ReadToEnd().Trim();
                    }
                }

                projects = EntityTranslator.TranslateGenericSearchResultsToProjects(json);
                viewModelCallback(projects, null);
            }
            catch (Exception ex)
            {
                viewModelCallback(null, ex);
            }
        }


        private class AsyncStateBase
        {
            public HttpWebRequest Request { get; set; }
        }


        private class GetProjectsAsyncState : AsyncStateBase
        {
            public Action<List<Project>, Exception> ViewModelCallback { get; set; }
        }
    }
}
