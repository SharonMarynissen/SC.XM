using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.BL.Domain
{
    public class Ticket
    {
        //EF doesn't work with virtual properties (only adding responses to a ticket doesn't work) , but NHibernate needs virtual properties
        public virtual int TicketNumber { get; set; }
        public virtual int AccountId { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Er zijn maximaal 100 tekens toegestaan")]
        public virtual string Text { get; set; }
        public virtual DateTime DateOpened { get; set; }
        public virtual TicketState State { get; set; }

        //public int TicketNumber { get; set; }
        //public int AccountId { get; set; }
        //[Required]
        //[MaxLength(100, ErrorMessage="Er zijn maximaal 100 tekens toegestaan")]
        //public string Text { get; set; }
        //public DateTime DateOpened { get; set; }
        //public TicketState State { get; set; }

        public virtual ICollection<TicketResponse> Responses { get; set; } /* TOEGEVOEGD 'virtual' for lazy-loading, if enabled on context (default) */
    }
}
