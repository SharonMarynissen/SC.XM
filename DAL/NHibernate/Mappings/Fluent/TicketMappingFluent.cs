using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using SC.BL.Domain;

namespace SC.DAL.NHibernate.Mappings.Fluent
{
    public class TicketMappingFluent : ClassMap<Ticket>
    {
        public TicketMappingFluent()
        {
            Id(t => t.TicketNumber, "TicketNumber").GeneratedBy.Native();
            Map(t => t.AccountId, "AccountId").Nullable();
            Map(t => t.Text, "Text").Nullable();
            Map(t => t.DateOpened, "DateOpened").Nullable();
            Map(t => t.State, "State").CustomType<TicketState>().Nullable();
            HasMany(t => t.Responses)
                .Cascade.SaveUpdate()
                .KeyColumn("Ticket_TicketNumber")
                .Inverse();
        }
    }

    public class HardwareTicketFluentMapping : SubclassMap<HardwareTicket>
    {
        public HardwareTicketFluentMapping()
        {
            DiscriminatorValue("HardwareTicket");
            Map(ht => ht.DeviceName, "DeviceName").Nullable();
        }
    }
}
