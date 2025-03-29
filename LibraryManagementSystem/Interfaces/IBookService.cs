using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface IBookService
    {
        void AddBook(Book book);
        bool BookExists(int id);
        void DeleteBook(int id);
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
        void UpdateBook(Book book);
    }
}