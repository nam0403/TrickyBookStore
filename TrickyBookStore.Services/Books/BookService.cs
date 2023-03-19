using System;
using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Store;
namespace TrickyBookStore.Services.Books
{
    internal class BookService : IBookService
    {
        readonly IList<Book> allBooks = (IList<Book>)Store.Books.Data;
        public IList<Book> GetBooks(params long[] ids)
        {
            IList<Book> books = new List<Book>();
            books = allBooks.Where(book => ids.Contains(book.Id)).ToList();
            return books;
            throw new NotImplementedException();
        }
    }
}
