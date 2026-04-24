using System;
using System.Collections;

class Program
{
    static Hashtable directory = new Hashtable();

    static void Main()
    {
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

            switch (choice)
            {
                case "1": AddEntry(); break;
                case "2": DeleteEntry(); break;
                case "3": PrintAll(); break;
                case "4": FindByPhone(); break;
                case "0": return;
                default: Console.WriteLine("Неверный выбор!"); break;
            }
        }
    }

    static void AddEntry()
    {
        Console.Write("Введите номер телефона: ");
        string phone = Console.ReadLine();

        if (directory.ContainsKey(phone))
        {
            Console.WriteLine("Запись с таким номером уже существует!");
            return;
        }

        Console.Write("Введите ФИО: ");
        string name = Console.ReadLine();

        directory.Add(phone, name);
        Console.WriteLine("Запись добавлена!");
    }

    static void DeleteEntry()
    {
        Console.Write("Введите номер телефона для удаления: ");
        string phone = Console.ReadLine();

        if (directory.ContainsKey(phone))
        {
            directory.Remove(phone);
            Console.WriteLine("Запись удалена!");
        }
        else
        {
            Console.WriteLine("Запись с таким номером не найдена!");
        }
    }

    static void PrintAll()
    {
        if (directory.Count == 0)
        {
            Console.WriteLine("Справочник пуст.");
            return;
        }

        Console.WriteLine("\nТелефонный справочник:");
        foreach (DictionaryEntry entry in directory)
        {
            Console.WriteLine("Телефон: " + entry.Key + " | ФИО: " + entry.Value);
        }
    }

    static void FindByPhone()
    {
        Console.Write("Введите номер телефона: ");
        string phone = Console.ReadLine();

        if (directory.ContainsKey(phone))
        {
            Console.WriteLine("ФИО: " + directory[phone]);
        }
        else
        {
            Console.WriteLine("Запись с таким номером не найдена!");
        }
    }
}
