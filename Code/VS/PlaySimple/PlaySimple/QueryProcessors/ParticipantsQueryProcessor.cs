using Domain;
using NHibernate;
using System;
using System.Collections.Generic;

namespace PlaySimple.QueryProcessors
{
    public interface IParticipantsQueryProcessor
    {
        IEnumerable<DTOs.Participant> Search(int orderId, int? customerId);

        DTOs.Participant GetParticipant(int id);

        DTOs.Participant Save(DTOs.Participant participant);

        DTOs.Participant Update(DTOs.Participant participant);
    }

    public class ParticipantsQueryProcessor : DBAccessBase<Participant>, IParticipantsQueryProcessor
    {
        private CustomersQueryProcessor _customersQueryProcessor;

        public ParticipantsQueryProcessor(ISession session, CustomersQueryProcessor customersQueryProcessor) : base(session)
        {
            _customersQueryProcessor = customersQueryProcessor;
        }

        public IEnumerable<DTOs.Participant> Search(int orderId, int? customerId)
        {
            return null;
        }

        public DTOs.Participant GetParticipant(int id)
        {
            return new DTOs.Participant().Initialize(Get(id));
        }

        public DTOs.Participant Save(DTOs.Participant participant)
        {
            throw new NotImplementedException();
        }

        public DTOs.Participant Update(DTOs.Participant participant)
        {
            throw new NotImplementedException();
        }
    }
}