using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using Moq;

namespace LibraryManagementSystem.Tests
{
    public class BookServiceUnitTests
    {
        private readonly BookService _bookService;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IBookValidator> _bookValidatorMock;
        public BookServiceUnitTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _bookValidatorMock = new Mock<IBookValidator>();
            _bookService = new BookService(_bookRepositoryMock.Object, _bookValidatorMock.Object);
        }


        [Fact]
        public void AddBook_ShouldCallRepositoryAddBook()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book", Author = "Test Author", ISBN = "1234567890", PublisherYear = "2023" };

            // Act
            _bookService.AddBook(book);

            // Assert
            _bookRepositoryMock.Verify(repo => repo.Add(book), Times.Once);
        }

        [Fact]
        public void GetAllBooks_ShouldReturnAllBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new() { Id = 1, Title = "Test Book 1", Author = "Test Author 1", ISBN = "1234567890", PublisherYear = "2023" },
                new() { Id = 2, Title = "Test Book 2", Author = "Test Author 2", ISBN = "0987654321", PublisherYear = "2022" }
            };
            _bookRepositoryMock.Setup(repo => repo.GetAll()).Returns(books);

            // Act
            var result = _bookService.GetAllBooks();

            // Assert
            Assert.Equal(books, result);
        }

        [Fact]
        public void GetBookById_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book", Author = "Test Author", ISBN = "1234567890", PublisherYear = "2023" };
            _bookRepositoryMock.Setup(repo => repo.Exists(book.Id)).Returns(true);
            _bookRepositoryMock.Setup(repo => repo.GetById(1)).Returns(book);

            // Act
            var result = _bookService.GetBookById(1);

            // Assert
            Assert.Equal(book, result);
        }

        [Fact]
        public void GetBookById_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Arrange
            _bookRepositoryMock.Setup(repo => repo.Exists(1)).Returns(true);
            _bookRepositoryMock.Setup(repo => repo.GetById(1)).Returns((Book)null!);

            // Act
            var result = _bookService.GetBookById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void UpdateBook_ShouldCallRepositoryUpdateBook()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Updated Book", Author = "Updated Author", ISBN = "1234567890", PublisherYear = "2023" };
            _bookRepositoryMock.Setup(repo => repo.Exists(book.Id)).Returns(true);

            // Act
            _bookService.UpdateBook(book);

            // Assert
            _bookRepositoryMock.Verify(repo => repo.Update(book), Times.Once);
        }

        [Fact]
        public void DeleteBook_ShouldCallRepositoryDeleteBook()
        {
            // Arrange
            var bookId = 1;
            _bookRepositoryMock.Setup(repo => repo.Exists(bookId)).Returns(true);

            // Act
            _bookService.DeleteBook(bookId);

            // Assert
            _bookRepositoryMock.Verify(repo => repo.Delete(bookId), Times.Once);
        }

        [Fact]
        public void BookExists_ShouldReturnTrue_WhenBookExists()
        {
            // Arrange
            var bookId = 1;
            _bookRepositoryMock.Setup(repo => repo.Exists(bookId)).Returns(true);

            // Act
            var result = _bookService.BookExists(bookId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void BookExists_ShouldReturnFalse_WhenBookDoesNotExist()
        {
            // Arrange
            var bookId = 1;
            _bookRepositoryMock.Setup(repo => repo.Exists(bookId)).Returns(false);

            // Act
            var result = _bookService.BookExists(bookId);

            // Assert
            Assert.False(result);
        }
    }
}
