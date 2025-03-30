// See https://aka.ms/new-console-template for more information
using LibraryManagementSystem;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Validators;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

internal class Program
{
    private static void Main()
    {
        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs\\log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        try
        {
            // Setup DI container
            var serviceProvider = new ServiceCollection()
            .AddSingleton<IBookNumberValidator, ISBNValidator>()
            .AddSingleton<IBookRepository, InMemoryBookRepository>()
            .AddSingleton<IBookService, BookService>()
            .AddSingleton<IBookValidator, BookValidator>()
            .AddSingleton<LibraryManagementSystemApp>()
            .AddLogging(configure => configure.AddSerilog(dispose: true))
            .AddSingleton(Log.Logger)
            .BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger>();
            logger.Information("Dependency injection configured. Application starting...");

            var libraryApp = serviceProvider.GetRequiredService<LibraryManagementSystemApp>();
            libraryApp.Run();
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "An unhandled exception occurred.");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}