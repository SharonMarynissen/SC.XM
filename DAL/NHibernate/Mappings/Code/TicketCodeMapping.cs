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
    public class TicketCodeMapping : ClassMapping<Ticket>
    {
        public TicketCodeMapping()
        {
            Discriminator(d =>
            {
                d.Column("Discriminator");
            });
            DiscriminatorValue("Ticket");
            Id(t => t.TicketNumber, mapper =>
            {
                mapper.Generator(Generators.Native);
                mapper.Column("TicketNumber");
            });

            Property(t => t.AccountId, mapper =>
            {
                mapper.Column("AccountId");
                mapper.NotNullable(false);
            });

            Property(t => t.Text, mapper =>
            {
                mapper.Column("Text");
                mapper.NotNullable(false);
                mapper.Type(NHibernateUtil.String);
            });

            Property(t => t.DateOpened, mapper =>
            {
                mapper.Column("DateOpened");
                mapper.NotNullable(false);
                mapper.Type(NHibernateUtil.DateTime);
            });

            Property(t => t.State, mapper =>
            {
                mapper.Column("State");
                mapper.NotNullable(false);
                mapper.Type(NHibernateUtil.Byte);
            });

            Bag(t => t.Responses,
                mapper =>
                {
                    mapper.Key(k => k.Column("Ticket_TicketNumber"));
                    mapper.Cascade(Cascade.DeleteOrphans);
                    mapper.Inverse(true);
                },
                relation => relation.OneToMany(
                    mapping => mapping.Class(typeof(TicketResponse))
                    )
            );
        }
    }

    public class HardwareTicketCodeMapping : SubclassMapping<HardwareTicket>
    {
        public HardwareTicketCodeMapping()
        {
            DiscriminatorValue("HardwareTicket");
            Property(ht => ht.DeviceName, mapper =>
            {
                mapper.Column("DeviceName");
                mapper.NotNullable(false);
                mapper.Type(NHibernateUtil.String);
            });
        }
    }
}