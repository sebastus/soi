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
    public class EventsController : ApiController
    {
        public ApiServices Services { get; set; }

        [HttpGet]
        [ActionName("Events")]
        public HttpResponseMessage GetEventsByGameId(string id)
        {
            var returnJson = new EventByGameIdRootObject();
            
            using (var context = new GMSTRAININGEntities())
            {
                var dbSportingEvents = context.apiGetEventsListByGameID(id);

                foreach (var dbSportingEvent in dbSportingEvents)
                {
                    returnJson.Add(dbSportingEvent);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, returnJson, new MediaTypeHeaderValue("application/json"));

        }


        [HttpGet]
        [ActionName("Events")]
        public HttpResponseMessage GetEventsWithAthletes(string gamesId, string eventIdOrCode)
        {
            var returnJson = new AthletesSearchByEventRootObject();

            using (var context = new GMSTRAININGEntities())
            {
                var dbAthletes = context.apiSearchAthleteByEvent(eventIdOrCode, gamesId);

                foreach (var dbAthlete in dbAthletes)
                {
                    returnJson.Add(dbAthlete);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, returnJson, new MediaTypeHeaderValue("application/json"));

        }

    }
}
