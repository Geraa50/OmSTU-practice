using System;

namespace theNumberOfNumbersEndingIn7
{
    class Program
    {
        static void Main()
        {
            int n = -1;
            int answer = 0;
            int flag = 0; // число не подходит, если 1 то подходит
            int cifra = 0;
            int chislo = 0;
            int chislo_full = 0;
            while (n != 0)
            {
                n = Convert.ToInt32(Console.ReadLine());
                if (n < 0)
                {
                    n *= -1;
                }
                cifra = n % 10;
                if (cifra == 7)
                {
                    answer += 1;
                }
            }
            Console.WriteLine(answer);

        }
    }
}