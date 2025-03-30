using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Utilities;
using Serilog;

namespace LibraryManagementSystem
{
    public class LibraryManagementSystemApp(IBookService bookService, ILogger logger)
    {
        private readonly IBookService _bookService = bookService;
        private readonly ILogger _logger = logger;

        public void Run()
        {
            // Populate initial books data
            PopulateBooksData();

            // Show menu options
            ShowMenuOptions();
        }

        private void ShowMenuOptions()
        {
            while (true)
            {
                const string menuOptions = "\n1. Add a new book.\n2. Update an existing book.\n3. Delete a book.\n" +
                    "4. List all books.\n5. View details of a specific book.\n6. Exit.";

                Console.Title = "Library Management System";
                Console.Clear();
                Utility.ConsoleWriteYellowLine("Welcome to Library Management System");
                Utility.ConsoleWriteYellowLine("=====================================");
                Console.WriteLine("Choose an option number and press enter:");
                Console.WriteLine(menuOptions);


                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        UpdateBook();
                        break;
                    case "3":
                        DeleteBook();
                        break;
                    case "4":
                        ListAllBooks();
                        break;
                    case "5":
                        GetBookById();
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    default:
                        Utility.ConsoleWriteRedLine("Invalid option. Please try again.");
                        Utility.WaitForUserInput();
                        break;
                }
            }
        }

        private void PopulateBooksData()
        {
            var books = new List<Book>
        {
            new() {
                Title = "Clean Code",
                Author = "Robert C. Martin",
                ISBN = "978-0132350884",
                PublicationYear = "2008"
            },
            new() {
                Title = "Building Microservices",
                Author = "Sam Newman",
                ISBN = "978-1491950357",
                PublicationYear = "2015"
            },
            new()
            {
                Title = "The Pragmatic Programmer",
                Author = "Andrew Hunt",
                ISBN = "978-0201616224",
                PublicationYear = "1999"
            },
            new()
            {
                Title = "Software Engineering",
                Author = "Sommerville, Ian",
                ISBN = "0133943038",
                PublicationYear = "2015"
            },
        };

            try
            {
                books.ForEach(book => _bookService?.AddBook(book));
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"Operation: {nameof(PopulateBooksData)}");
                Utility.ConsoleWriteRedLine($"Error populating Books data: {exception.Message}.");
            }
        }

        void AddBook()
        {
            try
            {
                Console.Clear();
                Utility.ConsoleWriteYellowLine("Add a book (* denotes required fields)");
                Utility.ConsoleWriteYellowLine("======================================");

                Book book = new()
                {
                    Title = Utility.ReadAndAddProperty($"{nameof(book.Title)}"),
                    Author = Utility.ReadAndAddProperty($"{nameof(book.Author)}"),
                    ISBN = Utility.ReadAndAddProperty($"{nameof(book.ISBN)}"),
                    PublicationYear = Utility.ReadAndAddProperty($"{nameof(book.PublicationYear)}")
                };

                _bookService?.AddBook(book);
                Utility.ConsoleWriteGreenLine("\nBook added successfully.");
            }
            catch (ArgumentException exception) 
            {
                Utility.ConsoleWriteRedLine($"\nBook cannot be added. Error: {exception.Message}.");
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"Operation: {nameof(AddBook)}");
            }
            finally
            {
                Utility.WaitForUserInput();
            }
        }

        void UpdateBook()
        {
            try
            {
                Console.Clear();
                Utility.ConsoleWriteYellowLine("\nUpdate a book");
                Utility.ConsoleWriteYellowLine("================");

                Console.WriteLine("\nEnter the Book ID to update:");

                _ = int.TryParse(Console.ReadLine(), out int id);
                var bookToUpdate = _bookService?.GetBookById(id);

                if (bookToUpdate == null) return;

                Utility.ConsoleWriteCyanLine("\nEnter the book details to update or leave empty to keep existing value:");

                var updatedBook = new Book
                {
                    Id = bookToUpdate.Id,
                    Title = Utility.ReadAndUpdateProperty($"{nameof(bookToUpdate.Title)}", bookToUpdate.Title),
                    Author = Utility.ReadAndUpdateProperty($"{nameof(bookToUpdate.Author)}", bookToUpdate.Author),
                    ISBN = Utility.ReadAndUpdateProperty($"{nameof(bookToUpdate.ISBN)}", bookToUpdate.ISBN),
                    PublicationYear = Utility.ReadAndUpdateProperty($"{nameof(bookToUpdate.PublicationYear)}", bookToUpdate.PublicationYear)
                };

                if (bookToUpdate.Equals(updatedBook))
                {
                    Utility.ConsoleWriteYellowLine("\nNo changes detected. Book not updated.");
                    return;
                }

                _bookService?.UpdateBook(updatedBook);
                Utility.ConsoleWriteGreenLine("\nBook updated successfully.");
            }
            catch (ArgumentException exception)
            {
                Utility.ConsoleWriteRedLine($"Book cannot be updated. Error: {exception.Message}");
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"Operation: {nameof(UpdateBook)}");
            }
            finally
            {
                Utility.WaitForUserInput();
            }
        }

        void DeleteBook()
        {
            try
            {
                Console.Clear();
                Utility.ConsoleWriteYellowLine("\nDelete a book");
                Utility.ConsoleWriteYellowLine("===============");
                Console.WriteLine("Enter the Book ID to delete:");

                bool isNumber = int.TryParse(Console.ReadLine(), out int id);
                _bookService?.DeleteBook(id);
                
                Utility.ConsoleWriteGreenLine("Book deleted successfully.");
            }
            catch (ArgumentException exception)
            {
                Utility.ConsoleWriteRedLine($"Book cannot be deleted. Error: {exception.Message}");
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"Operation: {nameof(DeleteBook)}");
            }
            finally
            {
                Utility.WaitForUserInput();
            }
        }

        void ListAllBooks()
        {
            try
            {
                Console.Clear();
                Utility.ConsoleWriteYellowLine("List of all books");
                Utility.ConsoleWriteYellowLine("=================");
                var books = _bookService?.GetAllBooks();

                if (books == null) return;

                foreach (var book in books)
                {
                    Console.WriteLine(book.ToString());
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"Operation: {nameof(ListAllBooks)}");
                Utility.ConsoleWriteRedLine($"List of all books cannot be displayed. Error: {exception.Message}");
            }
            finally
            {
                Utility.WaitForUserInput();
            }
        }

        void GetBookById()
        {
            try
            {
                Console.Clear();
                Utility.ConsoleWriteYellowLine("View details of a specific book");
                Utility.ConsoleWriteYellowLine("================================");
                Console.WriteLine("\nEnter the Book ID:");

                bool isNumber = int.TryParse(Console.ReadLine(), out int id);
                var book = _bookService?.GetBookById(id);

                Console.WriteLine(book?.ToString());
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"Operation: {nameof(GetBookById)}");
                Utility.ConsoleWriteRedLine($"Specific book cannot be displayed. Error: {exception.Message}");
            }
            finally
            {
                Utility.WaitForUserInput();
            }
        }
    }
}
