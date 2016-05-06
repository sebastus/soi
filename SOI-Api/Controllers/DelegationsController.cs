using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.WindowsAzure.Mobile.Service;
using Newtonsoft.Json;
using SOI_Api.Models;

namespace SOI_Api.Controllers
{
    public class DelegationsController : ApiController
    {
        public ApiServices Services { get; set; }

        [HttpGet]
        [ActionName("Delegations")]
        public HttpResponseMessage GetDelegationsByGameId(string id)
        {
            var returnJson = new DelegationByGameIdRootObject();            
            using (var context = new GMSTRAININGEntities())
            {
                var dbDelegations = context.apiGetDelegationsListByGameID(id);

                foreach (var dbDelegation in dbDelegations)
                {
                    returnJson.Add(dbDelegation);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, returnJson,new MediaTypeHeaderValue("application/json"));

        }
    }
}
