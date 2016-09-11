﻿using Domain;
using NHibernate;
using PlaySimple.DTOs;
using PlaySimple.QueryProcessors;
using System;
using System.Text;
using System.Web.Http;

namespace PlaySimple.Controllers
{
    public class LoginController : ApiController
    {
        private ICustomersQueryProcessor _customersQueryProcessor;
        ISession _session;

        public LoginController(ISession session, ICustomersQueryProcessor customersQueryProcessor)
        {
            _session = session;
            _customersQueryProcessor = customersQueryProcessor;
        }

        [HttpPost]
        [Route("api/login/login")]
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

        [HttpPost]
        [Route("api/login/registration")]
        public LoginResponse Registration(DTOs.Customer customer)
        {
            _customersQueryProcessor.Save(customer);

            UserCredentials credential = new UserCredentials()
            {
                Username = customer.Username,
                Password = customer.Password
            };

            return Login(credential);
        }
    }
}
