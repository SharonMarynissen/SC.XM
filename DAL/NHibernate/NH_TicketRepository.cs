using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using SC.BL.Domain;
using SC.DAL.NHibernate.Configuration;

namespace SC.DAL.NHibernate
{
    public class NhTicketRepository : ITicketRepository
    {
        private static ISessionFactory sessionFactory;

        public NhTicketRepository()
        {
            //sessionFactory = new global::NHibernate.Cfg.Configuration()
            //    .Configure(Assembly.GetExecutingAssembly(), "SC.DAL.NHibernate.Configuration.hibernate.cfg.xml")
            //    .BuildSessionFactory();
            //sessionFactory = new NhSqlServLoquaciousConf().SessionFactory;
            sessionFactory = new FluentSqlServerConf().SessionFactory;
        }

        public IEnumerable<Ticket> ReadTickets()
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    IEnumerable<Ticket> tickets = session.Query<Ticket>().Fetch(t => t.Responses);
                    tx.Commit();
                    return tickets.ToList();
                }
            }

        }

        public Ticket CreateTicket(Ticket ticket)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    object id = session.Save(ticket);
                    tx.Commit();
                    return session.Get<Ticket>(id);
                }
            }
        }

        public Ticket ReadTicket(int ticketNumber)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    Ticket t = session.Get<Ticket>(ticketNumber);
                    tx.Commit();
                    return t;
                }
            }
        }

        public void UpdateTicket(Ticket ticket)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Update(ticket);
                    tx.Commit();
                }
            }
        }

        public void UpdateTicketStateToClosed(int ticketNumber)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    Ticket t = session.Get<Ticket>(ticketNumber);
                    t.State = TicketState.Closed;
                    tx.Commit();
                }
            }
        }

        public void DeleteTicket(int ticketNumber)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Delete(session.Get<Ticket>(ticketNumber));
                    tx.Commit();
                }
            }
        }

        public IEnumerable<TicketResponse> ReadTicketResponsesOfTicket(int ticketNumber)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    IEnumerable<TicketResponse> responses =
                        session.Query<TicketResponse>().Where(tr => tr.Ticket.TicketNumber == ticketNumber).ToList();

                    tx.Commit();
                    return responses;
                }
            }
        }

        public TicketResponse CreateTicketResponse(TicketResponse response)
        {
            var session = sessionFactory.OpenSession();
            using (var tx = session.BeginTransaction())
            {
                try
                {
                    session.Save(response);
                    tx.Commit();
                    return response;
                }
                catch (Exception)
                {
                    tx.Rollback();
                    throw;
                }
                finally
                {
                    session.Close();
                }
            }
        }
    }
}
