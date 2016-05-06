using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using SOI_Api.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace APILoadTest
{
    [TestClass]
    public class AthletesControllerUnitTest
    {
        [TestMethod]
        public async Task TestGetAthletesByAthleteId_Name()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://soi-api-westus.azure-mobile.net/");
                client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", "aDBWnfLpEFchpHwCmQcMVHBUyPpEwB57");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/athletes/?Name=David");
                Assert.IsTrue(response.IsSuccessStatusCode);
                if (response.IsSuccessStatusCode)
                {
                    Dictionary<string, object> root = await response.Content.ReadAsAsync<Dictionary<string,object>>();
                }
            }
        }

        [TestMethod]
        public async Task TestGetAthletesByAthleteId_Bib()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://soi-api-westus.azure-mobile.net/");
                client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", "aDBWnfLpEFchpHwCmQcMVHBUyPpEwB57");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/athletes/?BibNumber=154");
                Assert.IsTrue(response.IsSuccessStatusCode);
                if (response.IsSuccessStatusCode)
                {
                    Dictionary<string, object> root = await response.Content.ReadAsAsync<Dictionary<string, object>>();
                }
            }
        }

        [TestMethod]
        public async Task TestGetAthletesByDelegation()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://soi-api-westus.azure-mobile.net/");
                client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", "aDBWnfLpEFchpHwCmQcMVHBUyPpEwB57");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Athletes?gamesId=RPTUG15U59PPDF64&delegation=AF.SO%20Botswana");
                Assert.IsTrue(response.IsSuccessStatusCode);
                if (response.IsSuccessStatusCode)
                {
                    Dictionary<string, object> root = await response.Content.ReadAsAsync<Dictionary<string, object>>();
                }
            }
        }


    }
}
