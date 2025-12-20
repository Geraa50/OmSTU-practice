using System;

namespace Houses
{
    class Program
    {
        static void Main()
        {
            int X = 3;
            int Y = 4;
            int L = 12;
            int remont = 10;
            int razbor = 4;
            int stroitelstvoOld = 7;
            int stroitelstvoNew = 6;
            int costNewMaterial = 6;
            int clearOldMaterial = 3;

            int P = (X + Y) * 2;
            int answer = 0;
            if (Y > X)
            {
                (X, Y) = (Y, X);
            }
            // теперь у нас X максимальная стена 


            // разбитие стены на пригодную и непригодную для реставрации
            int prigodnStenaForRestavration = 0;
            int trash = 0;
            if (L > X)
            {
                prigodnStenaForRestavration = X;
                trash = L - prigodnStenaForRestavration;
            }
            else
            {
                prigodnStenaForRestavration = L;
                trash = 0;
            }

            // что выгоднее сделать с имеющейся стеной, которую потенциально можно использовать
            if (remont < razbor + stroitelstvoOld || remont < razbor + clearOldMaterial)
            {
                // ремонтируем имеющуюся
                P -= prigodnStenaForRestavration;
                answer += remont * prigodnStenaForRestavration;
            }
            else if (stroitelstvoOld < clearOldMaterial + stroitelstvoNew + costNewMaterial)
            {
                // разбираем и из неё же строим
                P -= prigodnStenaForRestavration;
                answer += (razbor + stroitelstvoOld) * prigodnStenaForRestavration;
            }
            else
            {
                // разбираем и вывозим
                P -= 0;
                answer += (razbor + clearOldMaterial) * prigodnStenaForRestavration;
            }

            // к этому моменту мы разобрались с стеной которую потенциально можно было использовать (если экономически целесеобразно)
            // теперь будем разбираться с остатками стены которые точно нельзя реставрировать

            answer += trash * razbor; // однозначно её надо разобрать
            if (clearOldMaterial + costNewMaterial + stroitelstvoNew < stroitelstvoOld)
            {
                // вывозим
                P -= 0;
                answer += clearOldMaterial * trash;

            }
            else
            {
                // используем для строительства
                if (trash < P)
                {
                    // используем мусор по максимуму
                    P -= trash;
                    answer += trash * stroitelstvoOld;
                }
                else
                {
                    // если мусора больше чем периметра, то весь периметр застраиваем мусором, а остаток вывозим
                    trash -= P;
                    answer += P * stroitelstvoOld + clearOldMaterial * trash;
                    P = 0;
                }
            }
            // к этому моменту мы разобрались с имеющейся стеной и то есть или реставрация или разбор + строительство или разбор + вывоз
            //  к этому моменту у нас уже есть некая стоимость в answer и оставшийся периметр в P (или уже нет) который надо застроить
            if (P > 0)
            {
                answer += P * (costNewMaterial + stroitelstvoNew);
                Console.WriteLine(answer);
            }
            else if (P == 0)
            {
                Console.WriteLine(answer);
            }
            else
            {
                Console.WriteLine("Ошибка, P = " + P);
                Console.WriteLine("answer = " + answer);
            }
        }
    }
}
