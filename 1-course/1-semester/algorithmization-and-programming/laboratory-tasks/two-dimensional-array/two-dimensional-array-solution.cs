using System;
using System.Linq;
using System.Linq.Expressions;

namespace twoDimensionalMassiv
{
    class Program
    {
        static void Main()
        {
            var sizes = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int M = sizes[0];
            int N = sizes[1];

            int[,] arr = new int[M, N];

            for (int i = 0; i < M; i++)
            {
                var row = Console.ReadLine().Split().Select(int.Parse).ToArray();
                for (int j = 0; j < N; j++)
                {
                    arr[i, j] = row[j];
                }
            }

            // задание 1

            for (int i = 0; i < N; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    bool equal = true;
                    int[] countA = new int[201];
                    int[] countB = new int[201];

                    for (int k = 0; k < M; k++)
                    {
                        countA[arr[k, i] + 100]++;
                        countB[arr[k, j] + 100]++;
                    }

                    for (int t = 0; t < 201; t++)
                    {
                        if (countA[t] != countB[t])
                        {
                            equal = false;
                            break;
                        }
                    }

                    if (equal)
                        Console.Write($"{i + 1}{j + 1} ");
                }
            }
            Console.WriteLine();
            // задание 2
            int answer = 0;
            int sum = 0;
            int proizv = 0;
            int elem = 0;
            int[,] array = arr;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                sum = 0;
                proizv = 1;
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    elem = array[i, j];
                    sum += elem;
                    proizv *= elem;
                }
                if (sum > proizv)
                {
                    answer += 1;
                }
            }
            Console.WriteLine($"Ответ на задачу2: количество строк в которых суума элементов больше произведения элементов = {answer}");

            // задание 3
            int[,] array_answer = new int[arr.GetLength(0), arr.GetLength(1)];

            int sum_polozh_now = 0;
            int sum_polozh_old = 0;
            int[] sum_list = new int[arr.GetLength(0)];


            for (int i = 0; i < array.GetLength(0); i++)
            {
                sum_polozh_now = 0;
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    elem = arr[i, j];
                    if (elem > 0)
                    {
                        sum_polozh_now += elem;
                    }
                }
                sum_list[i] = sum_polozh_now;
            }
            
            Array.Sort(sum_list);
            Array.Reverse(sum_list);

            for (int i = 0; i < array.GetLength(0); i++)
            {
                sum_polozh_now = 0;
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    elem = arr[i, j];
                    if (elem > 0)
                    {
                        sum_polozh_now += elem;
                    }
                }
                for (int k = 0; k < array.GetLength(0); k++)
                {
                    if (sum_polozh_now == sum_list[k])
                    {
                        for (int q = 0; q < array.GetLength(1); q++)
                        {
                            array_answer[k, q] = array[i, q];
                        }
                    }
                }
            }

            for (int i = 0; i < array_answer.GetLength(0); i++)
            {
                for (int j = 0; j < array_answer.GetLength(1); j++)
                {
                    Console.Write(array_answer[i, j] + " ");
                }
                Console.WriteLine();
            }

            // задание 4
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            int[] minInRows = new int[rows]; // массив минимальных элементов по строкам

            for (int i = 0; i < rows; i++)
            {
                int min = array[i, 0]; // считаем первый элемент строки минимальным
                for (int j = 1; j < cols; j++)
                {
                    if (array[i, j] < min)
                    {
                        min = array[i, j];
                    }
                        
                }
                minInRows[i] = min;
            }

            Console.WriteLine("Минимальные элементы каждой строки:");
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine($"Строка {i + 1}: {minInRows[i]}");
            }


        }
    }
}
