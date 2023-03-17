using System;
using System.Runtime.CompilerServices;
using TrickyBookStore.Models;
using TrickyBookStore.Services;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.Payment;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.Subscriptions;

namespace TrickyBookStore.ServiceCollections
{
    public class ServiceCollections
    {
        IPaymentService paymentService = new PaymentService(new CustomerService(new SubscriptionService()), new PurchaseTransactionService(new BookService()), new SubscriptionService(), new BookService());
        public double GetCustomerPayment(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            double customerPayment = paymentService.GetPaymentAmount(customerId, fromDate, toDate);
            Console.Write(customerPayment);
            return customerPayment;
            throw new NotImplementedException();
        }
    }
}
