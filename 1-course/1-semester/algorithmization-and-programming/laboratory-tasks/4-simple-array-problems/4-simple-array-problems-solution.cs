using System;
using System.Formats.Asn1;
using System.Linq.Expressions;

namespace SimpleFourArrayProblems
{
    class Program
    {
        static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int[] massiv = new int[n];
            int max = 0;
            for (int i = 0; i < n; i++)
            {
                massiv[i] = Convert.ToInt32(Console.ReadLine());


            }

            // задача 1
            for (int j = 1; j < massiv.Length; j++)
            {
                if (massiv[j] > max)
                {
                    max = massiv[j];
                }
            }
            Console.WriteLine($"максимальный из положительных = {max}");

            // задача 2
            int min = massiv.Min();
            int max_index = Array.IndexOf(massiv, max);
            int min_index = Array.IndexOf(massiv, min);
            int answer = 0;

            if (max_index < min_index)
            {
                int temp = min_index;
                min_index = max_index;
                max_index = temp;
            }


            for (int k = (min_index + 1); k < max_index; k++)
            {
                int elem = massiv[k];
                if (elem % 2 == 0)
                {
                    answer += 1;
                }
            }
            Console.WriteLine($"кол-во четных элементов между макс и мин = {answer}");
        }
    }
}