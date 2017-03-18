using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using SC.BL.Domain;

namespace SC.DAL.NHibernate.Mappings.Code
{
    public class TicketResponseCodeMapping:ClassMapping<TicketResponse>
    {
        public TicketResponseCodeMapping()
        {
            Id(tr=>tr.Id, mapper =>
            {
                mapper.Column("Id");
                mapper.Generator(Generators.Native);
            });

            Property(tr=>tr.Text, mapper =>
            {
                mapper.Column("Text");
                mapper.NotNullable(false);
                mapper.Type(NHibernateUtil.String);
            });

            Property(tr=>tr.Date, mapper =>
            {
                mapper.Column("Date");
                mapper.NotNullable(false);
                mapper.Type(NHibernateUtil.DateTime);
            });

            Property(tr=>tr.IsClientResponse, mapper =>
            {
                mapper.Column("IsClientResponse");
                mapper.NotNullable(false);
                mapper.Type(NHibernateUtil.Boolean);
            });
            ManyToOne<Ticket>(tr=>tr.Ticket, mapper =>
            {
                mapper.Column("Ticket_TicketNumber");
                mapper.NotNullable(true);
                mapper.ForeignKey("FK_dbo.TicketResponse_dbo.Ticket_Ticket_TicketNumber");
            });
        }
    }
}
