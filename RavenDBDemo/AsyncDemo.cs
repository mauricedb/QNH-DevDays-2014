using System;
using System.Threading.Tasks;
using Raven.Client;

namespace RavenDBDemo
{
    public class AsyncDemo
    {
        private readonly IDocumentStore _documentStore;

        public AsyncDemo(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public async Task Execute()
        {
            using (IAsyncDocumentSession session = _documentStore.OpenAsyncSession())
            {
                await CreateCustomer(session);
                await ListCustomers(session);
            }
        }

        private static async Task ListCustomers(IAsyncDocumentSession session)
        {
            var customers = await session.Query<Customer>().ToListAsync();
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }

        private async Task CreateCustomer(IAsyncDocumentSession session)
        {
            var customer = new Customer
            {
                Name = "Demo " + DateTime.Now.ToLongTimeString()
            };

            await session.StoreAsync(customer);
            await session.SaveChangesAsync();
        }
    }
}