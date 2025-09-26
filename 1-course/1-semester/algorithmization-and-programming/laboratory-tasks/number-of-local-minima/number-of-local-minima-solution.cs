using System;

namespace changeSign
{
    class Program
    {
        static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int answer = 0;
            int oldElem = Convert.ToInt32(Console.ReadLine());
            int nowElem = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i <= n - 2; i++)
            {
                int newElem = Convert.ToInt32(Console.ReadLine());
                if ((oldElem > nowElem) && (newElem > nowElem))
                {
                    answer += 1;
                }
                oldElem = nowElem;
                nowElem = newElem;
            }
            Console.WriteLine(answer);

        }
    }
}
