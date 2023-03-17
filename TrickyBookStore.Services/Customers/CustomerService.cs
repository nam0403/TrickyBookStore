using System;
using System.Collections.Generic;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Subscriptions;
using TrickyBookStore.Services.Store;
using System.Linq;

namespace TrickyBookStore.Services.Customers
{
    internal class CustomerService : ICustomerService
    {
        ISubscriptionService SubscriptionService { get; }
        readonly IList<Customer> allCustomers = (IList<Customer>)Store.Customers.Data;
        public CustomerService(ISubscriptionService subscriptionService)
        {
            SubscriptionService = subscriptionService;
        }
        public Customer GetCustomerById(long id)
        {
            foreach (var customer in allCustomers)
            {
                if (customer.Id == id)
                {
                    return customer;
                }
            }
            throw new NotImplementedException();
        }
    }
}
