using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public class BookService(IBookRepository bookRepository, IBookValidator bookValidator) : IBookService
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IBookValidator _bookValidator = bookValidator;

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }

        public Book? GetBookById(int id)
        {
            EnsureBookIdExists(id);
            return _bookRepository.GetById(id);
        }

        public void AddBook(Book book)
        {
            _bookValidator.ValidateBook(book);
            _bookRepository.Add(book);
        }
        public void UpdateBook(Book book)
        {
            EnsureBookIdExists(book.Id);
            _bookValidator.ValidateBook(book);
            _bookRepository.Update(book);
        }
        public void DeleteBook(int id)
        {
            EnsureBookIdExists(id);
            _bookRepository.Delete(id);
        }
        private void EnsureBookIdExists(int id)
        {
            if (!BookExists(id))
            {
                throw new ArgumentException($"The specified Book ID not found.");
            }
        }
        private bool BookExists(int id)
        {
            return _bookRepository.Exists(id);
        }
    }
}
