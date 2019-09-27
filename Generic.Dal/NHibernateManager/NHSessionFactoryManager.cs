using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Reflection;

namespace Generic.Dal.NHibernateManager
{
    public class NHSessionFactoryManager
    {
        private static ISessionFactory _sessionFactory = null;
        private static Object createLock = new Object();


        public static ISessionFactory GetSessionFactory()
        {
            try
            {
                lock (createLock)
                {

                    if (_sessionFactory == null)
                    {
                        FluentConfiguration config = BuildConfiguration();
                        _sessionFactory = config.BuildSessionFactory();
                    }
                }

                return _sessionFactory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static FluentConfiguration BuildConfiguration()
        {
            try
            {

#if DEBUG
                var ConnectionString =
                    "Server=server;Initial Catalog=catalog;User Id=user;Password=pwd";
#else
                var ConnectionString =
                    "Server=server;Initial Catalog=catalog;User Id=user;Password=pwd";
#endif

                var config = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012
                        .ShowSql().FormatSql()
                        .ConnectionString(ConnectionString))
                    .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.Load("Record.Model")))
                    .ExposeConfiguration(conf => new SchemaUpdate(conf).Execute(false, true));

                return config;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
