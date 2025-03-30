using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book? GetById(int id);
        void Add(Book entity);
        void Update(Book entity);
        void Delete(int id);
        bool Exists(int id);
    }
}
