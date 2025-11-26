class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        string[] inpt = new string[n];

        // каждое действие просто обратное если - то + если умножить то разделить
        for (int i = 0; i < n; i++)
        {
            inpt[i] = Console.ReadLine();
        }

        Console.WriteLine("ВВедите R (итоговое число)");
        double R = int.Parse(Console.ReadLine());
        Console.WriteLine();

        string stroka = "";
        string deistv = "";
        int chislo = 0;
        string[] strokaMassiv = [];
        double koefficientX = 0;
        double answer = 0;

        for (int j = n - 1; j >= 0; j--)
        {
            
            stroka = inpt[j];
            strokaMassiv = stroka.Split(' ');
            deistv = strokaMassiv[0];
            if (strokaMassiv[1] == "x")
            {
                if (deistv == "+") koefficientX--;
                else koefficientX++;

            }
            else
            {
                chislo = int.Parse(strokaMassiv[1]);
                if (deistv == "+") R-= chislo;
                else if (deistv == "-") R+= chislo;
                else //умножение превращаем в деление
                {
                    R /= chislo;
                    koefficientX /= chislo;
                }
            }
            Console.WriteLine($"{R}  {koefficientX}x");
        }
        koefficientX--;
        Console.WriteLine($"{R}  {koefficientX}x");
        
        answer = R / koefficientX;
        Console.WriteLine(answer * -1);
    }
}