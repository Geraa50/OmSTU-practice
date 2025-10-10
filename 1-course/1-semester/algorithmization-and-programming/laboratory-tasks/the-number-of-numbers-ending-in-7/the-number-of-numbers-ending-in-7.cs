using System;

namespace theNumberOfNumbersEndingIn7
{
    class Program
    {
        static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int answer = 0;
            int flag = 0; // число не подходит, если 1 то подходит
            int cifra = 0;
            int chislo = 0;
            int chislo_full = 0;
            for (int i = 1; i <= n; i++)
            {
                chislo = Convert.ToInt32(Console.ReadLine());
                cifra = chislo % 10;
                if (cifra == 7)
                {
                    answer += 1;
                }
            }
            Console.WriteLine(answer);

        }
    }
}