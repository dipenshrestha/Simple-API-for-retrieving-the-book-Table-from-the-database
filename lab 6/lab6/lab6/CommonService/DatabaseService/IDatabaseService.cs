//IDatabaseService.cs
using lab6.Models;

namespace lab6.CommonService.DatabaseService
{
    public interface IDatabaseService
    {
        IReadOnlyList<Book> GetBooks();
        Book GetBookById(int id);
        string AddBook(Book book);
    }
}
