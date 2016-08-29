﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Maps;
using NHibernate;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using Ninject;
using Ninject.Activation;
using Ninject.Web.Common;
using PlaySimple.QueryProcessors;
using System;
using System.Web;

namespace PlaySimple.Common
{
    public class NinjectConfigurator
    {
        public void Configure(IKernel container)
        {
            AddBindings(container);
        }

        public void AddBindings(IKernel container)
        {
            ConfigureNhibernate(container);
            ConfigureQueryProcessors(container);
        }

        private void ConfigureQueryProcessors(IKernel container)
        {
            container.Bind<IDecodesQueryProcessor>().To<DecodesQueryProcessor>();
            container.Bind<IFieldsQueryProcessor>().To<FieldsQueryProcessor>();
        }

        private void ConfigureNhibernate(IKernel container)
        {
            //string absoluteDbPath = HttpContext.Current.Server.MapPath(Consts.DB_PATH);
            string absoluteDbPath = "D:/Dev/sadna/Sadna/Code/VS/PlaySimple/PlaySimple/App_Data/db.sqlite";

            ISessionFactory _sessionFactory = Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard.UsingFile(absoluteDbPath))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CustomerMap>())
                    .CurrentSessionContext("web")
                    .ExposeConfiguration(conf => new SchemaUpdate(conf).Execute(false, true))
                    .BuildSessionFactory();

            container.Bind<ISessionFactory>().ToConstant(_sessionFactory);

            container.Bind<ISession>().ToMethod(CreateSession).InRequestScope();
        }

        private ISession CreateSession(IContext context)
        {
            var sessionFactory = context.Kernel.Get<ISessionFactory>();

            if (!CurrentSessionContext.HasBind(sessionFactory))
            {
                var session = sessionFactory.OpenSession();
                CurrentSessionContext.Bind(session);
            }

            return sessionFactory.GetCurrentSession();
        }
    }
}