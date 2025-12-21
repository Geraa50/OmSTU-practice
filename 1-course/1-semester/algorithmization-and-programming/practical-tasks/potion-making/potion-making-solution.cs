using System;

class Program
{
    static string SpellFormation(string action, string[] ingredients)
    {
        string nachalo = "";
        string konec = "";

        switch (action)
        {
            case "MIX":
                nachalo = "MX";
                konec = "XM";
                break;
            case "WATER":
                nachalo = "WT";
                konec = "TW";
                break;
            case "DUST":
                nachalo = "DT";
                konec = "TD";
                break;
            case "FIRE":
                nachalo = "FR";
                konec = "RF";
                break;
        }

        string centr = "";
        foreach (var ing in ingredients)
            centr += ing;

        return nachalo + centr + konec;
    }

    static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        string[] answers = new string[n];
        string answer = "";

        for (int i = 0; i < n; i++)
        {
            string inpNow = Console.ReadLine();
            string[] parts = inpNow.Split(' ');

            string action = parts[0];
            string[] ingredients = new string[parts.Length - 1];

            for (int j = 1; j < parts.Length; j++)
            {
                // если число то тогда значим берем берем результат предыдущего действия
                if (char.IsDigit(parts[j][0]))
                {
                    int index = int.Parse(parts[j]) - 1;
                    ingredients[j - 1] = answers[index];
                }
                else
                {
                    ingredients[j - 1] = parts[j];
                }
            }

            answer = SpellFormation(action, ingredients);
            answers[i] = answer;
        }

        Console.WriteLine(answer);
    }
}
