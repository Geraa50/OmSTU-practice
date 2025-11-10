class Program
{
    static int Coord(int x, int y, int z, int x_c, int y_c, int z_c)
    {
        int answer = 0;
        if (x == 0) answer = 1;
        if (y == 0) answer = 2;
        if (z == 0) answer = 3;
        if (x == x_c) answer = 4;
        if (y == y_c) answer = 5;
        if (z == z_c) answer = 6;
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
        
        
        
    }
}