using Domain;
using NHibernate;
using PlaySimple.DTOs;
using PlaySimple.QueryProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlaySimple.Controllers
{
    public class LoginController : ApiController
    {
        ISession _session;

        public LoginController(ISession session)
        {
            _session = session;
        }

        [HttpPost]
        public string Login(UserCredentials credentials)
        {
            var user = _session.QueryOver<Domain.Customer>().Where(x => x.Username == credentials.Username && x.Password == credentials.Password).SingleOrDefault();

            if (user != null)
            {
                return Consts.Roles.Customer;
            }

            var employee = _session.QueryOver<Domain.Employee>().Where(x => x.Username == credentials.Username && x.Password == credentials.Password).SingleOrDefault();

            if (employee != null)
            {
                return Consts.Roles.Employee;
            }

            var admin = _session.QueryOver<Admin>().Where(x => x.Username == credentials.Username && x.Password == credentials.Password).SingleOrDefault();

            if (admin != null)
            {
                return Consts.Roles.Admin;
            }

            return Consts.Roles.None;
        }
    }
}
