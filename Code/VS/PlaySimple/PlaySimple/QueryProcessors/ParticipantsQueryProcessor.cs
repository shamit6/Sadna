using Domain;
using LinqKit;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlaySimple.QueryProcessors
{
    public interface IParticipantsQueryProcessor
    {
        IEnumerable<DTOs.Participant> Search(int? orderId, int? customerId, int?[] statusIds);

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

        public IEnumerable<DTOs.Participant> Search(int? orderId, int? customerId, int?[] statusIds)
        {
            var filter = PredicateBuilder.New<Participant>(x => true);

            if (orderId.HasValue)
            {
                filter.And(x => x.Order.Id == orderId);
            }

            if (customerId.HasValue)
            {
                filter.And(x => x.Customer.Id == customerId);
            }

            if (statusIds != null)
            {
                filter.And(x => statusIds.Contains(x.Status.Id));
            }

            var result =  Query().Where(filter).Select(x => new DTOs.Participant().Initialize(x));
            return result;

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

            Participant persistedParticipant = Save(newParticipant);

            return new DTOs.Participant().Initialize(persistedParticipant);
        }

        // Only status can be changed
        public DTOs.Participant Update(int id, DTOs.Participant participant)
        {
            Participant existingParticipant = Get(id);

            existingParticipant.Status = _decodesQueryProcessor.Get<InvitationStatusDecode>(participant.Status);

            Update(id, existingParticipant);

            return new DTOs.Participant().Initialize(existingParticipant);
        }
    }
}