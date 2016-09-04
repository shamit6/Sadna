using Domain;
using NHibernate;
using PlaySimple.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySimple.QueryProcessors
{
    public interface ICustomersQueryProcessor
    {
        IEnumerable<DTOs.Customer> Search(string firstName, string lastName, int? minAge, int? maxAge, string region, int? customerId);

        DTOs.Customer GetCustomer(int id);

        DTOs.Customer Save(DTOs.Customer customer);

        DTOs.Customer Update(int id, DTOs.Customer customer);
    }

    public class CustomersQueryProcessor : DBAccessBase<Customer>, ICustomersQueryProcessor
    {
        private readonly IDecodesQueryProcessor _decodesQueryProcessor;

        public CustomersQueryProcessor(ISession session, IDecodesQueryProcessor decodesQueryProcessor) : base(session)
        {
            _decodesQueryProcessor = decodesQueryProcessor;
        }

        public IEnumerable<DTOs.Customer> Search(string firstName, string lastName, int? minAge, int? maxAge, string region, int? customerId)
        {
            var query = Query();

            if (!string.IsNullOrEmpty(firstName))
            {
                query.Where(x => x.FirstName.Contains(firstName));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                query.Where(x => x.FirstName.Contains(lastName));
            }

            if (minAge.HasValue)
            {
                query.Where(x => DateUtils.GetAge(x.BirthDate) >= minAge);
            }

            if (maxAge.HasValue)
            {
                query.Where(x => DateUtils.GetAge(x.BirthDate) <= minAge);
            }

            if (region != null)
            {
                RegionDecode regionDecode = _decodesQueryProcessor.Get<RegionDecode>(region);
                query.Where(x => x.Region == regionDecode);
            }

            if (customerId.HasValue)
            {
                query.Where(x => x.Id == customerId);
            }

            return query.Select(x => new DTOs.Customer().Initialize(x));
        }

        public DTOs.Customer GetCustomer(int id)
        {
            return new DTOs.Customer().Initialize(Get(id));
        }

        public DTOs.Customer Save(DTOs.Customer customer)
        {
            Customer newCustomer = new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Username = customer.Username,
                Password = customer.Password,
                BirthDate = customer.BirthDate,
                Email = customer.Email,
                Region = _decodesQueryProcessor.Get<RegionDecode>(customer.Region),
                FreezeDate = customer.FreezeDate
            };

            Customer persistedCustomer = SaveOrUpdate(newCustomer);

            return new DTOs.Customer().Initialize(persistedCustomer);
        }

        public DTOs.Customer Update(int id, DTOs.Customer customer)
        {
            Customer existingCustomer = Get(id);

            existingCustomer.FirstName = customer.FirstName ?? existingCustomer.FirstName;
            existingCustomer.LastName = customer.LastName ?? existingCustomer.LastName;
            existingCustomer.Username = customer.Username ?? existingCustomer.Username;
            existingCustomer.Password = customer.LastName ?? existingCustomer.Password;
            existingCustomer.Email = customer.Email ?? existingCustomer.Email;

            if (customer.BirthDate != null)
                existingCustomer.BirthDate = customer.BirthDate;

            if (customer.Region != null)
                existingCustomer.Region = _decodesQueryProcessor.Get<RegionDecode>(customer.Region);

            if (customer.FreezeDate != null)
                existingCustomer.FreezeDate = customer.FreezeDate;

            Customer persistedCustomer = SaveOrUpdate(existingCustomer);

            return new DTOs.Customer().Initialize(persistedCustomer);
        }
    }
}