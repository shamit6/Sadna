using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Maps;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Web;

namespace PlaySimple.Common
{
    public class NhibernateManager
    {
        private static NhibernateManager _manager;

        private ISessionFactory _sessionFactory;

        public static NhibernateManager Instance
        {
            get
            {
                if (_manager == null)
                    _manager = new NhibernateManager();

                return _manager;
            }
        }

        private NhibernateManager()
        {
        }

        public void Init()
        {
            string absoluteDbPath = HttpContext.Current.Server.MapPath(Consts.DB_PATH);

            _sessionFactory = Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard.UsingFile(absoluteDbPath))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
                    .ExposeConfiguration(conf => new SchemaUpdate(conf).Execute(false, true))
                    .BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            // TODO: rewrite this (and open transaction)
            return _sessionFactory.OpenSession();
        }

        public void CloseSession()
        {
            // TODO: impl this
        }
    }
}