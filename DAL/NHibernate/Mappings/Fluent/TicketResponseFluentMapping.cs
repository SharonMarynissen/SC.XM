using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using SC.BL.Domain;

namespace SC.DAL.NHibernate.Mappings.Fluent
{
    public class TicketResponseFluentMapping : ClassMap<TicketResponse>
    {
        public TicketResponseFluentMapping()
        {
            Id(tr => tr.Id, "Id").GeneratedBy.Increment();
            Map(tr => tr.Text, "Text").Nullable();
            Map(tr => tr.Date, "Date").Nullable()
                .CustomType(typeof(DateTime));
            Map(tr => tr.IsClientResponse, "IsClientResponse").Nullable()
                .CustomType(typeof(bool));
            References<Ticket>(tr => tr.Ticket, "Ticket_id");
        }
    }
}
