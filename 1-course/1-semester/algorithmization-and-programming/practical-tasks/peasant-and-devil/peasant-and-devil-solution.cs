class Program
{
    static bool podhodit(int n, int k, int z)
    {
        for (int i = 0; i < z; i++)
        {
            n = n * 2 - k;
            if (n < 0) return false;
        }
        return n == 0;
    }
    static void Main()
    {
        int MaxN = int.Parse(Console.ReadLine());
        int answer = 0;
        // int N = 1;
        // int K = 0; // минимальный К должен быть > N * 2
        for (int Z = 1; Z <= 30; Z++)
        {
            int pow2 = 1 << Z; // 2 в степени з
            int denom = pow2 - 1;
            for (int N = 1; N <= MaxN; N++)
            {
                int num = N * pow2;
                if (num % denom != 0) continue;

                int K = num / denom;
                if (K <= 0) continue;

                if (podhodit(N, K, Z)) answer ++;
            }
        }
        Console.WriteLine(answer);

    }
}