using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface IBookRepository<T> where T : IBookEntity
    {
        IEnumerable<T> GetAllBooks();
        T? GetBookById(int id);
        void AddBook(T entity);
        void UpdateBook(T entity);
        void DeleteBook(int id);
        bool BookExists(int id);
    }
}
