using Domain;
using FluentNHibernate.Mapping;

namespace Maps
{
    public class ParticipantMap : ClassMap<Participant>
    {
        public ParticipantMap()
        {
            Id(x => x.Id);

            Map(x => x.Date);

            References(x => x.User);
            References(x => x.Status);
            //References(x => x.Order);
        }
    }
}
