using System;
using System.Collections.Generic;

struct FullName
{
    public string Surname;
    public string Name;
    public string Patronymic;
}

class Reader
{
    public int Id;
    public FullName FullName;
    public int BirthYear;
    public string Address;
}

class Book
{
    public int Id;
    public string Title;
    public string Author;
    public int PublicationYear;
    public int PageCount;
    public List<BookMovement> Movements = new List<BookMovement>();
}

class BookMovement
{
    public DateTime IssueDate;
    public DateTime? ReturnDate;
    public int ReaderId;
}

class Program
{
    static List<Reader> readers = new List<Reader>();
    static List<Book> books = new List<Book>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nбиблоитека");
            Console.WriteLine("1 = Добавить читателя");
            Console.WriteLine("2 = Добавить книгу");
            Console.WriteLine("3 = Показать информацию о книгах и их движение");
            Console.WriteLine("4 = Показать несданные книги");
            Console.WriteLine("5 = Показать книги, которые никто не брал");
            Console.WriteLine("6 = Взять книгу");
            Console.WriteLine("7 = Сдать книгу");
            Console.WriteLine("0 = Выход");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddReader(); break;
                case "2": AddBook(); break;
                case "3": ShowBooksInfo(); break;
                case "4": ShowUnreturnedBooks(); break;
                case "5": ShowNeverBorrowedBooks(); break;
                case "6": BorrowBook(); break;
                case "7": ReturnBook(); break;
                case "0": return;
            }
        }
    }

    static void AddReader()
    {
        Reader reader = new Reader();

        Console.Write("Введите ID читателя: ");
        reader.Id = int.Parse(Console.ReadLine());

        FullName fn = new FullName();
        Console.Write("Введите фамилию: ");
        fn.Surname = Console.ReadLine();
        Console.Write("Введите имя: ");
        fn.Name = Console.ReadLine();
        Console.Write("Введите отчество: ");
        fn.Patronymic = Console.ReadLine();
        reader.FullName = fn;

        while (true)
        {
            Console.Write("Введите год рождения: ");
            string input = Console.ReadLine();
            bool isValid = true;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] < '0' || input[i] > '9')
                {
                    isValid = false;
                    break;
                }
            }
            if (isValid && input.Length > 0)
            {
                reader.BirthYear = int.Parse(input);
                break;
            }
            Console.WriteLine("Нужно ввести только цифры");
        }

        Console.Write("Введите адрес: ");
        reader.Address = Console.ReadLine();

        readers.Add(reader);
        Console.WriteLine("Ура, победа, читатель добвлен!");
    }

    static void AddBook()
    {
        Book book = new Book();

        Console.Write("Введите ID книги: ");
        int id = int.Parse(Console.ReadLine());

        for (int i = 0; i < books.Count; i++)
        {
            if (books[i].Id == id)
            {
                Console.WriteLine("Книга с таким ID уже существует!");
                return;
            }
        }
        book.Id = id;

        Console.Write("Введите наименование книги: ");
        book.Title = Console.ReadLine();

        Console.Write("Введите автора: ");
        book.Author = Console.ReadLine();

        while (true)
        {
            Console.Write("Введите год издания: ");
            string input = Console.ReadLine();
            bool isValid = true;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] < '0' || input[i] > '9')
                {
                    isValid = false;
                    break;
                }
            }
            if (isValid && input.Length > 0)
            {
                book.PublicationYear = int.Parse(input);
                break;
            }
            Console.WriteLine("Ошибка! Введите только цифры");
        }

        while (true)
        {
            Console.Write("Введите количество страниц: ");
            string input = Console.ReadLine();
            bool isValid = true;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] < '0' || input[i] > '9')
                {
                    isValid = false;
                    break;
                }
            }
            if (isValid && input.Length > 0)
            {
                book.PageCount = int.Parse(input);
                break;
            }
            Console.WriteLine("Ошибка! Введите только цифры");
        }

        books.Add(book);
        Console.WriteLine("Ура, победа, книга добавлена!");
    }

    static void ShowBooksInfo()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("Книг нет");
            return;
        }

        for (int i = 0; i < books.Count; i++)
        {
            Console.WriteLine("\n--- Книга ---");
            Console.WriteLine("Наименование: " + books[i].Title);
            Console.WriteLine("Автор: " + books[i].Author);

            if (books[i].Movements.Count == 0)
            {
                Console.WriteLine("Движение книги: нет записей");
            }
            else
            {
                Console.WriteLine("Движение книги:");
                for (int j = 0; j < books[i].Movements.Count; j++)
                {
                    BookMovement m = books[i].Movements[j];
                    string readerName = "Неизвестный";
                    for (int k = 0; k < readers.Count; k++)
                    {
                        if (readers[k].Id == m.ReaderId)
                        {
                            readerName = readers[k].FullName.Surname + " " +
                                         readers[k].FullName.Name + " " +
                                         readers[k].FullName.Patronymic;
                            break;
                        }
                    }

                    Console.Write("  Выдана: " + m.IssueDate.ToShortDateString());
                    if (m.ReturnDate != null)
                    {
                        Console.Write(", Сдана: " + m.ReturnDate.Value.ToShortDateString());
                    }
                    else
                    {
                        Console.Write(", Не сдана");
                    }
                    Console.WriteLine(", Читатель: " + readerName);
                }
            }
        }
    }

    static void ShowUnreturnedBooks()
    {
        bool found = false;
        for (int i = 0; i < books.Count; i++)
        {
            for (int j = 0; j < books[i].Movements.Count; j++)
            {
                if (books[i].Movements[j].ReturnDate == null)
                {
                    string readerName = "Неизвестный";
                    for (int k = 0; k < readers.Count; k++)
                    {
                        if (readers[k].Id == books[i].Movements[j].ReaderId)
                        {
                            readerName = readers[k].FullName.Surname + " " +
                                         readers[k].FullName.Name + " " +
                                         readers[k].FullName.Patronymic;
                            break;
                        }
                    }
                    Console.WriteLine("Книга: " + books[i].Title +
                                      " | Читатель: " + readerName +
                                      " | Выдана: " + books[i].Movements[j].IssueDate.ToShortDateString());
                    found = true;
                }
            }
        }
        if (!found)
        {
            Console.WriteLine("Все книги сданы");
        }
    }

    static void ShowNeverBorrowedBooks()
    {
        bool found = false;
        for (int i = 0; i < books.Count; i++)
        {
            if (books[i].Movements.Count == 0)
            {
                Console.WriteLine("Книга: " + books[i].Title + " (ID: " + books[i].Id + ")");
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("Все книги хотя бы раз были взяты");
        }
    }

    static void BorrowBook()
    {
        if (books.Count == 0 || readers.Count == 0)
        {
            Console.WriteLine("Нет книг или читателей в системе");
            return;
        }

        Console.Write("Введите ID читателя: ");
        int readerId = int.Parse(Console.ReadLine());

        bool readerExists = false;
        for (int i = 0; i < readers.Count; i++)
        {
            if (readers[i].Id == readerId)
            {
                readerExists = true;
                break;
            }
        }
        if (!readerExists)
        {
            Console.WriteLine("Читатель не найден");
            return;
        }

        Console.Write("Введите ID книги: ");
        int bookId = int.Parse(Console.ReadLine());

        Book foundBook = null;
        for (int i = 0; i < books.Count; i++)
        {
            if (books[i].Id == bookId)
            {
                foundBook = books[i];
                break;
            }
        }
        if (foundBook == null)
        {
            Console.WriteLine("Книга не найдена");
            return;
        }

        for (int i = 0; i < foundBook.Movements.Count; i++)
        {
            if (foundBook.Movements[i].ReturnDate == null)
            {
                Console.WriteLine("Эта книга уже выдана и не сдана");
                return;
            }
        }

        BookMovement movement = new BookMovement();
        movement.ReaderId = readerId;

        while (true)
        {
            Console.Write("Введите дату выдачи (дд.мм.гггг) (то есть в формате как в России): ");
            string input = Console.ReadLine();
            try
            {
                movement.IssueDate = DateTime.Parse(input);
                break;
            }
            catch
            {
                Console.WriteLine("Неверный формат даты! Попробуйте ещё раз");
            }
        }

        movement.ReturnDate = null;
        foundBook.Movements.Add(movement);
        Console.WriteLine("Книга выдана");
    }

    static void ReturnBook()
    {
        Console.Write("Введите номер читательского билета (ID): ");
        int readerId = int.Parse(Console.ReadLine());

        bool readerExists = false;
        for (int i = 0; i < readers.Count; i++)
        {
            if (readers[i].Id == readerId)
            {
                readerExists = true;
                break;
            }
        }
        if (!readerExists)
        {
            Console.WriteLine("Читатель не найден");
            return;
        }

        List<Book> borrowedBooks = new List<Book>();
        List<int> movementIndexes = new List<int>();

        for (int i = 0; i < books.Count; i++)
        {
            for (int j = 0; j < books[i].Movements.Count; j++)
            {
                if (books[i].Movements[j].ReaderId == readerId && books[i].Movements[j].ReturnDate == null)
                {
                    borrowedBooks.Add(books[i]);
                    movementIndexes.Add(j);
                }
            }
        }

        if (borrowedBooks.Count == 0)
        {
            Console.WriteLine("У этого читателя нет взятых книг");
            return;
        }

        Console.WriteLine("Книги на руках у читателя:");
        for (int i = 0; i < borrowedBooks.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + borrowedBooks[i].Title + " (ID: " + borrowedBooks[i].Id + ")");
        }

        Console.Write("Выберите номер книги для сдачи: ");
        int choice = int.Parse(Console.ReadLine());

        if (choice < 1 || choice > borrowedBooks.Count)
        {
            Console.WriteLine("Неверный выбор");
            return;
        }

        Book bookToReturn = borrowedBooks[choice - 1];
        int movIndex = movementIndexes[choice - 1];

        while (true)
        {
            Console.Write("Введите дату сдачи (дд.мм.гггг) (то есть в формате как в России): ");
            string input = Console.ReadLine();
            try
            {
                DateTime returnDate = DateTime.Parse(input);
                if (returnDate > bookToReturn.Movements[movIndex].IssueDate)
                {
                    bookToReturn.Movements[movIndex].ReturnDate = returnDate;
                    Console.WriteLine("Книга сдана");
                    return;
                }
                else
                {
                    Console.WriteLine("Дата сдачи должна быть больше даты выдачи (" +
                                      bookToReturn.Movements[movIndex].IssueDate.ToShortDateString() + ")");
                }
            }
            catch
            {
                Console.WriteLine("Неверный формат даты, попробуйте ещё раз");
            }
        }
    }
}
