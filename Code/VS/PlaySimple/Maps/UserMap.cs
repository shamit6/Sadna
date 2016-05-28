using Domain;
using FluentNHibernate.Mapping;

namespace Maps
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("User");

            Id(x => x.Id);

            Map(x => x.Username);
            Map(x => x.Password);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.BirthDate);
            Map(x => x.Email);
            Map(x => x.FreezeDate);

            References(x => x.Region);

            HasMany(x => x.Reviews);
            HasMany(x => x.Complaints);
            HasMany(x => x.Orders);
            HasMany(x => x.ParticipationRequests);
        }
    }
}
