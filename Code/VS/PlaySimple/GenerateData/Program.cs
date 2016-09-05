using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Maps;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using PlaySimple.QueryProcessors;
using PlaySimple.DTOs;
using System.Collections.Generic;

namespace GenerateData
{
    class Program
    {
        static void Main(string[] args)
        {
            string absoluteDbPath = "C:/Users/Amit/Documents/GitHub/Sadna/Code/VS/PlaySimple/PlaySimple/App_Data/db.sqlite";

            ISessionFactory sessionFactory = Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard.UsingFile(absoluteDbPath))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CustomerMap>())
                    .CurrentSessionContext("web")
                    .ExposeConfiguration(conf => new SchemaUpdate(conf).Execute(false, true))
                    .BuildSessionFactory();
            var session = sessionFactory.OpenSession();

            DecodesQueryProcessor decode = new DecodesQueryProcessor(session);
            EmployeesQueryProcessor employeeQP = new EmployeesQueryProcessor(session);
            FieldsQueryProcessor fieldQP = new FieldsQueryProcessor(decode, session);
            CustomersQueryProcessor customerQP = new CustomersQueryProcessor(session, decode);

            ComplaintsQueryProcessor complaintQP = new ComplaintsQueryProcessor(session, decode, customerQP);
            OrdersQueryProcessor orderQP = new OrdersQueryProcessor(session, customerQP, fieldQP, decode);
            ParticipantsQueryProcessor participantQP = new ParticipantsQueryProcessor(session, customerQP, orderQP, decode);
            ReviewsQueryProcessor reviewQP = new ReviewsQueryProcessor(session, customerQP);

            ReportsQueryProcessor reportsQP = new ReportsQueryProcessor(customerQP, orderQP, complaintQP, participantQP);

            IEnumerable<CustomersActivityReport> resualt = reportsQP.GetCustomersActivityReport(null, null, null, null);
            //Order order = new Order()
            //{
            //    Field = fieldQP.GetField(1),
            //    Owner = customerQP.GetCustomer(1),
            //    StartDate = new System.DateTime(2016, 8, 27, 18, 0, 0),
            //    Status = 1,
            //    PlayersNumber = 2
            //};

            //orderQP.Save(order);

            foreach (var item in resualt)
            {
                item.ToString();
            }
            int i = 0;
            i++;
        }
    }
}
