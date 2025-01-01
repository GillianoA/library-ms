using System;
using System.Collections.Generic;

class LibraryManager
{
    static void Main()
    {
        string[] books = new string[5];
        Dictionary<string, List<string>> userBorrowedBooks = new Dictionary<string, List<string>>();

        while (true)
        {
            Console.WriteLine("Would you like to add, remove, search, borrow, check-in, or exit? (add/remove/search/borrow/check-in/exit)");
            string action = Console.ReadLine()?.Trim().ToLower();

            switch (action)
            {
                case "add":
                    AddBook(books);
                    break;
                case "remove":
                    RemoveBook(books);
                    break;
                case "search":
                    SearchBook(books);
                    break;
                case "borrow":
                    BorrowBook(books, userBorrowedBooks);
                    break;
                case "check-in":
                    CheckInBook(books, userBorrowedBooks);
                    break;
                case "exit":
                    Console.WriteLine("Exiting the program. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid action. Please type 'add', 'remove', 'search', 'borrow', 'check-in' or 'exit'.");
                    break;
            }
            DisplayBooks(books);
        }
    }

    static void AddBook(string[] books)
    {
        Console.WriteLine("Enter the title of the book to add:");
        string newBook = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(newBook))
        {
            Console.WriteLine("Invalid input. Book title cannot be empty or whitespace.");
            return;
        }

        foreach (string book in books)
        {
            if (book?.Equals(newBook, StringComparison.OrdinalIgnoreCase) == true)
            {
                Console.WriteLine($"The book '{newBook}' is already in the library.");
                return;
            }
        }

        bool added = false;
        for (int i = 0; i < books.Length; i++)
        {
            if (string.IsNullOrEmpty(books[i]))
            {
                books[i] = newBook;
                Console.WriteLine($"'{newBook}' has been added to the library.");
                added = true;
                break;
            }
        }

        if (!added)
        {
            Console.WriteLine("The library is full. No more books can be added.");
        }
    }

    static void RemoveBook(string[] books)
    {
        Console.WriteLine("Enter the title of the book to remove:");
        string removeBook = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(removeBook))
        {
            Console.WriteLine("Invalid input. Book title cannot be empty or whitespace.");
            return;
        }

        bool removed = false;
        for (int i = 0; i < books.Length; i++)
        {
            if (books[i]?.Equals(removeBook, StringComparison.OrdinalIgnoreCase) == true)
            {
                books[i] = "";
                Console.WriteLine($"'{removeBook}' has been removed from the library.");
                removed = true;
                break;
            }
        }

        if (!removed)
        {
            Console.WriteLine($"'{removeBook}' not found in the library.");
        }
    }

    static void SearchBook(string[] books){
        Console.Write("What book are you looking for: ");
        string searchBook = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(searchBook)){
            Console.WriteLine("Invalid input. Book title cannot be empty or whitespace.");
            return;
        }

        foreach(string book in books){
            if(book?.Equals(searchBook, StringComparison.OrdinalIgnoreCase) == true){
                Console.WriteLine($"'{searchBook}' is available in the library.");
                return;
            }
        }

        Console.WriteLine($"'{searchBook}' was not found in the collection.");
    }

    static void BorrowBook(string[] books, Dictionary<string, List<string>> userBorrowedBooks){
        Console.WriteLine("Enter your name:");
        string userName = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(userName)){
            Console.WriteLine("Invalid input. Name cannot be empty or whitespace.");
            return;
        }

        if (!userBorrowedBooks.ContainsKey(userName)){
            userBorrowedBooks[userName] = new List<string>();
        }

        if (userBorrowedBooks[userName].Count >= 3){
            Console.WriteLine("You have already borrowed the maximum of 3 books. You cannot borrow more.");
            return;
        }

        Console.WriteLine("Enter the title of the book to borrow:");
        string borrowBook = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(borrowBook)){
            Console.WriteLine("Invalid input. Book title cannot be empty or whitespace.");
            return;
        }

        for (int i = 0; i < books.Length; i++){
            if (books[i]?.Equals(borrowBook, StringComparison.OrdinalIgnoreCase) == true){
                userBorrowedBooks[userName].Add(borrowBook);
                books[i] = "";
                Console.WriteLine($"'{borrowBook}' has been borrowed by {userName}.");
                return;
            }
        }

        Console.WriteLine($"'{borrowBook}' is not available in the library.");
    }

    static void CheckInBook(string[] books, Dictionary<string, List<string>> userBorrowedBooks){
        Console.WriteLine("Enter your name:");
        string userName = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(userName) || !userBorrowedBooks.ContainsKey(userName) || userBorrowedBooks[userName].Count == 0){
            Console.WriteLine("You have no borrowed books to check in.");
            return;
        }

        Console.WriteLine("Enter the title of the book to check in:");
        string checkInBook = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(checkInBook)){
            Console.WriteLine("Invalid input. Book title cannot be empty or whitespace.");
            return;
        }

        if (userBorrowedBooks[userName].Contains(checkInBook)){
            userBorrowedBooks[userName].Remove(checkInBook);

            for (int i = 0; i < books.Length; i++){
                if (string.IsNullOrEmpty(books[i])){
                    books[i] = checkInBook;
                    Console.WriteLine($"'{checkInBook}' has been checked in and is now available in the library.");
                    return;
                }
            }

            Console.WriteLine("Library is full. Book cannot be added back to the collection but is marked as returned.");
        }
        else{
            Console.WriteLine($"'{checkInBook}' was not borrowed by you.");
        }
    }
    static void DisplayBooks(string[] books)
    {
        Console.WriteLine("\nAvailable books:");
        bool hasBooks = false;

        foreach (string book in books)
        {
            if (!string.IsNullOrEmpty(book))
            {
                Console.WriteLine($"- {book}");
                hasBooks = true;
            }
        }

        if (!hasBooks)
        {
            Console.WriteLine("No books available.");
        }

        Console.WriteLine();
    }
}