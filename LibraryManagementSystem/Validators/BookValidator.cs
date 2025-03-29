using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Validators
{
    public class BookValidator(IBookNumberValidator bookNumberValidator) : IBookValidator
    {
        private readonly IBookNumberValidator _bookNumberValidator = bookNumberValidator;

        public void ValidateBook(Book book)
        {
            ArgumentNullException.ThrowIfNull(book);

            var validationRules = new List<Func<Book, (bool IsValid, string ErrorMessage)>>
            {
                b => (!string.IsNullOrWhiteSpace(b.Title), "Title is required"),
                b => (!string.IsNullOrWhiteSpace(b.Author), "Author is required"),
                b => (!string.IsNullOrWhiteSpace(b.ISBN), "ISBN is required"),
                b => (!string.IsNullOrWhiteSpace(b.PublisherYear), "Publisher year is required"),
                b => (_bookNumberValidator.IsValid(b.ISBN), "Invalid ISBN"),
                b => (b.PublisherYear.All(char.IsDigit), "Publisher year must be a number")
            };

            validationRules.ForEach(rule =>
            {
                var (IsValid, ErrorMessage) = rule(book);
                if (!IsValid)
                {
                    throw new ArgumentException(ErrorMessage);
                }
            });
        }                
    }
}
