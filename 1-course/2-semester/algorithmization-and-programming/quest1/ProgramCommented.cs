// Подключаем пространство имён System — оно содержит базовые типы (Console, DateTime и т.д.)
using System;
// Подключаем пространство имён для работы с коллекциями, в частности List<T>
using System.Collections.Generic;

// Структура для хранения ФИО (Фамилия, Имя, Отчество)
// Структура — это тип-значение, она описывается до классов
struct FullName
{
    // Поле для хранения фамилии
    public string Surname;
    // Поле для хранения имени
    public string Name;
    // Поле для хранения отчества
    public string Patronymic;
}

// Класс "Читатель" — описывает одного читателя библиотеки
class Reader
{
    // Уникальный номер читательского билета (идентификатор)
    public int Id;
    // ФИО читателя — используем структуру FullName, описанную выше
    public FullName FullName;
    // Год рождения — при вводе проверяем, что введены только цифры
    public int BirthYear;
    // Адрес читателя — вводится без проверок
    public string Address;
}

// Класс "Книга" — описывает одну книгу в библиотеке
class Book
{
    // Уникальный номер книги (ID), не должен повторяться
    public int Id;
    // Название книги
    public string Title;
    // Автор книги
    public string Author;
    // Год издания — при вводе проверяем, что введены только цифры
    public int PublicationYear;
    // Количество страниц — при вводе проверяем, что введены только цифры
    public int PageCount;
    // Список движений книги — кто когда брал и сдавал
    // Создаём пустой список сразу при создании объекта книги
    public List<BookMovement> Movements = new List<BookMovement>();
}

// Класс "Движение книги" — описывает одну запись о выдаче/сдаче книги
class BookMovement
{
    // Дата выдачи книги читателю
    public DateTime IssueDate;
    // Дата сдачи книги (может быть null, если книга ещё не сдана)
    // DateTime? — это nullable тип, то есть может хранить null
    public DateTime? ReturnDate;
    // ID читателя, который взял книгу
    public int ReaderId;
}

class Program
{
    // Статический список всех читателей — доступен из всех методов класса Program
    static List<Reader> readers = new List<Reader>();
    // Статический список всех книг — доступен из всех методов класса Program
    static List<Book> books = new List<Book>();

    // Главный метод — точка входа в программу
    static void Main()
    {
        // Бесконечный цикл для отображения меню, пока пользователь не выберет "Выход"
        while (true)
        {
            // Выводим меню на экран
            Console.WriteLine("\n=== Библиотечная система ===");
            Console.WriteLine("1. Добавить читателя");
            Console.WriteLine("2. Добавить книгу");
            Console.WriteLine("3. Показать информацию о книгах и их движение");
            Console.WriteLine("4. Показать несданные книги");
            Console.WriteLine("5. Показать книги, которые никто не брал");
            Console.WriteLine("6. Взять книгу");
            Console.WriteLine("7. Сдать книгу");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите пункт меню: ");

            // Считываем выбор пользователя
            string choice = Console.ReadLine();

            // Используем switch для вызова нужного метода в зависимости от выбора
            switch (choice)
            {
                case "1": AddReader(); break;          // Вызываем метод добавления читателя
                case "2": AddBook(); break;            // Вызываем метод добавления книги
                case "3": ShowBooksInfo(); break;      // Вызываем метод показа информации о книгах
                case "4": ShowUnreturnedBooks(); break; // Вызываем метод показа несданных книг
                case "5": ShowNeverBorrowedBooks(); break; // Вызываем метод показа невостребованных книг
                case "6": BorrowBook(); break;         // Вызываем метод выдачи книги
                case "7": ReturnBook(); break;         // Вызываем метод сдачи книги
                case "0": return;                      // Выходим из программы (return завершает Main)
                default: Console.WriteLine("Неверный выбор!"); break; // Если ввели что-то другое
            }
        }
    }

    // Метод для добавления нового читателя в систему
    static void AddReader()
    {
        // Создаём новый объект класса Reader
        Reader reader = new Reader();

        // Просим ввести ID читателя и преобразуем строку в число
        Console.Write("Введите ID читателя: ");
        reader.Id = int.Parse(Console.ReadLine());

        // Создаём структуру FullName для хранения ФИО
        FullName fn = new FullName();
        // Просим ввести каждую часть ФИО отдельно
        Console.Write("Введите фамилию: ");
        fn.Surname = Console.ReadLine();
        Console.Write("Введите имя: ");
        fn.Name = Console.ReadLine();
        Console.Write("Введите отчество: ");
        fn.Patronymic = Console.ReadLine();
        // Записываем заполненную структуру ФИО в объект читателя
        reader.FullName = fn;

        // Цикл для ввода года рождения с проверкой на цифры
        while (true)
        {
            Console.Write("Введите год рождения: ");
            string input = Console.ReadLine();
            // Флаг, показывающий корректность ввода
            bool isValid = true;
            // Проходим по каждому символу строки и проверяем, что это цифра
            for (int i = 0; i < input.Length; i++)
            {
                // Если символ не является цифрой (не в диапазоне '0'..'9')
                if (input[i] < '0' || input[i] > '9')
                {
                    isValid = false; // Устанавливаем флаг в false
                    break;           // Прерываем цикл проверки
                }
            }
            // Если все символы — цифры и строка не пустая
            if (isValid && input.Length > 0)
            {
                // Преобразуем строку в число и сохраняем
                reader.BirthYear = int.Parse(input);
                break; // Выходим из цикла while
            }
            // Если проверка не прошла — выводим ошибку и просим ввести заново
            Console.WriteLine("Нужно ввести только цифры");
        }

        // Адрес вводится без проверок
        Console.Write("Введите адрес: ");
        reader.Address = Console.ReadLine();

        // Добавляем нового читателя в общий список
        readers.Add(reader);
        Console.WriteLine("Ура, победа, читатель добвлен!");
    }

    // Метод для добавления новой книги в систему
    static void AddBook()
    {
        // Создаём новый объект класса Book
        Book book = new Book();

        // Просим ввести ID книги
        Console.Write("Введите ID книги: ");
        int id = int.Parse(Console.ReadLine());

        // Проверяем, что книги с таким ID ещё нет (ID должен быть уникальным)
        for (int i = 0; i < books.Count; i++)
        {
            if (books[i].Id == id)
            {
                Console.WriteLine("Книга с таким ID уже существует!");
                return; // Прерываем метод, книга не добавляется
            }
        }
        // Если ID уникальный — сохраняем его
        book.Id = id;

        // Просим ввести название книги
        Console.Write("Введите наименование книги: ");
        book.Title = Console.ReadLine();

        // Просим ввести автора
        Console.Write("Введите автора: ");
        book.Author = Console.ReadLine();

        // Цикл для ввода года издания с проверкой на цифры
        while (true)
        {
            Console.Write("Введите год издания: ");
            string input = Console.ReadLine();
            bool isValid = true;
            // Проверяем каждый символ — должен быть цифрой
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
            Console.WriteLine("Ошибка! Введите только цифры.");
        }

        // Цикл для ввода количества страниц с проверкой на цифры
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
            Console.WriteLine("Ошибка! Введите только цифры.");
        }

        // Добавляем книгу в общий список
        books.Add(book);
        Console.WriteLine("Книга добавлена!");
    }

    // Метод для вывода информации о каждой книге и её движении
    static void ShowBooksInfo()
    {
        // Если книг нет — сообщаем об этом
        if (books.Count == 0)
        {
            Console.WriteLine("Книг нет.");
            return;
        }

        // Проходим по каждой книге в списке
        for (int i = 0; i < books.Count; i++)
        {
            Console.WriteLine("\n--- Книга ---");
            // Выводим название и автора книги
            Console.WriteLine("Наименование: " + books[i].Title);
            Console.WriteLine("Автор: " + books[i].Author);

            // Если у книги нет записей о движении
            if (books[i].Movements.Count == 0)
            {
                Console.WriteLine("Движение книги: нет записей");
            }
            else
            {
                Console.WriteLine("Движение книги:");
                // Проходим по каждой записи о движении
                for (int j = 0; j < books[i].Movements.Count; j++)
                {
                    // Сохраняем текущую запись в переменную для удобства
                    BookMovement m = books[i].Movements[j];

                    // Ищем ФИО читателя по его ID
                    string readerName = "Неизвестный";
                    for (int k = 0; k < readers.Count; k++)
                    {
                        if (readers[k].Id == m.ReaderId)
                        {
                            // Собираем ФИО из структуры FullName
                            readerName = readers[k].FullName.Surname + " " +
                                         readers[k].FullName.Name + " " +
                                         readers[k].FullName.Patronymic;
                            break; // Нашли — выходим из цикла поиска
                        }
                    }

                    // Выводим дату выдачи
                    Console.Write("  Выдана: " + m.IssueDate.ToShortDateString());
                    // Если книга сдана — выводим дату сдачи
                    if (m.ReturnDate != null)
                    {
                        Console.Write(", Сдана: " + m.ReturnDate.Value.ToShortDateString());
                    }
                    else
                    {
                        // Если книга не сдана — пишем об этом
                        Console.Write(", Не сдана");
                    }
                    // Выводим ФИО читателя
                    Console.WriteLine(", Читатель: " + readerName);
                }
            }
        }
    }

    // Метод для вывода списка несданных книг
    static void ShowUnreturnedBooks()
    {
        // Флаг — нашли ли хотя бы одну несданную книгу
        bool found = false;

        // Проходим по всем книгам
        for (int i = 0; i < books.Count; i++)
        {
            // Проходим по всем движениям каждой книги
            for (int j = 0; j < books[i].Movements.Count; j++)
            {
                // Если дата сдачи == null, значит книга не сдана
                if (books[i].Movements[j].ReturnDate == null)
                {
                    // Ищем ФИО читателя
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
                    // Выводим информацию о несданной книге
                    Console.WriteLine("Книга: " + books[i].Title +
                                      " | Читатель: " + readerName +
                                      " | Выдана: " + books[i].Movements[j].IssueDate.ToShortDateString());
                    found = true; // Пометили, что нашли хотя бы одну
                }
            }
        }

        // Если ни одной несданной книги не нашли
        if (!found)
        {
            Console.WriteLine("Все книги сданы.");
        }
    }

    // Метод для вывода книг, которые ни разу никто не брал
    static void ShowNeverBorrowedBooks()
    {
        bool found = false;

        // Проходим по всем книгам
        for (int i = 0; i < books.Count; i++)
        {
            // Если список движений пуст — книгу никто не брал
            if (books[i].Movements.Count == 0)
            {
                Console.WriteLine("Книга: " + books[i].Title + " (ID: " + books[i].Id + ")");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Все книги хотя бы раз были взяты.");
        }
    }

    // Метод для выдачи книги читателю
    static void BorrowBook()
    {
        // Проверяем, что в системе есть и книги, и читатели
        if (books.Count == 0 || readers.Count == 0)
        {
            Console.WriteLine("Нет книг или читателей в системе!");
            return;
        }

        // Просим ввести ID читателя
        Console.Write("Введите ID читателя: ");
        int readerId = int.Parse(Console.ReadLine());

        // Проверяем, существует ли читатель с таким ID
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
            Console.WriteLine("Читатель не найден!");
            return;
        }

        // Просим ввести ID книги
        Console.Write("Введите ID книги: ");
        int bookId = int.Parse(Console.ReadLine());

        // Ищем книгу по ID
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
            Console.WriteLine("Книга не найдена!");
            return;
        }

        // Проверяем, не выдана ли уже эта книга (есть ли движение без даты сдачи)
        for (int i = 0; i < foundBook.Movements.Count; i++)
        {
            if (foundBook.Movements[i].ReturnDate == null)
            {
                Console.WriteLine("Эта книга уже выдана и не сдана!");
                return;
            }
        }

        // Создаём новую запись о движении книги
        BookMovement movement = new BookMovement();
        movement.ReaderId = readerId;

        // Вводим дату выдачи с проверкой формата
        while (true)
        {
            Console.Write("Введите дату выдачи (дд.мм.гггг) (то есть в формате как в России): ");
            string input = Console.ReadLine();
            try
            {
                // Пытаемся преобразовать строку в дату
                movement.IssueDate = DateTime.Parse(input);
                break; // Если получилось — выходим из цикла
            }
            catch
            {
                // Если формат неверный — ловим исключение и просим ввести заново
                Console.WriteLine("Неверный формат даты! Попробуйте ещё раз.");
            }
        }

        // Дата сдачи пока null — книга только что выдана
        movement.ReturnDate = null;
        // Добавляем запись о движении в список движений этой книги
        foundBook.Movements.Add(movement);
        Console.WriteLine("Книга выдана!");
    }

    // Метод для сдачи книги
    static void ReturnBook()
    {
        // Просим ввести номер читательского билета
        Console.Write("Введите номер читательского билета (ID): ");
        int readerId = int.Parse(Console.ReadLine());

        // Проверяем, существует ли такой читатель
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
            Console.WriteLine("Читатель не найден!");
            return;
        }

        // Собираем список книг, которые этот читатель взял и ещё не сдал
        List<Book> borrowedBooks = new List<Book>();
        // Параллельно запоминаем индексы движений в списках движений книг
        List<int> movementIndexes = new List<int>();

        // Проходим по всем книгам и их движениям
        for (int i = 0; i < books.Count; i++)
        {
            for (int j = 0; j < books[i].Movements.Count; j++)
            {
                // Если движение относится к этому читателю и книга не сдана
                if (books[i].Movements[j].ReaderId == readerId && books[i].Movements[j].ReturnDate == null)
                {
                    borrowedBooks.Add(books[i]);   // Запоминаем книгу
                    movementIndexes.Add(j);         // Запоминаем индекс движения
                }
            }
        }

        // Если у читателя нет книг на руках
        if (borrowedBooks.Count == 0)
        {
            Console.WriteLine("У этого читателя нет взятых книг.");
            return;
        }

        // Выводим список книг на руках у читателя
        Console.WriteLine("Книги на руках у читателя:");
        for (int i = 0; i < borrowedBooks.Count; i++)
        {
            // Нумерация начинается с 1 для удобства пользователя
            Console.WriteLine((i + 1) + ". " + borrowedBooks[i].Title + " (ID: " + borrowedBooks[i].Id + ")");
        }

        // Просим выбрать книгу для сдачи
        Console.Write("Выберите номер книги для сдачи: ");
        int choice = int.Parse(Console.ReadLine());

        // Проверяем корректность выбора
        if (choice < 1 || choice > borrowedBooks.Count)
        {
            Console.WriteLine("Неверный выбор!");
            return;
        }

        // Получаем выбранную книгу и индекс её движения
        // choice - 1 потому что нумерация в списке с 0, а для пользователя с 1
        Book bookToReturn = borrowedBooks[choice - 1];
        int movIndex = movementIndexes[choice - 1];

        // Вводим дату сдачи с проверкой
        while (true)
        {
            Console.Write("Введите дату сдачи (дд.мм.гггг): ");
            string input = Console.ReadLine();
            try
            {
                DateTime returnDate = DateTime.Parse(input);
                // Проверяем, что дата сдачи больше даты выдачи
                if (returnDate > bookToReturn.Movements[movIndex].IssueDate)
                {
                    // Записываем дату сдачи в объект движения
                    bookToReturn.Movements[movIndex].ReturnDate = returnDate;
                    Console.WriteLine("Книга сдана!");
                    return; // Выходим из метода
                }
                else
                {
                    Console.WriteLine("Дата сдачи должна быть больше даты выдачи (" +
                                      bookToReturn.Movements[movIndex].IssueDate.ToShortDateString() + ")!");
                }
            }
            catch
            {
                Console.WriteLine("Неверный формат даты! Попробуйте ещё раз.");
            }
        }
    }
}
