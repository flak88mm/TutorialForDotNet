using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tutorial.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class AboutController : Controller
    {
        public string Me()
        {
            return "Dave";
        }

        public string Company()
        {
            return "No Company";
        }
    }
}
