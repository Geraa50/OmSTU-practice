class Program
{
    static string inpt()
    {
        Console.Write("Введите строку:");
        return Console.ReadLine();
    }

    static void outp(string otp)
    {
        Console.Write("Ваша строка:");
        Console.WriteLine(otp);
    }

    static void schetOdinakSlov(string stroka)
    {
        Console.WriteLine("слово | встречается");
        int answer = 0;
        string[] words = stroka.Split(' ');
        string word = "";
        for (int i = 0; i < words.Length; i++)
        {
            word = words[i];
            answer = 1;
            for (int j = 0; j < words.Length; j++)
            {
                if (word == words[j]) answer++;
            }
            Console.Write($"{word} ");
            Console.WriteLine(answer);
        }
    }

    static void schetChetLenght(string stroka)
    {
        Console.Write("Слов с четной длиной: ");
        int answer = 0;
        string[] words = stroka.Split(' ');
        string word = "";
        int len = 0;
        for (int i = 0; i < words.Length; i++)
        {
            word = words[i];
            len = word.Length;
            if (len % 2 == 0) answer += 1;
        }
        Console.WriteLine(answer);
    }
    
    static void OpredCifrWhyNot(string stroka)
    {
        Console.WriteLine("Цифр которых нет в строке: ");
        int[] kotorieEst = {-1, -1, -1, -1, -1, -1, -1, -1, -1};
        int count = 0;
        string[] words = stroka.Split(' ');
        string word = "";
        for (int i = 0; i < words.Length; i++)
        {
            word = words[i];
            if (word == "0") kotorieEst[0] = 0;
            else if (word == "1") kotorieEst[1] = 1;
            else if (word == "2") kotorieEst[2] = 2;
            else if (word == "3") kotorieEst[3] = 3;
            else if (word == "4") kotorieEst[4] = 4;
            else if (word == "5") kotorieEst[5] = 5;
            else if (word == "6") kotorieEst[6] = 6;
            else if (word == "7") kotorieEst[7] = 7;
            else if (word == "8") kotorieEst[8] = 8;
            else if (word == "9") kotorieEst[9] = 9;
        }
        for (int j = 0; j < kotorieEst.Length; j++)
        {
            if (kotorieEst[j] == -1) Console.Write($"{j}, ");
        }
        Console.WriteLine();
    }

    static void Main()
    {
        string stroka = "";
        int flag = 0;
        int inp = 0;

        while (flag != 4)
        {
            inp = int.Parse(Console.ReadLine());
            switch (inp)
            {
                case 1: stroka = inpt(); flag = 1; break;

                case 2: if (flag == 1) outp(stroka); else Console.WriteLine("Сначала введите строку"); break;

                case 3: schetOdinakSlov(stroka); break;
                
                case 4: schetChetLenght(stroka); break;

                case 5: OpredCifrWhyNot(stroka); break;

                case 6: flag = 4; break;
            }
        }

    }
}