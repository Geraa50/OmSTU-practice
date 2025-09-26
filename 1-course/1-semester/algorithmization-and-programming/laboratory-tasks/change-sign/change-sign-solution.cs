using System;

namespace changeSign
{
    class Program
    {
        static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int firstInp = Convert.ToInt32(Console.ReadLine());
            int answer = 0;
            int znak = 123;
            if (firstInp < 0)
            {
                znak = 0; // 0 если отрицательный
            }
            else
            {
                znak = 1;
            }

            for (int i = 1; i <= n - 1; i++)
            {
                int inp = Convert.ToInt32(Console.ReadLine());
                if ((inp < 0) && (znak == 1))
                {
                    znak = 0;
                    answer += 1;
                }
                else if ((inp > 0) && (znak == 0))
                {
                    znak = 1;
                    answer += 1;
                }

            }
            Console.WriteLine(answer);
        }
    }
}
