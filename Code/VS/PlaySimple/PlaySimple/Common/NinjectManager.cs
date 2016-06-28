using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Maps;
using NHibernate;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using Ninject;
using Ninject.Activation;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PlaySimple.Common
{
    public class NinjectManager
    {
        public static IKernel Init()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);

            // Install our Ninject-based IDependencyResolver into the Web API config
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

            return kernel;
        }

        private static void RegisterServices(IKernel container)
        {
            string absoluteDbPath = HttpContext.Current.Server.MapPath(Consts.DB_PATH);

            ISessionFactory _sessionFactory = Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard.UsingFile(absoluteDbPath))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
                    .CurrentSessionContext("web")
                    .ExposeConfiguration(conf => new SchemaUpdate(conf).Execute(false, true))
                    .BuildSessionFactory();

            container.Bind<ISessionFactory>().ToConstant(_sessionFactory);

            container.Bind<ISession>().ToMethod(CreateSession).InRequestScope();
        }

        private static ISession CreateSession(IContext container)
        {
            var sessionFactory = (ISessionFactory)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ISessionFactory));

            if (!CurrentSessionContext.HasBind(sessionFactory))
            {
                var session = sessionFactory.OpenSession();
                CurrentSessionContext.Bind(session);
            }

            return sessionFactory.GetCurrentSession();
        }
    }
}