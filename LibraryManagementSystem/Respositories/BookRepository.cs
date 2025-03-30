using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Respositories
{
    public class BookRepository : IBookRepository<Book>
    {
        private readonly Dictionary<int, Book> _books;
        private int _nextId = 1;

        public BookRepository() => _books = new Dictionary<int, Book>();

        public void AddBook(Book book)
        {
            book.Id = _nextId++;
            _books.Add(book.Id, book);
        }

        public void UpdateBook(Book book)
        {
            if (_books.ContainsKey(book.Id))
            {
                _books[book.Id] = book;
            }
        }

        public void DeleteBook(int id)
        {
            _ = BookExists(id) && _books.Remove(id);
        }
        public bool BookExists(int id)
        {
            return _books.ContainsKey(id);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books.Values;
        }

        public Book? GetBookById(int id)
        {
            return BookExists(id) ? _books[id] : null!;
        }
    }
}
