using Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySimple.QueryProcessors
{
    public interface ICustomersQueryProcessor
    {
        IEnumerable<DTOs.Customer> Search(string firstName, string lastName, int minAge, int maxAge, int regionId, int customerId);

        DTOs.Customer Get(int id);

        DTOs.Customer SaveOrUpdate(DTOs.Customer customer);
    }

    public class CustomersQueryProcessor : DBAccessBase<Customer>, ICustomersQueryProcessor
    {
        private readonly IParticipantsQueryProcessor _participantsQueryProcessor;

        public CustomersQueryProcessor(ISession session, IParticipantsQueryProcessor participantsQueryProcessor) : base(session)
        {
            _participantsQueryProcessor = participantsQueryProcessor;
        }

        public IEnumerable<DTOs.Customer> Search(string firstName, string lastName, int minAge, int maxAge, int regionId, int customerId)
        {
            return null;
        }

        public DTOs.Customer Get(int id) { return null; }

        public DTOs.Customer SaveOrUpdate(DTOs.Customer customer) { return null; }
    }
}