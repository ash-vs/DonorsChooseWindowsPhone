using System;
using System.Net;
using System.Collections.Generic;
using DonorsChoose.WindowsPhone.Services.Models;
using System.IO;

namespace DonorsChoose.WindowsPhone.Services
{
    public class DonorsChooseApiService : IDonorsChooseApiService
    {
        private Random _random;
        private string _baseDonorsChooseApiUrl = "http://api.donorschoose.org/";


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
            string completeUrl = string.Format("{0}{1}{2}", _baseDonorsChooseApiUrl,
                requestUrl, randomNumberQueryStringParameter);

            HttpWebRequest request = WebRequest.Create(completeUrl) as HttpWebRequest;

            return request;
        }


        public void GetProjects(Action<string, Exception, Action<List<Project>, Exception>> modelCallback, 
            Action<List<Project>, Exception> viewModelCallback)
        {
            try
            {
                string requestUrl = string.Format("common/json_feed.html?APIKey={0}",
                        ServiceConstants.DonorsChooseApiKey);
                HttpWebRequest request = createHttpGetRequest(requestUrl);

                var state = new GetProjectsAsyncState
                {
                    ModelCallback = modelCallback,   
                    Request = request,
                    ViewModelCallback = viewModelCallback
                };

                request.BeginGetResponse(getProjectsCallback, state);
            }
            catch (Exception ex)
            {
                modelCallback(null, ex, viewModelCallback);
            }
        }


        private void getProjectsCallback(IAsyncResult asyncResult)
        {
            GetProjectsAsyncState asyncState =
                asyncResult.AsyncState as GetProjectsAsyncState;
            WebRequest request = asyncState.Request;
            Action<string, Exception, Action<List<Project>, Exception>> modelCallback = asyncState.ModelCallback;
            Action<List<Project>, Exception> viewModelCallback = asyncState.ViewModelCallback;

            try
            {
                WebResponse response = request.EndGetResponse(asyncResult);
                string jsonResult = null;
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        jsonResult = reader.ReadToEnd().Trim();
                    }
                }
                modelCallback(jsonResult, null, viewModelCallback);
            }
            catch (Exception ex)
            {
                modelCallback(null, ex, viewModelCallback);
            }
        }


        private class AsyncStateBase
        {
            public HttpWebRequest Request { get; set; }
        }


        private class GetProjectsAsyncState : AsyncStateBase
        {
            public Action<string, Exception, Action<List<Project>, Exception>> ModelCallback { get; set; }
            public Action<List<Project>, Exception> ViewModelCallback { get; set; }
        }
    }
}
