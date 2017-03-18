using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg.ConfigurationSchema;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using SC.DAL.NHibernate.Mappings.Fluent;

namespace SC.DAL.NHibernate.Configuration
{
    public class FluentSqlServerConf
    {
        public FluentSqlServerConf()
        {
            var config = Fluently.Configure()
                .Mappings(mapper =>
                {
                    mapper.FluentMappings.Add<TicketMappingFluent>();
                    mapper.FluentMappings.Add<HardwareTicketFluentMapping>();
                    mapper.FluentMappings.Add<TicketResponseFluentMapping>();
                })
                .Database(MsSqlConfiguration
                    .MsSql2012
                    .ConnectionString(c => c.FromConnectionStringWithKey("SupportCenterDB_EFCodeFirst"))
                    .ShowSql()
                ).CurrentSessionContext<ThreadLocalSessionContext>()
                .BuildConfiguration();
              sessionFactory = config.BuildSessionFactory();
            new SchemaUpdate(config).Execute(true, true);
        }

        private ISessionFactory sessionFactory;

        public ISessionFactory SessionFactory { get { return sessionFactory; } }
    }
}
