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
                
                return CalculateTotalPayment(customerSubcription, customerTransactions);
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
                    switch (subscription.SubscriptionType)
                    {
                        case SubscriptionTypes.Free:
                            if (!book.IsOld)
                                bookPayment += CalculateBookPrice(book, subscription.PriceDetails["DiscountBuyNewBook"]);
                            else
                                bookPayment += CalculateBookPrice(book, subscription.PriceDetails["DiscountBuyOldBook"]);
                            break;
                        case SubscriptionTypes.Paid:
                            if (!book.IsOld)
                                bookPayment += book.Price;
                            else 
                                bookPayment += book.Price * subscription.PriceDetails["FeeCharge"];
                            break;
                        case SubscriptionTypes.CategoryAddicted:
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

        public double CalculateTotalPayment(IList<Subscription> customerSubscriptions, IList<PurchaseTransaction> customerTransactions)
        {
            double subscriptionPayment = 0;
            double bookPayment = 0;
            var customerPurchaseBooks = SortBook(customerTransactions);

            // Iterate over a copy of the customerSubscriptions list, so we can remove items from the original list safely
            foreach (var subscription in customerSubscriptions)
            {
                subscriptionPayment += subscription.PriceDetails["FixPrice"];
                bookPayment += CalculateBookPayment(subscription, customerPurchaseBooks, bookPayment);
                Console.WriteLine("Subcription Name: " + subscription.SubscriptionType);
                Console.WriteLine("Book payment is " + bookPayment);
                Console.WriteLine("Customer subscription price is " + subscriptionPayment);
                Console.WriteLine("The remain book is " + customerPurchaseBooks.Count);
                foreach (var purchaseTransaction in customerTransactions)
                {
                    Book book = new Book();
                    book = customerPurchaseBooks.FirstOrDefault(book_test => book_test.Id == purchaseTransaction.BookId);
                    customerPurchaseBooks.Remove(book);
                }
                /*customerSubscriptions.Remove(subscription);*/
            }

            
            return subscriptionPayment + bookPayment;
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

        public IList<Book> SortBook(IList<PurchaseTransaction> customerTransactions)
        {
            var bookIds = GetBooksId(customerTransactions);
            IList<Book> books = BookService.GetBooks(bookIds.ToArray());
            // Create a dictionary that maps book IDs to their latest purchase date
            var latestPurchaseDates = customerTransactions
                .GroupBy(transaction => transaction.BookId)
                .ToDictionary(group => group.Key, group => group.Max(transaction => transaction.CreatedDate));

            // Sort the books based on their latest purchase date (if available)
            var sortedBooks = books
                .OrderBy(book => latestPurchaseDates.ContainsKey(book.Id) ? latestPurchaseDates[book.Id] : DateTimeOffset.MinValue)
                .ToList();

            return sortedBooks;
        }
        

    }
}
