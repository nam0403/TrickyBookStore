using System;
using System.Collections.Generic;
using System.Linq;
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
            IList<PurchaseTransaction> customerTransactions = allPurchaseTransactionsFromStore.Where(
                transaction =>  transaction.CustomerId == customerId &&
                                transaction.CreatedDate >= fromDate &&
                                transaction.CreatedDate <= toDate).ToList();
            return customerTransactions;
            throw new NotImplementedException();
        }
    }
}
