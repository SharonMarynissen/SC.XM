
using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using SC.DAL.NHibernate.Mappings.Code;

namespace SC.DAL.NHibernate.Configuration
{
    public class NhSqlServLoquaciousConf
    {
        private readonly ISessionFactory sessionFactory;

        public NhSqlServLoquaciousConf()
        {
            var config = new global::NHibernate.Cfg.Configuration();
            config.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2012Dialect>();
                db.Driver<SqlClientDriver>();
                db.ConnectionString =
                    @"Data Source=.\SQLSERVER2012;Initial Catalog=SC_NHibernate;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                db.ConnectionReleaseMode = ConnectionReleaseMode.OnClose;
                db.LogSqlInConsole = true;
                db.LogFormattedSql = true;
            })
            .AddMapping(GetMappings());

            sessionFactory = config.BuildSessionFactory();
        //    var session = sessionFactory.OpenSession();

        //    using (var tx = session.BeginTransaction())
        //    {
        //        new SchemaUpdate(config).Execute(true, false);
        //        tx.Commit();
        //    }
        //    session.Clear();
       }

        private HbmMapping GetMappings()
        {
            var mapper = new ModelMapper();
            mapper.AddMapping<TicketCodeMapping>();
            mapper.AddMapping<HardwareTicketCodeMapping>();
            mapper.AddMapping<TicketResponseCodeMapping>();

            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }

        public ISessionFactory SessionFactory { get { return sessionFactory; } }
    }
}
