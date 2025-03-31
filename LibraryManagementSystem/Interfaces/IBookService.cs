using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface IBookService
    {
        void AddBook(Book book);
        void DeleteBook(int id);
        IEnumerable<Book> GetAllBooks();
        Book? GetBookById(int id);
        void UpdateBook(Book book);
    }
}