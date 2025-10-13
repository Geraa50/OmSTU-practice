using System;
using System.Formats.Asn1;
using System.Linq.Expressions;

namespace maximumOfPositiveUsingArray
{
    class Program
    {
        static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int[] massiv = new int[n];
            int answer = 0;
            for (int i = 0; i < n; i++)
            {
                massiv[i] = Convert.ToInt32(Console.ReadLine());


            }
            // answer = massiv[0];
            for (int j = 1; j < massiv.Length; j ++)
            {
                if (massiv[j] > answer)
                {
                    answer = massiv[j];
                }
            }
            Console.WriteLine(answer);
        }
    }
}