using System;
using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.Store;
using TrickyBookStore.Services.Subscriptions;

namespace TrickyBookStore.Services.Payment
{ 
    internal class PaymentService : IPaymentService
    {
        ICustomerService CustomerService { get; }
        IPurchaseTransactionService PurchaseTransactionService { get; }
        ISubscriptionService SubscriptionService { get; }
        IBookService BookService { get; }
        public PaymentService(ICustomerService customerService, 
            IPurchaseTransactionService purchaseTransactionService , ISubscriptionService subscriptionService , IBookService bookService)
        {
            CustomerService = customerService;
            PurchaseTransactionService = purchaseTransactionService;
            SubscriptionService = subscriptionService;
            BookService = bookService;
        }

        public double GetPaymentAmount(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            IList<PurchaseTransaction> customerTransactions = PurchaseTransactionService.GetPurchaseTransactions(customerId, fromDate, toDate);
            Customer customer = CustomerService.GetCustomerById(customerId);
            int[] ids = customer.SubscriptionIds.ToArray();
            IList<Subscription> customerSubcription = SubscriptionService.GetSubscriptions(ids);
            List<long> bookIds = new List<long>();
            double sum = 0;
            foreach (PurchaseTransaction transaction in customerTransactions)
            {
                bookIds.Add(transaction.BookId);
            }
            IList<Book> books = new List<Book>();
            books = BookService.GetBooks(bookIds.ToArray());
            foreach (Book book in books)
            {
                sum += book.Price;
            }
            return sum;
            throw new NotImplementedException();
        }
    }
}
