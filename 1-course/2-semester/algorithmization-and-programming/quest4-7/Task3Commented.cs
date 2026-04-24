// Подключаем System для работы с консолью
using System;
// Подключаем System.Collections — здесь находится класс Hashtable
using System.Collections;

class Program
{
    // Создаём хеш-таблицу для хранения телефонного справочника
    // Ключ (Key) — номер телефона (string)
    // Значение (Value) — ФИО пользователя (string)
    // static — чтобы переменная была доступна из всех статических методов
    static Hashtable directory = new Hashtable();

    static void Main()
    {
        // Бесконечный цикл для отображения меню
        while (true)
        {
            Console.WriteLine("\n=== Телефонный справочник ===");
            Console.WriteLine("1. Добавить запись");
            Console.WriteLine("2. Удалить запись по номеру телефона");
            Console.WriteLine("3. Вывести весь справочник");
            Console.WriteLine("4. Найти пользователя по номеру телефона");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите: ");

            string choice = Console.ReadLine();

            // Вызываем нужный метод в зависимости от выбора пользователя
            switch (choice)
            {
                case "1": AddEntry(); break;    // Добавление записи
                case "2": DeleteEntry(); break; // Удаление записи
                case "3": PrintAll(); break;    // Вывод всего справочника
                case "4": FindByPhone(); break; // Поиск по номеру
                case "0": return;               // Выход из программы
                default: Console.WriteLine("Неверный выбор!"); break;
            }
        }
    }

    // Метод для добавления новой записи в справочник
    static void AddEntry()
    {
        // Просим ввести номер телефона
        Console.Write("Введите номер телефона: ");
        string phone = Console.ReadLine();

        // Проверяем, нет ли уже записи с таким номером
        // ContainsKey — проверяет, существует ли ключ в хеш-таблице
        if (directory.ContainsKey(phone))
        {
            Console.WriteLine("Запись с таким номером уже существует!");
            return; // Прерываем метод
        }

        // Просим ввести ФИО
        Console.Write("Введите ФИО: ");
        string name = Console.ReadLine();

        // Добавляем пару "номер телефона — ФИО" в хеш-таблицу
        // Add добавляет новую пару ключ-значение
        directory.Add(phone, name);
        Console.WriteLine("Запись добавлена!");
    }

    // Метод для удаления записи по номеру телефона
    static void DeleteEntry()
    {
        Console.Write("Введите номер телефона для удаления: ");
        string phone = Console.ReadLine();

        // Проверяем, есть ли такой номер в справочнике
        if (directory.ContainsKey(phone))
        {
            // Remove удаляет пару ключ-значение по ключу
            // Удаляются и ключ (номер), и значение (ФИО)
            directory.Remove(phone);
            Console.WriteLine("Запись удалена!");
        }
        else
        {
            Console.WriteLine("Запись с таким номером не найдена!");
        }
    }

    // Метод для вывода всего телефонного справочника
    static void PrintAll()
    {
        // Проверяем, не пуст ли справочник
        if (directory.Count == 0)
        {
            Console.WriteLine("Справочник пуст.");
            return;
        }

        Console.WriteLine("\nТелефонный справочник:");

        // Перебираем все записи в хеш-таблице
        // DictionaryEntry — структура с полями Key и Value
        foreach (DictionaryEntry entry in directory)
        {
            // entry.Key — номер телефона, entry.Value — ФИО
            Console.WriteLine("Телефон: " + entry.Key + " | ФИО: " + entry.Value);
        }
    }

    // Метод для поиска пользователя по номеру телефона
    static void FindByPhone()
    {
        Console.Write("Введите номер телефона: ");
        string phone = Console.ReadLine();

        // Проверяем, есть ли такой ключ в хеш-таблице
        if (directory.ContainsKey(phone))
        {
            // directory[phone] — получаем значение по ключу (ФИО)
            Console.WriteLine("ФИО: " + directory[phone]);
        }
        else
        {
            Console.WriteLine("Запись с таким номером не найдена!");
        }
    }
}
