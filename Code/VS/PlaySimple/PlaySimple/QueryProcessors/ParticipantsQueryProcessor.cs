using Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlaySimple.QueryProcessors
{
    public interface IParticipantsQueryProcessor
    {
        IEnumerable<DTOs.Participant> Search(int? orderId, int? customerId, string status);

        DTOs.Participant GetParticipant(int id);

        DTOs.Participant Save(DTOs.Participant participant);

        DTOs.Participant Update(int id, DTOs.Participant participant);
    }

    public class ParticipantsQueryProcessor : DBAccessBase<Participant>, IParticipantsQueryProcessor
    {
        private CustomersQueryProcessor _customersQueryProcessor;
        private OrdersQueryProcessor _ordersQueryProcessor;
        private IDecodesQueryProcessor _decodesQueryProcessor;

        public ParticipantsQueryProcessor(ISession session, CustomersQueryProcessor customersQueryProcessor, OrdersQueryProcessor ordersQueryProcessor, IDecodesQueryProcessor decodesQueryProcessor) : base(session)
        {
            _customersQueryProcessor = customersQueryProcessor;
            _ordersQueryProcessor = ordersQueryProcessor;
            _decodesQueryProcessor = decodesQueryProcessor;
        }

        public IEnumerable<DTOs.Participant> Search(int? orderId, int? customerId, string status)
        {
            var query = Query();

            if (customerId.HasValue)
            {
                query.Where(x => x.Order.Id == customerId);
            }

            if (customerId.HasValue)
            {
                query.Where(x => x.Customer.Id == customerId);
            }

            if (status != null)
            {
                query.Where(x => x.Status == _decodesQueryProcessor.Get<InvitationStatusDecode>(status));
            }

            return query.Select(x => new DTOs.Participant().Initialize(x));

        }

        public DTOs.Participant GetParticipant(int id)
        {
            return new DTOs.Participant().Initialize(Get(id));
        }

        public DTOs.Participant Save(DTOs.Participant participant)
        {
            Participant newParticipant = new Participant()
            {
                Customer = _customersQueryProcessor.Get(participant.Customer.Id ?? 0),
                Date = participant.Date,
                Order = _ordersQueryProcessor.Get(participant.Order.Id ?? 0),
                Status = _decodesQueryProcessor.Get<InvitationStatusDecode>(participant.Status)
            };

            Participant persistedParticipant = SaveOrUpdate(newParticipant);

            return new DTOs.Participant().Initialize(persistedParticipant);
        }

        // Only status can be changed
        public DTOs.Participant Update(int id, DTOs.Participant participant)
        {
            Participant existingParticipant = Get(id);

            existingParticipant.Status = _decodesQueryProcessor.Get<InvitationStatusDecode>(participant.Status);

            Participant newParticipant = SaveOrUpdate(existingParticipant);

            return new DTOs.Participant().Initialize(newParticipant);
        }
    }
}