using LibraryManagementSystem.Interfaces;

namespace LibraryManagementSystem.Validators
{
    public class ISBNValidator : IBookNumberValidator
    {
        /// <summary>
        /// Validates both ISBN-10 and ISBN-13 formats
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public bool IsValid(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn)) return false;

            isbn = isbn.Trim().Replace("-", string.Empty).Replace(" ", string.Empty);

            if (!isbn.All(char.IsDigit)) return false;

            return isbn.Length switch
            {
                10 => IsValidISBN10DigitsFormat(isbn),
                13 => IsValidISBN13DigitsFormat(isbn),
                _ => false
            };
        }

        /// <summary>
        /// Validates ISBN-10 format
        /// Credits: https://en.wikipedia.org/wiki/International_Standard_Book_Number#ISBN-10_check_digits
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        private bool IsValidISBN10DigitsFormat(string isbn)
        {
            int isbnSum = Enumerable.Range(0, 10)
                .Select(i => int.Parse(isbn[i].ToString()) * (10 - i))
                .Sum();

            return isbnSum % 11 == 0;
        }

        /// <summary>
        /// Validates ISBN-13 format
        /// Credits: https://en.wikipedia.org/wiki/International_Standard_Book_Number#ISBN-13_check_digit_calculation
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        private bool IsValidISBN13DigitsFormat(string isbn)
        {
            int isbnSum = Enumerable.Range(0, 12)
                .Select(i => int.Parse(isbn[i].ToString()) * (i % 2 == 0 ? 1 : 3))
                .Sum();

            return (10 - isbnSum % 10) % 10 == int.Parse(isbn[12].ToString());
        }
    }
}
