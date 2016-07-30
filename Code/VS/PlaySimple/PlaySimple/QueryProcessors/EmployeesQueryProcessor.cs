using Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySimple.QueryProcessors
{
    public interface IEmployeesQueryProcessor
    {
        IEnumerable<DTOs.Employee> Search(string firstName, string lastName, int eMail, int id);

        DTOs.Employee Get(int id);

        DTOs.Employee SaveOrUpdate(DTOs.Employee employee);
    }

    public class EmployeesQueryProcessor : DBAccessBase<Employee>, IEmployeesQueryProcessor
    {
        public EmployeesQueryProcessor(ISession session) : base(session)
        {
        }
        public IEnumerable<DTOs.Employee> Search(string firstName, string lastName, int eMail, int id)
        {
            return null;
        }

        public DTOs.Employee Get(int id) { return null; }

        public DTOs.Employee SaveOrUpdate(DTOs.Employee employee) { return null; }

    }
}