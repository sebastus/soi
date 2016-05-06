using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using SOI_Api.Models;

namespace SOI_Api.Controllers
{
    public class ScheduleController : ApiController
    {
        public ApiServices Services { get; set; }

        [HttpGet]
        public HttpResponseMessage GetScheduleByGame(string gamesId, string fromDate = null, string toDate = null, string delegation = null)
        {
            var returnJson = new ScheduledEventsRootObject();
            DateTime dbFromDate = new DateTime(2015,7,24);
            DateTime dbToDate = new DateTime(2015,8,3);
            try
            {
                var provider = CultureInfo.InvariantCulture;
                if (!string.IsNullOrEmpty(fromDate)) dbFromDate = DateTime.ParseExact(fromDate, "MMddyyyy", provider);
                if (!string.IsNullOrEmpty(toDate)) dbToDate = DateTime.ParseExact(toDate, "MMddyyyy", provider);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, returnJson, new MediaTypeHeaderValue("application/json"));
            }

            using (var context = new GMSTRAININGEntities())
            {
                var dbSchedules = context.apiGetGamesSchedule(gamesId, dbFromDate, dbToDate, delegation);

                foreach (var dbSchedule in dbSchedules)
                {
                    returnJson.Add(dbSchedule);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, returnJson, new MediaTypeHeaderValue("application/json"));

        }

    }
}
