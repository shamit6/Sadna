using Domain;
using NHibernate;
using PlaySimple.DTOs;
using PlaySimple.QueryProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
        public LoginResponse Login(UserCredentials credentials)
        {
            byte[] toEncodeAsBytes = ASCIIEncoding.ASCII.GetBytes(credentials.Username + ":" + credentials.Password);
            string authenticationKey = "Basic " + Convert.ToBase64String(toEncodeAsBytes);
            string role = null;

            var user = _session.QueryOver<Domain.Customer>().Where(x => x.Username == credentials.Username && x.Password == credentials.Password).SingleOrDefault();

            if (user != null)
            {
                role = Consts.Roles.Customer;
            }

            var employee = _session.QueryOver<Domain.Employee>().Where(x => x.Username == credentials.Username && x.Password == credentials.Password).SingleOrDefault();

            if (employee != null)
            {
                role = Consts.Roles.Employee;
            }

            var admin = _session.QueryOver<Admin>().Where(x => x.Username == credentials.Username && x.Password == credentials.Password).SingleOrDefault();

            if (admin != null)
            {
                role = Consts.Roles.Admin;
            }

            role = role ?? Consts.Roles.None;

            return new LoginResponse
            {
                AuthorizationKey = authenticationKey,
                Role = role
            };
        }
    }
}
