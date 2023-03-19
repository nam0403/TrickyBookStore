using System.Collections.Generic;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Store
{
    public static class BookCategories
    {
        public static readonly IEnumerable<BookCategory> Data = new List<BookCategory>
        {
            new BookCategory{ Id = 1, Title = "History"},
            new BookCategory{ Id = 2, Title = "Geography"}, 
            new BookCategory{ Id = 3, Title = "Nature" },
            new BookCategory{ Id = 4, Title = "Math" }, 
            new BookCategory{ Id = 5, Title = "Biology" },
            new BookCategory{ Id = 6, Title = "Novel" }
        };
    }
}
