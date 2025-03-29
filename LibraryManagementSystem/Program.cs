// See https://aka.ms/new-console-template for more information
using LibraryManagementSystem;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Respositories;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Validators;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main()
    {
        // Setup DI container
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IBookNumberValidator, ISBNValidator>()
            .AddSingleton<IBookRepository, BookRepository>()
            .AddSingleton<IBookService, BookService>()
            .AddSingleton<IBookValidator, BookValidator>()
            .AddSingleton<LibraryManagementSystemApp>()
            .BuildServiceProvider();

        // Resolve the BookService dependency and use it
        var libraryApp = serviceProvider.GetRequiredService<LibraryManagementSystemApp>();
        libraryApp.Run();
        
    }
}