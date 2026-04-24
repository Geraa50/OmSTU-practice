using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.Write("Введите путь к файлу: ");
        string path = Console.ReadLine();

        if (!File.Exists(path))
        {
            Console.WriteLine("Файл не найден!");
            return;
        }

        string[] lines = File.ReadAllLines(path);

        if (lines.Length == 0)
        {
            Console.WriteLine("Файл пуст!");
            return;
        }

        int globalMax = 0;
        int[] maxCounts = new int[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            if (line.Length == 0)
            {
                maxCounts[i] = 0;
                continue;
            }

            int maxCount = 1;
            int currentCount = 1;

            for (int j = 1; j < line.Length; j++)
            {
                if (line[j] == line[j - 1])
                {
                    currentCount++;
                    if (currentCount > maxCount)
                    {
                        maxCount = currentCount;
                    }
                }
                else
                {
                    currentCount = 1;
                }
            }

            maxCounts[i] = maxCount;

            if (maxCount > globalMax)
            {
                globalMax = maxCount;
            }
        }

        Console.WriteLine("\nМаксимальное количество подряд идущих одинаковых элементов: " + globalMax);
        Console.WriteLine("Строки с таким количеством:");

        for (int i = 0; i < lines.Length; i++)
        {
            if (maxCounts[i] == globalMax)
            {
                Console.WriteLine(lines[i]);
            }
        }
    }
}
