namespace LibraryManagementSystem.Utilities
{
    public static class Utility
    {
        public static void WaitForUserInput()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.ResetColor();
        }

        public static string ConsoleReadLine()
        {
            string? input = Console.ReadLine();
            return !string.IsNullOrWhiteSpace(input) ? input : string.Empty;
        }

        public static void ConsoleWriteRedLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ConsoleWriteGreenLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ConsoleWriteYellowLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ConsoleWriteCyanLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static string ReadAndUpdateProperty(string propertyName, string currentValue)
        {
            Console.WriteLine($"Enter the book {propertyName}:");
            var newValue = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newValue))
            {
                Console.WriteLine($"Keeping existing {propertyName}: {currentValue}\n");
                return currentValue;
            }
            return newValue;
        }
    }
}
