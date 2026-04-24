using System;
using System.Collections;

class Program
{
    static string[] words = new string[0];

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nменю");
            Console.WriteLine("1 = Ввести слова");
            Console.WriteLine("2 = Подсчитать частоту слов");
            Console.WriteLine("0 = Выход");
            Console.Write("Выберите: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    InputWords();
                    break;
                case "2":
                    PrintFrequency();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
    }

    static void InputWords()
    {
        Console.Write("Введите слова через пробел: ");
        string text = Console.ReadLine();

        words = text.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine("Слова сохранены.");
    }

    static void PrintFrequency()
    {
        if (words.Length == 0)
        {
            Console.WriteLine("Сначала введите слова.");
            return;
        }

        Hashtable frequency = new Hashtable();

        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i].ToLower();

            if (frequency.ContainsKey(word))
            {
                frequency[word] = (int)frequency[word] + 1;
            }
            else
            {
                frequency[word] = 1;
            }
        }

        Console.WriteLine("\nЧастота появления каждого слова:");
        foreach (DictionaryEntry entry in frequency)
        {
            Console.WriteLine("\"" + entry.Key + "\" - " + entry.Value + " раз(а)");
        }
    }
}
