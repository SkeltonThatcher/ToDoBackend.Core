using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ToDoBackend.Core.Controllers
{
    [Route("api/[controller]")]
    public class HealthCheckController : Controller
    {
                // GET api/values
        [HttpGet]
        public string Get()
        {
            return "OK";
        }
    }
}