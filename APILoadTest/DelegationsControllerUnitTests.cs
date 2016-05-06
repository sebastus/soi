using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using SOI_Api.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace APILoadTest
{
    [TestClass]
    public class DelegationsControllerUnitTests
    {
        [TestMethod]
        public async Task TestGetDelegationsByGameId()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://soi-api-westus.azure-mobile.net/");
                client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", "aDBWnfLpEFchpHwCmQcMVHBUyPpEwB57");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/delegations/RPTUG15U59PPDF64");
                Assert.IsTrue(response.IsSuccessStatusCode);
                if (response.IsSuccessStatusCode)
                {
                    Dictionary<string, object> root = await response.Content.ReadAsAsync<Dictionary<string, object>>();
                }
            }
        }
    }
}
