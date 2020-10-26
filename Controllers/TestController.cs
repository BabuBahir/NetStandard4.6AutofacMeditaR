using Sample4._6Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Sample4._6Api.Controllers
{
    public class TestController : ApiController
    {
        private readonly ILogger _logger;

        public TestController(ILogger logger)
        {
            this._logger = logger;
        }

        public string Get()
        {
            this._logger.Write("Inside the 'Get' method of the '{0}' controller.", GetType().Name);

            return "Hello, world!";
        }
    }
}