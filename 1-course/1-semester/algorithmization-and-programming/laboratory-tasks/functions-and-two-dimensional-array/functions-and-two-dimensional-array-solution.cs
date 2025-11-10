using System;
using System.Linq;

class Program
{
    static int[,] inpt(int x, int y)
    {
        int[,] arr = new int[x, y];
        for (int i = 0; i < x; i++)
        {
            var row = Console.ReadLine().Split().Select(int.Parse).ToArray();
            for (int j = 0; j < y; j++)
            {
                arr[i, j] = row[j];
            }
        }
        Console.WriteLine("массив задан");
        return arr;
    }

    static void outp(int[,] arr)
    {
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                Console.Write(arr[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("Массив выведен");
    }

    static int[,] reform(int[,] arr)
    {
        int maxElem = arr[0, 0];
        int maxElemX = 0;
        int maxElemY = 0;

        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                if (arr[i, j] > maxElem)
                {
                    maxElem = arr[i, j];
                    maxElemX = i;
                    maxElemY = j;
                }
            }
        }

        arr = lineReform(arr, maxElemX);
        arr = columReform(arr, maxElemY);
        Console.WriteLine("преобразование завершено");
        return arr;
    }

    static int[,] lineReform(int[,] arr, int a)
    {
        for (int j = 0; j < arr.GetLength(1); j++)
        {
            int temp = arr[a, j];
            arr[a, j] = arr[0, j];
            arr[0, j] = temp;
        }
        return arr;
    }

    static int[,] columReform(int[,] arr, int a)
    {
        int last = arr.GetLength(1) - 1;
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            int temp = arr[i, a];
            arr[i, a] = arr[i, last];
            arr[i, last] = temp;
        }
        return arr;
    }

    static void Main()
    {
        int flag = 0;
        int inp = 0;
        int[,] arr = null;

        while (flag != 4)
        {
            inp = int.Parse(Console.ReadLine());
            switch (inp)
            {
                case 1:
                    var sizes = Console.ReadLine().Split().Select(int.Parse).ToArray();
                    int x = sizes[0];
                    int y = sizes[1];
                    flag = 1;
                    arr = inpt(x, y);
                    Console.WriteLine("1");
                    break;
                case 2:
                    if (flag == 1) outp(arr);
                    else Console.WriteLine("Ошибка, массив еще не задан");
                    Console.WriteLine("2");
                    break;
                case 3:
                    if (flag == 1) arr = reform(arr);
                    else Console.WriteLine("Ошибка, массив еще не задан");
                    Console.WriteLine("3");
                    break;
                case 4:
                    flag = 4;
                    Console.WriteLine("4");
                    break;
            }
        }
    }
}
