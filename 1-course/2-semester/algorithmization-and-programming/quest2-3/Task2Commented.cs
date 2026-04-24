// Подключаем System для работы с консолью и базовыми типами
using System;
// Подключаем System.IO для работы с файлами
using System.IO;

class Program
{
    static void Main()
    {
        // Просим пользователя ввести путь к входному файлу (с матрицей)
        Console.Write("Введите путь к входному файлу: ");
        string inputPath = Console.ReadLine();

        // Просим ввести путь к выходному файлу (куда запишем результат)
        Console.Write("Введите путь к выходному файлу: ");
        string outputPath = Console.ReadLine();

        // Проверяем, существует ли входной файл
        if (!File.Exists(inputPath))
        {
            Console.WriteLine("Входной файл не найден!");
            return;
        }

        // Считываем все строки из файла
        // Каждая строка файла — это одна строка матрицы
        string[] lines = File.ReadAllLines(inputPath);

        // Количество строк = количество строк матрицы (матрица квадратная)
        int n = lines.Length;

        // Создаём зубчатый массив (массив массивов) для хранения матрицы
        int[][] matrix = new int[n][];

        // Заполняем матрицу: разбиваем каждую строку по пробелам и табуляциям
        for (int i = 0; i < n; i++)
        {
            // Split разделяет строку по символам-разделителям
            // StringSplitOptions.RemoveEmptyEntries убирает пустые элементы
            string[] parts = lines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            // Создаём массив чисел для текущей строки матрицы
            matrix[i] = new int[parts.Length];

            // Преобразуем каждый элемент из строки в число
            for (int j = 0; j < parts.Length; j++)
            {
                matrix[i][j] = int.Parse(parts[j]);
            }
        }

        // Открываем выходной файл для записи
        StreamWriter writer = new StreamWriter(outputPath);

        // Проверяем каждую строку матрицы на монотонность
        for (int i = 0; i < n; i++)
        {
            // Если в строке 1 элемент или меньше — она всегда монотонна
            if (matrix[i].Length <= 1)
            {
                // Записываем строку в выходной файл
                writer.WriteLine(string.Join(" ", matrix[i]));
                continue; // Переходим к следующей строке
            }

            // Флаги: предполагаем, что последовательность и возрастающая, и убывающая
            bool increasing = true;  // Строго возрастающая
            bool decreasing = true;  // Строго убывающая

            // Проходим по элементам строки, начиная со второго
            for (int j = 1; j < matrix[i].Length; j++)
            {
                // Если текущий элемент НЕ больше предыдущего — не возрастающая
                if (matrix[i][j] <= matrix[i][j - 1])
                    increasing = false;
                // Если текущий элемент НЕ меньше предыдущего — не убывающая
                if (matrix[i][j] >= matrix[i][j - 1])
                    decreasing = false;
            }

            // Если последовательность строго возрастающая ИЛИ строго убывающая —
            // она монотонная, записываем в выходной файл
            if (increasing || decreasing)
            {
                // Собираем строку из чисел через пробел
                string row = "";
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (j > 0) row += " "; // Добавляем пробел перед каждым числом, кроме первого
                    row += matrix[i][j];    // Добавляем число
                }
                // Записываем собранную строку в файл
                writer.WriteLine(row);
            }
        }

        // Закрываем файл — это важно, чтобы данные записались на диск
        writer.Close();
        Console.WriteLine("Результат записан в файл: " + outputPath);
    }
}
