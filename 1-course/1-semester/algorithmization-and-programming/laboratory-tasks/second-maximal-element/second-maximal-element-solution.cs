using System;

namespace changeSign
{
    class Program
    {
        static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int max1Elem = Convert.ToInt32(Console.ReadLine());
            int max2Elem = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i <= n - 2; i++)
            {
                int newElem = Convert.ToInt32(Console.ReadLine());
                if (newElem > max2Elem)
                {
                    if (newElem > max1Elem)
                    {
                        max2Elem = max1Elem;
                        max1Elem = newElem;

                    }
                    else
                    {
                        max2Elem = newElem;
                    }
                }
            }
            Console.WriteLine(max2Elem);
            Console.WriteLine(max1Elem);

        }
    }
}
