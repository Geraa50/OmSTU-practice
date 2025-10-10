using System;
using System.Formats.Asn1;

namespace sumOfNumbersWithOne
{
    class Program
    {
        static void Main()
        {
            int n = 0;
            int full_n = 0;
            int cifra = 0;
            int answer = 0;
            while (n != 0)
            {
                n = Convert.ToInt32(Console.ReadLine());
                full_n = n;
                while (n != 0)
                {
                    cifra = n % 10;
                    if (cifra == 1)
                    {
                        answer += full_n;
                        break;
                    }
                    n /= 10;
                }
            }
            Console.WriteLine(answer);

        }
    }
}