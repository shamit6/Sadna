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
        IEnumerable<DTOs.Employee> Search(string firstName, string lastName, string eMail, int? id);

        DTOs.Employee GetEmployee(int id);

        DTOs.Employee Save(DTOs.Employee employee);

        DTOs.Employee Update(int id, DTOs.Employee employee);
    }

    public class EmployeesQueryProcessor : DBAccessBase<Employee>, IEmployeesQueryProcessor
    {
        public EmployeesQueryProcessor(ISession session) : base(session)
        {
        }
        public IEnumerable<DTOs.Employee> Search(string firstName, string lastName, string eMail, int? id)
        {
            var query = Query();

            if (!string.IsNullOrEmpty(firstName))
            {
                query.Where(x => x.FirstName.Contains(firstName));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                query.Where(x => x.LastName.Contains(lastName));
            }

            if (!string.IsNullOrEmpty(eMail))
            {
                query.Where(x => x.Email.Contains(eMail));
            }

            if (id.HasValue)
            {
                query.Where(x => x.Id == id);
            }

            return query.Select(x => new DTOs.Employee().Initialize(x));
        }

        public DTOs.Employee GetEmployee(int id)
        {
            return new DTOs.Employee().Initialize(Get(id));
        }

        public DTOs.Employee Save(DTOs.Employee employee)
        {
            Employee newEmployee = new Employee
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Username = employee.Username,
                Password = employee.Username,
                Salary = employee.Salary,
                Email = employee.Email
            };

            Employee persistedEmployee = SaveOrUpdate(newEmployee);

            return new DTOs.Employee().Initialize(persistedEmployee);
        }

        public DTOs.Employee Update(int id, DTOs.Employee employee)
        {
            Employee existingEmployee = Get(id);

            existingEmployee.FirstName = employee.FirstName ?? existingEmployee.FirstName;
            existingEmployee.LastName = employee.LastName ?? existingEmployee.LastName;
            existingEmployee.Username = employee.Username ?? existingEmployee.Username;
            existingEmployee.Password = employee.Password ?? existingEmployee.Password;
            existingEmployee.Email = employee.Email ?? existingEmployee.Email;

            if (employee.Salary != 0)
                existingEmployee.Salary = employee.Salary;

            Employee newEmployee = SaveOrUpdate(existingEmployee);

            return new DTOs.Employee().Initialize(newEmployee);
        }
    }
}