using Domain;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySimple.QueryProcessors
{
    public abstract class DBAccessBase<T> : IDatabaseAccess<T> where T : Entity
    {
        private readonly ISession _session;

        public DBAccessBase(ISession session)
        {
            _session = session;
        }

        // TODO improve
        public T SaveOrUpdate(T entity)
        {
            if (entity.Id != 0)
            {
                _session.Update(entity);       
            }
            else
            {
                entity.Id = (int)_session.Save(entity);
            }

            return entity;
        }

        public T Get(int id)
        {
            return _session.Get<T>(id);
        }

        public IQueryable<T> Query()
        {
            return _session.Query<T>();
        }
    }

}