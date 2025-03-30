using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Tests
{
    public class InMemoryRepositoryUnitTests
    {
        private readonly InMemoryBookRepository _bookRepository;
        private readonly Book _sampleBook;

        public InMemoryRepositoryUnitTests()
        {
            _bookRepository = new();
            _sampleBook = new() { Id = 1, Title = "Book 1", Author = "Author 1", ISBN = "123", PublicationYear = "2020" };
            ClearRepository();
        }

        private void ClearRepository()
        {
            // Remove all books by getting their IDs first
            var books = _bookRepository.GetAll().ToList();
            foreach (var book in books)
            {
                _bookRepository.Delete(book.Id);
            }
        }

        [Fact]
        public void GetAllBooks_ShouldReturnEmptyList_WhenNoBooksAdded()
        {
            // Act
            var result = _bookRepository.GetAll();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Add_ShouldAddBook_WithAutoIncrementedId()
        {
            // Act
            _bookRepository.Add(_sampleBook);
            var books = _bookRepository.GetAll().ToList();

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
                PublicationYear = "2022"
            };

            // Act
            _bookRepository.Add(_sampleBook);
            _bookRepository.Add(book2);
            var books = _bookRepository.GetAll().ToList();

            // Assert
            Assert.Equal(2, books.Count);
            Assert.Equal(1, books[0].Id);
            Assert.Equal(2, books[1].Id);
        }

        [Fact]
        public void GetById_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            _bookRepository.Add(_sampleBook);
            var addedBook = _bookRepository.GetAll().First();

            // Act
            var result = _bookRepository.GetById(addedBook.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(addedBook.Id, result.Id);
            Assert.Equal(_sampleBook.Title, result.Title);
        }

        [Fact]
        public void GetById_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Act
            var result = _bookRepository.GetById(999); // Non-existent ID

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void UpdateBook_ShouldModifyExistingBook()
        {
            // Arrange
            _bookRepository.Add(_sampleBook);
            var addedBook = _bookRepository.GetAll().First();
            var UpdateBook = new Book
            {
                Id = addedBook.Id,
                Title = "UpdateBookd Title",
                Author = "UpdateBookd Author",
                ISBN = "978-0743273565",
                PublicationYear = "2024"
            };

            // Act
            _bookRepository.Update(UpdateBook);
            var result = _bookRepository.GetById(addedBook.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UpdateBook.Title, result.Title);
            Assert.Equal(UpdateBook.Author, result.Author);
            Assert.Equal(UpdateBook.ISBN, result.ISBN);
            Assert.Equal(UpdateBook.PublicationYear, result.PublicationYear);
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
                PublicationYear = ""
            };

            // Act & Assert (should not throw)
            _bookRepository.Update(nonExistentBook);
        }

        [Fact]
        public void Delete_ShouldRemoveBook_WhenBookExists()
        {
            // Arrange
            _bookRepository.Add(_sampleBook);
            var addedBook = _bookRepository.GetAll().First();

            // Act
            _bookRepository.Delete(addedBook.Id);
            var result = _bookRepository.GetById(addedBook.Id);

            // Assert
            Assert.Null(result);
            Assert.Empty(_bookRepository.GetAll());
        }

        [Fact]
        public void Delete_ShouldNotThrow_WhenBookDoesNotExist()
        {
            // Act & Assert (should not throw)
            _bookRepository.Delete(999); // Non-existent ID
        }

        [Fact]
        public void Exists_ShouldReturnTrue_WhenBookExists()
        {
            // Arrange
            _bookRepository.Add(_sampleBook);
            var addedBook = _bookRepository.GetAll().First();

            // Act
            var result = _bookRepository.Exists(addedBook.Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Exists_ShouldReturnFalse_WhenBookDoesNotExist()
        {
            // Act
            var result = _bookRepository.Exists(999); // Non-existent ID

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetAllBooks_ShouldReturnAllBooks_InOrderOfAddition()
        {
            // Arrange
            var book1 = new Book { Title = "First Book", Author = "Author 1", ISBN = "1111111111", PublicationYear = "2020" };
            var book2 = new Book { Title = "Second Book", Author = "Author 2", ISBN = "2222222222", PublicationYear = "2021" };
            var book3 = new Book { Title = "Third Book", Author = "Author 3", ISBN = "3333333333", PublicationYear = "2022" };

            // Act
            _bookRepository.Add(book1);
            _bookRepository.Add(book2);
            _bookRepository.Add(book3);
            var result = _bookRepository.GetAll().ToList();

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
            var initialBook = new Book { Title = "Initial Book", Author = "Initial Author", ISBN = "4444444444", PublicationYear = "2019" };
            _bookRepository.Add(initialBook);
            var initialCount = _bookRepository.GetAll().Count();

            // Act
            _bookRepository.Add(_sampleBook);
            var books = _bookRepository.GetAll().ToList();

            // Assert
            Assert.Equal(initialCount + 1, books.Count);
            Assert.Contains(books, b => b.Title == initialBook.Title);
            Assert.Contains(books, b => b.Title == _sampleBook.Title);
        }
    }
}