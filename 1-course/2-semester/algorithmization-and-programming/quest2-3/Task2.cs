using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.Write("Введите путь к входному файлу: ");
        string inputPath = Console.ReadLine();

        Console.Write("Введите путь к выходному файлу: ");
        string outputPath = Console.ReadLine();

        if (!File.Exists(inputPath))
        {
            Console.WriteLine("Входной файл не найден!");
            return;
        }

        string[] lines = File.ReadAllLines(inputPath);
        int n = lines.Length;

        int[][] matrix = new int[n][];

        for (int i = 0; i < n; i++)
        {
            string[] parts = lines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            matrix[i] = new int[parts.Length];
            for (int j = 0; j < parts.Length; j++)
            {
                matrix[i][j] = int.Parse(parts[j]);
            }
        }

        StreamWriter writer = new StreamWriter(outputPath);

        for (int i = 0; i < n; i++)
        {
            if (matrix[i].Length <= 1)
            {
                writer.WriteLine(string.Join(" ", matrix[i]));
                continue;
            }

            bool increasing = true;
            bool decreasing = true;

            for (int j = 1; j < matrix[i].Length; j++)
            {
                if (matrix[i][j] <= matrix[i][j - 1])
                    increasing = false;
                if (matrix[i][j] >= matrix[i][j - 1])
                    decreasing = false;
            }

            if (increasing || decreasing)
            {
                string row = "";
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (j > 0) row += " ";
                    row += matrix[i][j];
                }
                writer.WriteLine(row);
            }
        }

        writer.Close();
        Console.WriteLine("Результат записан в файл: " + outputPath);
    }
}
