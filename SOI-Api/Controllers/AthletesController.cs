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
    public static class ExtensionMethods
    {
        // returns the number of milliseconds since Jan 1, 1970 (useful for converting C# dates to JS dates)
        public static double UnixTicks(this DateTime dt)
        {
            DateTime d1 = new DateTime(1970, 1, 1);
            DateTime d2 = dt.ToUniversalTime();
            TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);
            return ts.TotalMilliseconds;
        }

        public static string ISO8601z(this DateTime dt)
        {
            return dt.ToString("s") + dt.ToString("zzz");
        }
    }

    public class AthletesController : ApiController
    {
        public ApiServices Services { get; set; }
        
        [HttpGet]
        [ActionName("Athletes")]
        public HttpResponseMessage GetAthletesByBibNumber(string bibNumber)
        {
            var returnJson = new AthletesByIdRootObject();
            using (var context = new GMSTRAININGEntities())
            {                
                var dbAthletes = context.apiSearchAthleteByID(bibNumber, null, null, null, null);

                foreach (var dbAthlete in dbAthletes)
                {
                    returnJson.Add(dbAthlete);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, returnJson, new MediaTypeHeaderValue("application/json"));

        }

        [HttpGet]
        [ActionName("Athletes")]
        public HttpResponseMessage GetAthletesByName(string name)
        {
            var returnJson = new AthletesByIdRootObject();
            using (var context = new GMSTRAININGEntities())
            {
                var dbAthletes = context.apiSearchAthleteByID(null, name, null, null, null);

                foreach (var dbAthlete in dbAthletes)
                {
                    returnJson.Add(dbAthlete);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, returnJson, new MediaTypeHeaderValue("application/json"));

        }

        [HttpGet]
        [ActionName("Athletes")]
        public HttpResponseMessage GetAthletesByDelegation(string gamesId, string delegation, string group = null)
        {
            var returnJson = new AthletesByIdRootObject();
            using (var context = new GMSTRAININGEntities())
            {
                var dbAthletes = context.apiSearchAthleteByID(null, null, gamesId, delegation, group);

                foreach (var dbAthlete in dbAthletes)
                {
                    returnJson.Add(dbAthlete);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, returnJson, new MediaTypeHeaderValue("application/json"));

        }
    }
}
