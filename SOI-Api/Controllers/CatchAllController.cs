using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;

namespace SOI_Api.Controllers
{
    public class NoMatch
    {
        public string Message { get; set; }
    }

    public class CatchAllController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/CatchAll
        public NoMatch Get()
        {
            var m = new NoMatch {Message = "No results at that address."};
            return m;
        }
    }
}
