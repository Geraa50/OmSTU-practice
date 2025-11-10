
class MyFunctions
{
    public int[] inpt(int n)
    {
        int[] massiv = new int[n];
        int elem = 0;
        for (int i = 0; i < n; i++)
        {
            elem = int.Parse(Console.ReadLine());
            massiv[i] = elem;
        }
        return massiv;
    }
    public void outp(int[] massiv)
    {
        for (int j = 0; j < massiv.Length; j++)
        {
            Console.Write(massiv[j] + " ");

        }
        Console.WriteLine();

    }
    public int[] replcmnt(int[] massiv)
    {
        int max = 0;
        for (int q = 0; q < massiv.Length; q++)
        {
            if (massiv[q] > max)
            {
                max = massiv[q];
            }
        }
        for (int e = 0; e < massiv.Length; e++)
        {
            if (massiv[e] == 0)
            {
                massiv[e] = max;
            }
        }
        return massiv;
    }
/*
}

class Program
{
*/
    static void Main()
    {
        MyFunctions mf = new MyFunctions();
        int[] arr = null;
        //int n = int.Parse(Console.ReadLine());
        //arr = mf.inpt(n);
        //mf.outp(arr);
        int inp = 0;
        int flag = 0;
        while (flag != 4)
        {
            inp = int.Parse(Console.ReadLine());
            switch (inp)
            {
                case 1:
                    int n = int.Parse(Console.ReadLine());
                    arr = mf.inpt(n);
                    flag = 1;
                    break;
                case 2:
                    if (flag == 1)
                    {
                        mf.outp(arr);
                    }
                    else
                    {
                        Console.WriteLine("Ошибка, массив еще не задан");
                    }
                    break;
                case 3:
                    if (flag == 1)
                    {
                        mf.replcmnt(arr);
                    }
                    else
                    {
                        Console.WriteLine("Ошибка, массив еще не задан");
                    }
                    break;
                case 4:
                    flag = 4;
                    break;

            }
        }
    }
}
