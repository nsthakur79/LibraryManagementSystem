using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Validators;
using Moq;

namespace LibraryManagementSystem.Tests
{
    public class BookValidatorUnitTests
    {
        private readonly BookValidator _bookValidator;
        private readonly Mock<IBookNumberValidator> _bookNumberValidatorMock;

        public BookValidatorUnitTests()
        {
            _bookNumberValidatorMock = new Mock<IBookNumberValidator>();
            _bookValidator = new BookValidator(_bookNumberValidatorMock.Object);
        }

        [Fact]
        public void ValidateBook_ShouldThrowArgumentNullException_WhenBookIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _bookValidator.ValidateBook(null!));
        }

        [Theory]
        [InlineData("", "Author", "1234567890", "2023", "Title is required")]
        [InlineData("Title", "", "1234567890", "2023", "Author is required")]
        [InlineData("Title", "Author", "", "2023", "ISBN is required")]
        [InlineData("Title", "Author", "1234567890", "", "Publisher year is required")]
        [InlineData("Title", "Author", "InvalidISBN", "2023", "Invalid ISBN")]
        [InlineData("Title", "Author", "1234567890", "InvalidYear", "Publisher year must be a number")]
        public void ValidateBook_ShouldThrowArgumentException_WhenValidationFails(string title, string author, string isbn, string PublicationYear, string expectedMessage)
        {
            // Arrange
            var book = new Book { Title = title, Author = author, ISBN = isbn, PublicationYear = PublicationYear };
            _bookNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>())).Returns(isbn == "1234567890");

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _bookValidator.ValidateBook(book));
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void ValidateBook_ShouldNotThrowException_WhenBookIsValid()
        {
            // Arrange
            var book = new Book { Title = "Valid Title", Author = "Valid Author", ISBN = "1234567890", PublicationYear = "2023" };
            _bookNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>())).Returns(true);

            // Act & Assert
            var exception = Record.Exception(() => _bookValidator.ValidateBook(book));
            Assert.Null(exception);
        }
    }
}
