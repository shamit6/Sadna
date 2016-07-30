using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaySimple.QueryProcessors
{
    interface IDatabaseAccess<T> where T : Entity
    {
        T SaveOrUpdate(T entity);
        T Get(int id);
        IQueryable<T> Query();
    }
}
