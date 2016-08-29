﻿using Domain;
using NHibernate;
using PlaySimple.Common;
using PlaySimple.Controllers;
using System;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PlaySimple.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class GlobalAuthorizationFilter : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Checks basic authentication request
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(HttpActionContext filterContext)
        {
            var identity = FetchAuthHeader(filterContext);

            // no authentication header sent
            if (identity == null)
                throw new SecurityException();

            var genericPrincipal = new GenericPrincipal(identity, null);
            
            // saves the user details on the current thread
            Thread.CurrentPrincipal = genericPrincipal;

            if (!AuthorizeUser(identity.Name, identity.Password, filterContext))
            {
                throw new SecurityException();
            }

            base.OnAuthorization(filterContext);
        }

        /// <summary>
        /// Virtual method.Can be overriden with the custom Authorization.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool AuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            ISession session =(ISession)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ISession));

            try
            {
                var user = session.QueryOver<Customer>().Where(x => x.Username == username && x.Password == password).SingleOrDefault();

                if (user != null)
                {
                    AuthorizationSucceed(user.Id, Consts.Roles.Customer);
                    return true;
                }

                var employee = session.QueryOver<Employee>().Where(x => x.Username == username && x.Password == password).SingleOrDefault();

                if (employee != null)
                {
                    AuthorizationSucceed(employee.Id, Consts.Roles.Employee);
                    return true;
                }

                var admin = session.QueryOver<Admin>().Where(x => x.Username == username && x.Password == password).SingleOrDefault();

                if (admin != null)
                {
                    AuthorizationSucceed(admin.Id, Consts.Roles.Admin);
                    return true;
                }
            }
            catch
            {
            }

            return false;
        }

        private void AuthorizationSucceed(int userId, string userRole)
        {
            var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                    
            basicAuthenticationIdentity.UserId = userId;

            // TODO: add user role for authorization
        }

        /// <summary>
        /// Checks for autrhorization header in the request and parses it, creates user credentials and returns as BasicAuthenticationIdentity
        /// </summary>
        /// <param name="filterContext"></param>
        protected virtual BasicAuthenticationIdentity FetchAuthHeader(HttpActionContext filterContext)
        {
            string authHeaderValue = null;

            var authRequest = filterContext.Request.Headers.Authorization;

            // support only basic authentication (the browser sends username:password as a request header)
            if (authRequest != null && authRequest.Scheme == "Basic")
            {
                authHeaderValue = authRequest.Parameter;
            }

            // no header found
            if (string.IsNullOrEmpty(authHeaderValue))
                return null;

            // deserialize authentication header
            authHeaderValue = Encoding.Default.GetString(Convert.FromBase64String(authHeaderValue));

            var credentials = authHeaderValue.Split(':');

            return credentials.Length < 2 ? null : new BasicAuthenticationIdentity(credentials[0], credentials[1]);
        }
    }
}