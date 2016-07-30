using Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySimple.QueryProcessors
{
    public interface IParticipantsQueryProcessor
    {
        IEnumerable<DTOs.Participant> Search(int orderId, int? customerId);

        DTOs.Participant Get(int id);

        DTOs.Participant SaveOrUpdate(DTOs.Participant participant);
    }

    public class ParticipantsQueryProcessor : DBAccessBase<Participant>, IParticipantsQueryProcessor
    {
        public ParticipantsQueryProcessor(ISession session) : base(session)
        {

        }

        public IEnumerable<DTOs.Participant> Search(int orderId, int? customerId)
        {
            return null;
        }

        public DTOs.Participant Get(int id) { return null; }

        public DTOs.Participant SaveOrUpdate(DTOs.Participant Customer) { return null; }
    }
}