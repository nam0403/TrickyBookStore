using System;
using System.Collections.Generic;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Books;


namespace TrickyBookStore.Services.PurchaseTransactions
{
    internal class PurchaseTransactionService : IPurchaseTransactionService
    {
        IBookService BookService { get; }

        readonly IList<PurchaseTransaction> allPurchaseTransactionsFromStore = (IList<PurchaseTransaction>)Store.PurchaseTransactions.Data;
        public PurchaseTransactionService(IBookService bookService)
        {
            BookService = bookService;
        }

        public IList<PurchaseTransaction> GetPurchaseTransactions(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            IList<PurchaseTransaction> customerTransactions = new List<PurchaseTransaction>();
            foreach (var transaction in allPurchaseTransactionsFromStore)
            {
                if (transaction.CustomerId == customerId)
                {
                    customerTransactions.Add(transaction);
                }
            }
            return customerTransactions;
            throw new NotImplementedException();
        }
    }
}
