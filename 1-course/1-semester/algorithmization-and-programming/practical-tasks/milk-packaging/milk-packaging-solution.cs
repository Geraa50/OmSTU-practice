using System;

namespace milkPackaging
{
    class Program
    {
        static void Main()
        {
            int answer_company = 0;
            double answer_cost = 0;
            int n = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i <= n; i++)
            {
                int x1 = Convert.ToInt32(Console.ReadLine()); // 
                int y1 = Convert.ToInt32(Console.ReadLine()); // размеры первой упаковки фтрмы
                int z1 = Convert.ToInt32(Console.ReadLine()); // 

                int x2 = Convert.ToInt32(Console.ReadLine()); // 
                int y2 = Convert.ToInt32(Console.ReadLine()); // размеры второй упаковки фирмы
                int z2 = Convert.ToInt32(Console.ReadLine()); // 

                double c1 = Convert.ToDouble(Console.ReadLine()); // стоимость первой упаковки
                double c2 = Convert.ToDouble(Console.ReadLine()); // стоимость второй упаковки

                int V_1 = x1 * y1 * z1; // объем первой упаковки
                int S_1 = x1 * y1 * 2 + x1 * z1 * 2 + z1 * y1 * 2; // площадь первой упаковки

                int V_2 = x2 * y2 * z2; // объем второй упаковки
                int S_2 = x2 * y2 * 2 + x2 * z2 * 2 + z2 * y2 * 2; // площадь второй упаковки

                double cost = (c2 - S_2 * c1 / S_1) / (V_2 - S_2 * V_1 / S_1) * 1000;
                if (answer_cost == 0 || cost < answer_cost)
                {
                    answer_company = i;
                    answer_cost = cost;
                }

            }
            Console.WriteLine(answer_company + " " + answer_cost.ToString("F2")); 
        }
    }
}
