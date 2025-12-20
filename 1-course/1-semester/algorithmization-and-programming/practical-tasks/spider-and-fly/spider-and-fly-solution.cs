
// паук и муха
using System.Collections;

class Program
{
    static int Coord(int x, int y, int z, int x_c, int y_c, int z_c)
    {
        int answer = 0;
        if (x == 0) answer = 1;
        else if (y == 0) answer = 2;
        else if (z == 0) answer = 3;
        else if (x == x_c) answer = 4;
        else if (y == y_c) answer = 5;
        else if (z == z_c) answer = 6;
        return answer;
    }
    static void Main()
    {
        string korob = Console.ReadLine();
        string spider = Console.ReadLine();
        string myha = Console.ReadLine();

        string[] korob_cords = korob.Split(' ');
        string[] spider_cords = spider.Split(' ');
        string[] myha_cords = myha.Split(' ');

        int korob_x = int.Parse(korob_cords[0]);
        int korob_y = int.Parse(korob_cords[1]);
        int korob_z = int.Parse(korob_cords[2]);

        int spider_x = int.Parse(spider_cords[0]);
        int spider_y = int.Parse(spider_cords[1]);
        int spider_z = int.Parse(spider_cords[2]);

        int myha_x = int.Parse(myha_cords[0]);
        int myha_y = int.Parse(myha_cords[1]);
        int myha_z = int.Parse(myha_cords[2]);
        // теперь у нас есть все коордианты в виде отельных переменных
        // теперь нужно определить на каких стенах расположены паук и муха 
        // (то есть проверить какая из координат паука и мухи равна или соответствующей координате коробка или 0)
        // таким образом можно перенести из 3д в 2д развертку коробки, так как одна координата уже известна
        // после определения координат на развертке коробки надо создать список всех возможных путей
        // то есть будут все возможные пути, в том числе и самый короткий и потом функцией мин мы находим ответ

        // распределение стен 0 0 0 макс x y z это будут 1 2 3 4 5 6
        int spiderZeroCord = Coord(spider_x, spider_y, spider_z, korob_x, korob_y, korob_z);
        int muhaZeroCord = Coord(myha_x, myha_y, myha_z, korob_x, korob_y, korob_z);

        Console.WriteLine(spiderZeroCord);
        Console.WriteLine(muhaZeroCord);
        
        int locationOption = spiderZeroCord - muhaZeroCord;
        // если разница = 0 то на одной стороне, если разница 3 то на параллельных в остальных случаях означает на соседних
        
        // на одной стороне -- РЕАЛИЗОВАНО
        // на соседних сторонах -- 
        // на параллельных сторонах -- 
        if (locationOption == 0)
        {
            // на одной стороне
            Console.WriteLine("Паук и муха находятся на одной стороне");
            int x = (int)Math.Pow(myha_x - spider_x, 2);
            int y = (int)Math.Pow(myha_y - spider_y, 2);
            int z = (int)Math.Pow(myha_z - spider_z, 2);
            /*
            Console.WriteLine(myha_x);
            Console.WriteLine(myha_y);
            Console.WriteLine(myha_z);
            Console.WriteLine(spider_x);
            Console.WriteLine(spider_y);
            Console.WriteLine(spider_z);

            Console.WriteLine(x);
            Console.WriteLine(y);
            Console.WriteLine(z);
            */
            Console.WriteLine($"{Math.Sqrt(x + y + z):F3}");    
        }
        else if (locationOption == 3 || locationOption == -3)
        {
            Console.WriteLine("Паук и муха находятся на параллельных сторонах");
            int[] answers = new int[4]; // перебрать все возможные варианты прохода через все 4 стороны
            // и через max найти кротчайший
            // при этом будут такие варианты + x - x + y - y вот они 4 варианта проекции
            double[] ans = new double[4];

            if ((spider_x == 0 && myha_x == korob_x) || (spider_x == korob_x && myha_x == 0))
            {
                // через потолок
                ans[0] = Math.Sqrt(
                    Math.Pow(spider_y - myha_y, 2) +
                    Math.Pow(spider_z + (korob_z - myha_z), 2)
                );  

                // через пол
                ans[1] = Math.Sqrt(
                    Math.Pow(spider_y - myha_y, 2) +
                    Math.Pow((korob_z - spider_z) + myha_z, 2)
                );

                // через переднюю стену
                ans[2] = Math.Sqrt(
                    Math.Pow(spider_z - myha_z, 2) +
                    Math.Pow(spider_y + (korob_y - myha_y), 2)
                );

                // через заднюю стену
                ans[3] = Math.Sqrt(
                    Math.Pow(spider_z - myha_z, 2) +
                    Math.Pow((korob_y - spider_y) + myha_y, 2)
                );
            }

            double result = ans.Min();
            Console.WriteLine($"{result:F3}");

            // ------------------------------------------------------------

        }
        else
        {
            Console.WriteLine("Паук и муха находятся на соседних сторонах");
            double answer = double.MaxValue;

            // паук на y=0, муха на x=a
            if (spider_y == 0 && myha_x == korob_x || myha_y == 0 && spider_x == korob_x)
            {
                double dx = spider_x + myha_y;
                double dz = Math.Abs(spider_z - myha_z);
                answer = Math.Sqrt(dx * dx + dz * dz);
            }

            // паук на y=0, муха на x=0
            if (spider_y == 0 && myha_x == 0 || myha_y == 0 && spider_x == 0)
            {
                double dx = Math.Abs(spider_x - myha_y);
                double dz = Math.Abs(spider_z - myha_z);
                answer = Math.Min(answer, Math.Sqrt(dx * dx + dz * dz));
            }

            // паук на z=0, муха на x=a
            if (spider_z == 0 && myha_x == korob_x || myha_z == 0 && spider_x == korob_x)
            {
                double dx = spider_x + myha_z;
                double dy = Math.Abs(spider_y - myha_y);
                answer = Math.Min(answer, Math.Sqrt(dx * dx + dy * dy));
            }

            // аналогично для остальных комбинаций (x/y/z)

            Console.WriteLine($"{answer:F3}");

            
        }

        
        
    }
}
