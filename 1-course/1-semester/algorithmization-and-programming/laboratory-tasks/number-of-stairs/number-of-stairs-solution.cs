using System;

class Program
{
    static int ways(int left, int maxBlock)
    {
        if (left == 0) return 1;
        if (left < 0) return 0;
        if (maxBlock == 0) return 0;

        int count = 0;
        for (int next = maxBlock; next >= 1; next--)
            count += ways(left - next, next - 1);

        return count;
    }

    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        Console.WriteLine(ways(n, n - 1) + 1);
    }
}
