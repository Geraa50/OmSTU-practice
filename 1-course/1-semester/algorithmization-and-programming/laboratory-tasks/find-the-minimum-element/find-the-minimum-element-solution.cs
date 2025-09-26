using System;

namespace changeSign
{
    class Program
    {
        static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int minElem = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i <= n - 1; i++)
            {
                int newElem = Convert.ToInt32(Console.ReadLine());
                if (newElem < minElem)
                {
                    minElem = newElem;
                }
            }
            Console.WriteLine(minElem);

        }
    }
}
