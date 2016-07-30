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

        DTOs.Complaint Get(int id);

        DTOs.Complaint SaveOrUpdate(DTOs.Complaint complaint);
    }

    public class ComplaintsQueryProcessor : DBAccessBase<Complaint>, IComplaintsQueryProcessor
    {
        public ComplaintsQueryProcessor(ISession session) : base(session)
        {
        }

        public IEnumerable<DTOs.Complaint> Search(int customerId)
        {
            this.Query().Where(x => x.Description == "").Select(x => new DTOs.Complaint().Initialize(x));
            return null;
        }

        public DTOs.Complaint Get(int id) { return null; }

        public DTOs.Complaint SaveOrUpdate(DTOs.Complaint complaint) { return null; }
    }
}