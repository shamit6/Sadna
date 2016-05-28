﻿using Domain;
using FluentNHibernate.Mapping;

namespace Maps
{
    public class ComplaintMap : ClassMap<Complaint>
    {
        public ComplaintMap()
        {
            Id(x => x.Id);

            Map(x => x.Description);

            References(x => x.Type);
            References(x => x.OffendingUser);
            References(x => x.OffendedUser);
        }
    }
}
