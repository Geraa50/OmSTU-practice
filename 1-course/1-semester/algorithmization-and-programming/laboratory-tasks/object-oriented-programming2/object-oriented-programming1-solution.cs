using System.Collections;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Markup;
using System.Xml;

class Student
{
    public int Number;
    public string FullName;
    public int BirtYear;
    public int EnterYear;
    public int Term;
    public string Group;

    public Student(int number, string fullname, int birtYear, int enterYear, int term, string group)
    {
        Number = number;
        FullName = fullname;
        BirtYear = birtYear;
        EnterYear = enterYear;
        Term = term;
        Group = group;
    }

    public int GetTerm()
    {
        return Term;
    }

    public string GetGroup()
    {
        return Group;
    }

    public string GetFullName()
    {
        return FullName;
    }
}

class University
{
    private Student[] students;

    public University(Student[] students)
    {
        this.students = students;
    }

    public void PrintQuantityFalse()
    {
        int ans = 0;
        foreach (Student p in students)
        {
            if (p.GetTerm() == 0)
            {
                ans++;                
            }

        }
        Console.WriteLine(ans);
    }

    public void PrintStudentsForThisGroup(int course)
    {
        int crs = 0;

        foreach (Student p in students)
        {
            crs = p.GetTerm();
            if (crs == course)
            {
                Console.WriteLine(p.GetFullName());
            }
        }
    }

    public void PrintQuantityStudentsForAllGroups()
    {
        string[] groups = new string[10000];
        int[] counts = new int[10000];
        int size = 0;

        foreach (Student p in students)
        {
            if (p.GetGroup() == "0") continue;
            int index = -1;
            for (int i = 0; i < size; i++)
            {
                if (groups[i] ==  p.GetGroup())
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                groups[size] = p.GetGroup();
                counts[size] = 1;
                size++;
            }
            else
            {
                counts[index]++;
            }

        }
        for (int j = 0;j < size; j++)
        {
            Console.WriteLine(groups[j] + " " + counts[j]);

        }


    }

}


class Program
{
    static Student[] zapolnenie(int n)
    {
        // int n = int.Parse(Console.ReadLine());

        Student[] students = new Student[n];

        for (int i = 0; i < n; i++)
        {
            string line = Console.ReadLine();
            string[] parts = line.Split();

            int number = int.Parse(parts[0]);
            string fullName = parts[1];
            int birtYear = int.Parse(parts[2]);
            int enterYear = int.Parse(parts[3]);
            int term = int.Parse(parts[4]);
            string group = parts[5];
            
            int age = enterYear - birtYear;
            if (age < 16)
            {
                term = 0;
                group = "0";
            }
            students[i] = new Student(number, fullName, birtYear, enterYear, term, group);

        }
        return students;
    }
    

    static void Main()
    {
        Student[] students = null;
        University university = null;

        Console.WriteLine("Выберите пункт меню");
        int q = int.Parse(Console.ReadLine());
        while (q != 1)
        {
            Console.WriteLine("Сначала выберите первый пункт меню нажав 1 что бы ввести массив");
            q = int.Parse(Console.ReadLine());
        }
        while (q != 5)
        {
            switch (q)
            {
                case 1: 
                    Console.WriteLine("Введите колво участников");
                    int n = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введите участников каждого с новой строки и разделяя поля пробелом");
                    students = zapolnenie(n);
                    university = new University(students);
                    break;
                case 2:
                    university.PrintQuantityFalse();
                    break;
                case 3:
                    Console.WriteLine("Введите курс, количество студентов которого хотите узнать");
                    int crs = int.Parse(Console.ReadLine());
                    university.PrintStudentsForThisGroup(crs);
                    break;
                case 4:
                    university.PrintQuantityStudentsForAllGroups();
                    break;
            }
            Console.WriteLine("Выберите пункт меню");
            q = int.Parse(Console.ReadLine());
        }
    }
}