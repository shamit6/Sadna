using Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySimple.QueryProcessors
{
    public interface IComplaintsQueryProcessor
    {
        IEnumerable<DTOs.Complaint> Search(int customerId);

        DTOs.Complaint GetComplaint(int id);

        DTOs.Complaint Save(DTOs.Complaint complaint);
    }

    public class ComplaintsQueryProcessor : DBAccessBase<Complaint>, IComplaintsQueryProcessor
    {
        DBAccessBase<Customer> _customersQueryProcessor;
        IDecodesQueryProcessor _decodesQueryProcessor;

        public ComplaintsQueryProcessor(ISession session, IDecodesQueryProcessor decodesQueryProcessor, DBAccessBase<Customer> customersQueryProcessor) : base(session)
        {
            _decodesQueryProcessor = decodesQueryProcessor;
            _customersQueryProcessor = customersQueryProcessor;
        }

        public IEnumerable<DTOs.Complaint> Search(int customerId)
        {
            Customer customer = _customersQueryProcessor.Get(customerId);
   
            return this.Query().Where(x => x.OffendingCustomer == customer).Select(x => new DTOs.Complaint().Initialize(x));
        }

        public DTOs.Complaint GetComplaint(int id)
        {
            return new DTOs.Complaint().Initialize(Get(id));
        }

        public DTOs.Complaint Save(DTOs.Complaint complaint)
        {
            Complaint newComplaint = new Complaint()
            {
                OffendingCustomer = _customersQueryProcessor.Get(complaint.OffendingCustomer.Id ?? 0),
                OffendedCustomer = _customersQueryProcessor.Get(complaint.OffendedCustomer.Id ?? 0),
                Description = complaint.Description,
                Type = _decodesQueryProcessor.Get<ComplaintTypeDecode>(complaint.Type)
            };

            Complaint persistedComplaint = SaveOrUpdate(newComplaint);

            return new DTOs.Complaint().Initialize(persistedComplaint);
        }
    }
}