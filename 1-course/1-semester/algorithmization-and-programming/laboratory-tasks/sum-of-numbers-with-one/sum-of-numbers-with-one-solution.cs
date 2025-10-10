using System;

namespace sumOfNumbersWithOne
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
                flag = 0;
                chislo = Convert.ToInt32(Console.ReadLine());
                chislo_full = chislo;
                while (chislo > 1)
                {
                    cifra = chislo % 10;
                    chislo /= 10;
                    if (chislo == 1 || cifra == 1)
                    {
                        answer += chislo_full;
                        break;
                    }
                }
            }
            Console.WriteLine(answer);

        }
    }
}