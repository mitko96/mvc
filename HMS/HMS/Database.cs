using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using HMS.Models;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS
{
    public partial class Database
    {
        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(PostgreSQLConfiguration.PostgreSQL82
                  .ConnectionString(@"Server=localhost;Port=5433;Database=HMS;User Id=postgres;Password=mitko9670;")
                              .ShowSql()
                )
               .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<DoctorsMap>()
                              .AddFromAssemblyOf<PatiensMap>()
                              .AddFromAssemblyOf<UsersMap>()
                              .AddFromAssemblyOf<AddMap>()
                              .AddFromAssemblyOf<StarRatingMap>()
                              .AddFromAssemblyOf<RecipteMap>()
                              .AddFromAssemblyOf<ViewMap>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                                .Create(false, false))
                .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}