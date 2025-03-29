using LibraryManagementSystem.Models;
using LibraryManagementSystem.Respositories;

namespace LibraryManagementSystem.Tests
{
    public class BookRepositoryUnitTests
    {
        private readonly BookRepository _bookRepository;
        private readonly Book _sampleBook;

        public BookRepositoryUnitTests()
        {
            _bookRepository = new();
            _sampleBook = new() { Id = 1, Title = "Book 1", Author = "Author 1", ISBN = "123", PublisherYear = "2020" };
            ClearRepository();
        }

        private void ClearRepository()
        {
            // Remove all books by getting their IDs first
            var books = _bookRepository.GetAllBooks().ToList();
            foreach (var book in books)
            {
                _bookRepository.DeleteBook(book.Id);
            }
        }

        [Fact]
        public void GetAllBooks_ShouldReturnEmptyList_WhenNoBooksAdded()
        {
            // Act
            var result = _bookRepository.GetAllBooks();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Add_ShouldAddBook_WithAutoIncrementedId()
        {
            // Act
            _bookRepository.AddBook(_sampleBook);
            var books = _bookRepository.GetAllBooks().ToList();

            // Assert
            Assert.Single(books);
            Assert.Equal(1, books[0].Id);
            Assert.Equal(_sampleBook.Title, books[0].Title);
        }

        [Fact]
        public void Add_ShouldIncrementId_ForMultipleBooks()
        {
            // Arrange
            var book2 = new Book
            {
                Title = "Second Book",
                Author = "Another Author",
                ISBN = "978-0451524935",
                PublisherYear = "2022"
            };

            // Act
            _bookRepository.AddBook(_sampleBook);
            _bookRepository.AddBook(book2);
            var books = _bookRepository.GetAllBooks().ToList();

            // Assert
            Assert.Equal(2, books.Count);
            Assert.Equal(1, books[0].Id);
            Assert.Equal(2, books[1].Id);
        }

        [Fact]
        public void GetById_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            _bookRepository.AddBook(_sampleBook);
            var addedBook = _bookRepository.GetAllBooks().First();

            // Act
            var result = _bookRepository.GetBookById(addedBook.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(addedBook.Id, result.Id);
            Assert.Equal(_sampleBook.Title, result.Title);
        }

        [Fact]
        public void GetById_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Act
            var result = _bookRepository.GetBookById(999); // Non-existent ID

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void UpdateBook_ShouldModifyExistingBook()
        {
            // Arrange
            _bookRepository.AddBook(_sampleBook);
            var addedBook = _bookRepository.GetAllBooks().First();
            var UpdateBook = new Book
            {
                Id = addedBook.Id,
                Title = "UpdateBookd Title",
                Author = "UpdateBookd Author",
                ISBN = "978-0743273565",
                PublisherYear = "2024"
            };

            // Act
            _bookRepository.UpdateBook(UpdateBook);
            var result = _bookRepository.GetBookById(addedBook.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UpdateBook.Title, result.Title);
            Assert.Equal(UpdateBook.Author, result.Author);
            Assert.Equal(UpdateBook.ISBN, result.ISBN);
            Assert.Equal(UpdateBook.PublisherYear, result.PublisherYear);
        }

        [Fact]
        public void UpdateBook_ShouldNotThrow_WhenBookDoesNotExist()
        {
            // Arrange
            var nonExistentBook = new Book
            {
                Id = 999,
                Author = "",
                ISBN = "",
                Title = "",
                PublisherYear = ""
            };

            // Act & Assert (should not throw)
            _bookRepository.UpdateBook(nonExistentBook);
        }

        [Fact]
        public void Delete_ShouldRemoveBook_WhenBookExists()
        {
            // Arrange
            _bookRepository.AddBook(_sampleBook);
            var addedBook = _bookRepository.GetAllBooks().First();

            // Act
            _bookRepository.DeleteBook(addedBook.Id);
            var result = _bookRepository.GetBookById(addedBook.Id);

            // Assert
            Assert.Null(result);
            Assert.Empty(_bookRepository.GetAllBooks());
        }

        [Fact]
        public void Delete_ShouldNotThrow_WhenBookDoesNotExist()
        {
            // Act & Assert (should not throw)
            _bookRepository.DeleteBook(999); // Non-existent ID
        }

        [Fact]
        public void Exists_ShouldReturnTrue_WhenBookExists()
        {
            // Arrange
            _bookRepository.AddBook(_sampleBook);
            var addedBook = _bookRepository.GetAllBooks().First();

            // Act
            var result = _bookRepository.BookExists(addedBook.Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Exists_ShouldReturnFalse_WhenBookDoesNotExist()
        {
            // Act
            var result = _bookRepository.BookExists(999); // Non-existent ID

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetAllBooks_ShouldReturnAllBooks_InOrderOfAddition()
        {
            // Arrange
            var book1 = new Book { Title = "First Book", Author = "Author 1", ISBN = "1111111111", PublisherYear = "2020" };
            var book2 = new Book { Title = "Second Book", Author = "Author 2", ISBN = "2222222222", PublisherYear = "2021" };
            var book3 = new Book { Title = "Third Book", Author = "Author 3", ISBN = "3333333333", PublisherYear = "2022" };

            // Act
            _bookRepository.AddBook(book1);
            _bookRepository.AddBook(book2);
            _bookRepository.AddBook(book3);
            var result = _bookRepository.GetAllBooks().ToList();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(book1.Title, result[0].Title);
            Assert.Equal(book2.Title, result[1].Title);
            Assert.Equal(book3.Title, result[2].Title);
        }

        [Fact]
        public void Add_ShouldNotAffectOtherBooks_WhenAddingNewBook()
        {
            // Arrange
            var initialBook = new Book { Title = "Initial Book", Author = "Initial Author", ISBN = "4444444444", PublisherYear = "2019" };
            _bookRepository.AddBook(initialBook);
            var initialCount = _bookRepository.GetAllBooks().Count();

            // Act
            _bookRepository.AddBook(_sampleBook);
            var books = _bookRepository.GetAllBooks().ToList();

            // Assert
            Assert.Equal(initialCount + 1, books.Count);
            Assert.Contains(books, b => b.Title == initialBook.Title);
            Assert.Contains(books, b => b.Title == _sampleBook.Title);
        }
    }
}