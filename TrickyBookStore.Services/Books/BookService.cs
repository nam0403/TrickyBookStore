using System;
using System.Collections.Generic;
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
            foreach (var book in allBooks)
            {
                foreach (var id in ids)
                {
                    if (book.Id == id)
                    {
                        books.Add(book);
                    }

                }
            }
            return books;
            throw new NotImplementedException();
        }
    }
}
