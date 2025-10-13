using System;

namespace theProductOfOddDigitsAndTheNumberOfZerosInThe7BaseNumberSystem
{
    class Program
    {
        static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int answer_1 = 0;
            int n_1 = n;
            int cifra = 0;

            if (n_1 < 0)
            {
                n_1 *= -1;
            }

            while (n_1 != 0)
            {

                cifra = n_1 % 10;
                n_1 /= 10;
                if (cifra % 2 == 1)
                {
                    if (answer_1 == 0)
                    {
                        answer_1 = 1;
                    }
                    answer_1 *= cifra;
                }

            }
            Console.WriteLine($"произведение нечетных чисел числа = {answer_1}");


            // теперь про 7сс
            int answer_2 = 0;
            if (n == 0)
            {
                Console.WriteLine("количество нулей в 7сс = 0");
            }
            else
            {
                while (n > 0)
                {
                    cifra = n % 7;
                    n /= 7;
                    if (cifra == 0)
                    {
                        answer_2 += 1;
                    }
                }
                Console.WriteLine($"количество нулей в 7сс = {answer_2}");
            }

            
        }
    }
}



