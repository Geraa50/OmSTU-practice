class Program
{
    static int func(int n)
    {
        if (n < 3) return 0;
        else if (n == 3) return 1;
        else
        {
            return func(n / 2) + func((n + 1) / 2);
        }
    }
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        Console.WriteLine(func(n));
    }
}