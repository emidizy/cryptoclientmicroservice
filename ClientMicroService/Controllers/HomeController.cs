﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TransactionMicroService.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Produces("text/html")]
        [HttpGet]
        public ContentResult Index()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = "<html><body>" +
                "<h1 style=\"text-align: center;margin-top: 50px;\">Hello, Crypto Client Service</h1><p/>" +
                "<p></p>" +
                "<p></p>" +
                "<h4 style=\"text-align: center;\">Status:  Running</h4>" +
                "<h5 style=\"text-align: center;\">Docker support:  Enabled</h5>" +
                "</body></html>"
            };
        }
    }
}
