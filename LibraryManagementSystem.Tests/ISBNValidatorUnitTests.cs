using LibraryManagementSystem.Validators;

namespace LibraryManagementSystem.Tests
{
    public class ISBNValidatorUnitTests
    {
        private readonly ISBNValidator _isbnValidator;

        public ISBNValidatorUnitTests()
        {
            _isbnValidator = new ISBNValidator();
        }

        [Theory]
        [InlineData("0-306-40615-2", true)] // Valid ISBN-10
        [InlineData("0306406152", true)] // Valid ISBN-10 without dashes
        [InlineData("0-306-40615-X", false)] // Invalid ISBN-10
        [InlineData("978-3-16-148410-0", true)] // Valid ISBN-13
        [InlineData("9783161484100", true)] // Valid ISBN-13 without dashes
        [InlineData("978-3-16-148410-1", false)] // Invalid ISBN-13
        [InlineData("", false)] // Empty string
        [InlineData(" ", false)] // Whitespace
        [InlineData("123456789", false)] // Invalid length
        [InlineData("12345678901234", false)] // Invalid length
        public void IsValid_ShouldReturnExpectedResult(string isbn, bool expected)
        {
            // Act
            var result = _isbnValidator.IsValid(isbn);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
