using System.Reflection;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public class BookService(IBookRepository<Book> bookRepository, IBookValidator bookValidator) : IBookService
    {
        private readonly IBookRepository<Book> _bookRepository = bookRepository;
        private readonly IBookValidator _bookValidator = bookValidator;

        public IEnumerable<Book> GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks();

            if (books == null || !books.Any())
            {
                throw new ArgumentException("No book found.");
            }

            return books;
        }

        public Book? GetBookById(int id)
        {
            EnsureBookIdExists(id);
            return _bookRepository.GetBookById(id);
        }

        public void AddBook(Book book)
        {
            _bookValidator.ValidateBook(book);
            _bookRepository.AddBook(book);
        }
        public void UpdateBook(Book book)
        {
            EnsureBookIdExists(book.Id);
            _bookValidator.ValidateBook(book);
            _bookRepository.UpdateBook(book);
        }
        public void DeleteBook(int id)
        {
            EnsureBookIdExists(id);
            _bookRepository.DeleteBook(id);
        }
        public void EnsureBookIdExists(int id)
        {
            if (!BookExists(id))
            {
                throw new ArgumentException($"The specified Book ID not found.");
            }
        }

        public bool BookExists(int id)
        {
            return _bookRepository.BookExists(id);
        }
    }
}
