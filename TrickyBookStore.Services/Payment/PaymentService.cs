using System;
using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.Store;
using TrickyBookStore.Services.Subscriptions;
using static System.Reflection.Metadata.BlobBuilder;

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
            try
            {
                IList<PurchaseTransaction> customerTransactions = PurchaseTransactionService.GetPurchaseTransactions(customerId, fromDate, toDate);
                Customer customer = CustomerService.GetCustomerById(customerId);
                IList<Subscription> customerSubcription = GetPriority(SubscriptionService.GetSubscriptions(customer.SubscriptionIds.ToArray()));
                var bookIds = GetBooksId(customerTransactions);
                IList<Book> books = BookService.GetBooks(bookIds.ToArray());
                double subcriptionPayment = 0;
                double bookPayment = 0;
                foreach (var sub in customerSubcription)
                {
                    subcriptionPayment += sub.PriceDetails["FixPrice"];
                }
                bookPayment = test(customerSubcription, books, bookPayment);
                return subcriptionPayment + bookPayment;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        private double CalculateBookPayment(Subscription subscription, IList<Book> books, double bookPayment)
        {
            double booklimit = subscription.PriceDetails["LimitBuyNewBook"];
            foreach (var book in books)
            {
                if (!book.IsOld && booklimit > 0)
                {
                    bookPayment += CalculateBookPrice(book, subscription.PriceDetails["DiscountBuyNewBook"]);
                    booklimit--;
                }
                else
                {
                    switch (subscription.SubscriptionType.ToString())
                    {
                        case "Free":
                            if (!book.IsOld)
                                bookPayment += CalculateBookPrice(book, subscription.PriceDetails["DiscountBuyNewBook"]);
                            else
                                bookPayment += CalculateBookPrice(book, subscription.PriceDetails["DiscountBuyOldBook"]);
                            break;
                        case "Paid":
                            if (!book.IsOld)
                                bookPayment += book.Price;
                            else 
                                bookPayment += book.Price * subscription.PriceDetails["FeeCharge"];
                            break;
                        case "CategoryAddicted":
                            if (book.CategoryId != subscription.BookCategoryId || !book.IsOld && book.CategoryId == subscription.BookCategoryId)
                            {
                                bookPayment += book.Price;
                            }
                            break;
                    }
                }
            }
            return bookPayment;
        }

        public double test(IList<Subscription> customerSubcription , IList<Book> books, double testPrice)
        {
            if (customerSubcription.Count == 1)
            {
                testPrice = CalculateBookPayment(customerSubcription.First(), books, testPrice);
            }
            else
            {
                foreach ( var subscription in  customerSubcription )
                {
                    testPrice += CalculateBookPayment(subscription, books, testPrice);
                    /*customerSubcription.Remove(subscription);*/
                }
            }
            return testPrice;
        }

        public double CalculateBookPrice(Book book, double discount)
        {
            return book.Price * (1 - discount);
        }

        public List<long> GetBooksId(IList<PurchaseTransaction> customerTransactions)
        {
            List<long> bookIds = new List<long>();
            foreach (PurchaseTransaction transaction in customerTransactions)
            {
                bookIds.Add(transaction.BookId);
            }
            return bookIds;
        }

        public IList<Subscription> GetPriority(IList<Subscription> subscriptions)
        {
            var orderPriority = subscriptions.OrderBy(sub => sub.Priority).ToList();
            return orderPriority;
        }
    }
}
