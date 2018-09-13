using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using ChallengeAccepted.Shared.Models;

namespace ChallengeAccepted.DAL.Helper
{
    public interface IApiHelper
    {
        void SetUrl(string url);
        List<Photo> Get(string parameters);
    }

    public class ApiHelper: IApiHelper
    {
        private string _url;

        public void SetUrl(string url)
        {
            _url = url;
        }

        public List<Photo> Get(string parameters)
        {
            var client = new HttpClient { BaseAddress = new Uri(_url) };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            var response = client.GetAsync(parameters).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<Photo>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll

                return dataObjects.ToList();
            }
            else
            {
                return new List<Photo>();
            }
        }
    }
}
