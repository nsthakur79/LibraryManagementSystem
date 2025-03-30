# Library Management System

## Overview
The Library Management System is a .NET 8 application designed to manage books in a library. It includes functionalities such as adding, updating, deleting, and retrieving books. The project follows SOLID design principles and includes unit tests to ensure the reliability of the code.

## Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or later

## Getting Started

### Clone the Repository
First, clone the repository to your local machine:

### Build the Solution
Open the solution in Visual Studio:
1. Launch Visual Studio.
2. Open the `LibraryManagementSystem.sln` file.

Build the solution by selecting `Build > Build Solution` or pressing `Ctrl+Shift+B`.

### Run the Application
To run the application:
1. Set the `LibraryManagementSystem` project as the startup project.
2. Press `F5` or click the `Start` button to run the application.

### Run Unit Tests
To run the unit tests:
1. Open the Test Explorer in Visual Studio by selecting `Test > Test Explorer`.
2. Click `Run All` to execute all the unit tests.

## Project Structure
- `LibraryManagementSystem`: Contains the main application code.
- `LibraryManagementSystem.Interfaces`: Contains all the abstractions (interfaces) for repositories and validations.
- `LibraryManagementSystem.Models`: Contains the data models/entities.
- `LibraryManagementSystem.Resources`: Contains the static resource files for the application.`
- `LibraryManagementSystem.Repositories`: Contains the repository classes for data access.
- `LibraryManagementSystem.Services`: Contains the service classes implementing the business logic and validation.
- `LibraryManagementSystem.Utilities`: Contains utility classes for common operations. 
- `LibraryManagementSystem.Validators`: Contains the validator classes for data validation.
- `LibraryManagementSystem.Tests`: Contains the unit tests for the application.

## Key Classes and Interfaces
- `Book`: Represents a book entity.
- `IBookRepository`: Interface for book repository operations.
- `IBookValidator`: Interface for book validation.
- `BookService`: Service class implementing the business logic for managing books.
- `BookServiceUnitTests`: Unit tests for the `BookService` class.

## Design Decisions
- **SOLID Principles**: The project follows SOLID design principles to ensure that the code is maintainable, scalable, and testable.
- **Repository Pattern**: The project uses the repository pattern to separate the data access logic from the business logic. Implemented with an interface (IBookRepository) and concrete implementation (BookRepository). 
- **Dependency Injection**: The project uses dependency injection to inject dependencies into classes, making them more testable and decoupled.
- **Validation**: The project includes validation classes to validate the data before performing any operations. Comprehensive ISBN validation supporting both ISBN-10 and ISBN-13 formats.
- **Exception Handling**: The project includes exception handling to handle errors and exceptions gracefully.
- **Resource Files**: The project includes resource files for storing static data such as error messages and validation messages.
- - **Unit Testing**: The project includes unit tests to ensure the reliability of the code and to catch any regressions.


## License
This project is licensed under the MIT License. See the `LICENSE` file for details.

## Contact
For any questions or issues, please feel free to get in touch.
