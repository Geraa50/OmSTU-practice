// Подключаем System для базовых операций (Console и т.д.)
using System;
// Подключаем System.Collections — именно здесь находится класс Hashtable
using System.Collections;
// Подключаем System.IO для работы с файлами
using System.IO;

class Program
{
    static void Main()
    {
        // Просим пользователя ввести путь к файлу, в котором записаны слова
        Console.Write("Введите путь к файлу со словами: ");
        string path = Console.ReadLine();

        // Проверяем, существует ли файл
        if (!File.Exists(path))
        {
            Console.WriteLine("Файл не найден!");
            return;
        }

        // Считываем весь текст из файла в одну строку
        string text = File.ReadAllText(path);

        // Разбиваем текст на отдельные слова
        // Разделителями служат: пробел, перевод строки (\n), возврат каретки (\r), табуляция (\t)
        // RemoveEmptyEntries — убираем пустые элементы, которые могут возникнуть при нескольких разделителях подряд
        string[] words = text.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        // Создаём хеш-таблицу для подсчёта частоты слов
        // Ключ (Key) — слово (string)
        // Значение (Value) — количество появлений этого слова (int)
        Hashtable frequency = new Hashtable();

        // Проходим по каждому слову из массива
        for (int i = 0; i < words.Length; i++)
        {
            // Приводим слово к нижнему регистру (все буквы маленькие)
            string word = words[i].ToLower();

            // Проверяем, есть ли уже такое слово в хеш-таблице
            if (frequency.ContainsKey(word))
            {
                // Если слово уже встречалось — увеличиваем счётчик на 1
                // frequency[word] возвращает object, поэтому приводим к int
                frequency[word] = (int)frequency[word] + 1;
            }
            else
            {
                // Если слово встретилось впервые — добавляем с количеством 1
                frequency[word] = 1;
            }
        }

        // Выводим результат: каждое слово и сколько раз оно встретилось
        Console.WriteLine("\nЧастота появления каждого слова:");

        // foreach перебирает все пары ключ-значение в хеш-таблице
        // DictionaryEntry — структура, содержащая свойства Key и Value
        foreach (DictionaryEntry entry in frequency)
        {
            // entry.Key — слово, entry.Value — количество
            Console.WriteLine("\"" + entry.Key + "\" — " + entry.Value + " раз(а)");
        }
    }
}
