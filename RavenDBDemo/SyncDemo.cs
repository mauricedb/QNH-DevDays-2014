using System;
using System.Linq;
using Raven.Client;

namespace RavenDBDemo
{
    public class SyncDemo
    {
        private readonly IDocumentStore _documentStore;

        public SyncDemo(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public void Execute()
        {
            using (IDocumentSession session = _documentStore.OpenSession())
            {
                CreateCustomer(session);
                ListCustomers(session);
            }
        }

        private static void ListCustomers(IDocumentSession session)
        {
            var customers = session.Query<Customer>().ToList();

            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }

        private static void CreateCustomer(IDocumentSession session)
        {
            var customer = new Customer
            {
                Name = "Demo " + DateTime.Now.ToLongTimeString()
            };

            session.Store(customer);
            session.SaveChanges();
        }
    }
}