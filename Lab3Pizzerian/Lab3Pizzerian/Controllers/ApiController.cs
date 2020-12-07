using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Controllers
{
    public class ApiController : Controller
    {
        [HttpGet]
        public IActionResult CreateOrder()
        {
            return Ok();
        }
    }
}
