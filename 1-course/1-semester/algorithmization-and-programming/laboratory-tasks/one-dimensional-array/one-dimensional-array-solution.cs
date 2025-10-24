
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

            // задача 1, 3
            int chetnost = 1;  // станет 0 значит есть четные
            for (int j = 1; j < massiv.Length; j++)
            {
                if (massiv[j] > max)
                {
                    max = massiv[j];
                }
                if (massiv[j] % 2 == 0)
                {
                    chetnost = 0;
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

            int elem = 0;
            for (int k = (min_index + 1); k < max_index; k++)
            {
                elem = massiv[k];
                if (elem % 2 == 0)
                {
                    answer += 1;
                }
            }
            Console.WriteLine($"кол-во четных элементов между макс и мин = {answer}");

            // задача 3 уже решена в цикле задачи 1
            if (chetnost == 1)
            {
                Console.WriteLine("Все элементы нечетные");
            }
            else
            {
                Console.WriteLine("Есть четные элементы");
            }

            // задача 4
            // int elem = 0;
            int sumcifr = 0;
            for (int k = 0; k < massiv.Length; k++)
            {
                elem = massiv[k];
                while (elem != 0)
                {
                    sumcifr += elem % 10;
                    elem /= 10;

                }
                if (sumcifr % 3 == 0)
                {
                    Console.WriteLine($"сумма цифр элемента под индексом {k + 1} кратна 3");
                }
                sumcifr = 0;
            }
            
        }
    }
}