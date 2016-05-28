using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlaySimple.Controllers
{
    public class ReportsController : ApiController
    {
        [HttpGet]
        [Authorize(Roles = Consts.Roles.Admin + "," + Consts.Roles.Employee)]
        public OffendingUsersReport ReportedUsersReport()
        {
            return null;
        }

        [HttpGet]
        [Authorize(Roles = Consts.Roles.Admin + "," + Consts.Roles.Employee)]
        public UserActivityReport UserActivityReport()
        {
            return null;
        }

        [HttpGet]
        [Authorize(Roles = Consts.Roles.Employee)]
        public UserActivityReport FieldsReport()
        {
            return null;
        }
    }
}
