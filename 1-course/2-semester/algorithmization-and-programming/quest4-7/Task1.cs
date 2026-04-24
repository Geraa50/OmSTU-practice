using System;
using System.Collections.Generic;

class MyStack<T>
{
    private List<T> items = new List<T>();

    public void Push(T item)
    {
        items.Add(item);
    }

    public T Peek()
    {
        if (items.Count == 0)
        {
            Console.WriteLine("Стек пуст!@!@!@!!!! Невозможно посмотреть элемент");
            return default(T);
        }
        return items[items.Count - 1];
    }

    public T Pop()
    {
        if (items.Count == 0)
        {
            Console.WriteLine("Стек пуст!@!@!@!!!! Невозможно извлечь элемент");
            return default(T);
        }
        T item = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        return item;
    }

    public int Count
    {
        get { return items.Count; }
    }

    public void Print()
    {
        if (items.Count == 0)
        {
            Console.WriteLine("Стек пуст");
            return;
        }
        Console.WriteLine("Элементы стека (сверху вниз):");
        for (int i = items.Count - 1; i >= 0; i--)
        {
            Console.WriteLine("  " + items[i]);
        }
    }
}

class Program
{
    static void Main()
    {
        MyStack<int> intStack = new MyStack<int>();
        MyStack<string> stringStack = new MyStack<string>();

        while (true)
        {
            Console.WriteLine("\nменю");
            Console.WriteLine("1 = Работа со стеком int");
            Console.WriteLine("2 = Работа со стеком string");
            Console.WriteLine("0 = Выход");

            string choice = Console.ReadLine();

            if (choice == "1")
                WorkWithIntStack(intStack);
            else if (choice == "2")
                WorkWithStringStack(stringStack);
            else if (choice == "0")
                break;
            else
                Console.WriteLine("Неверный выбор");
        }
    }

    static void WorkWithIntStack(MyStack<int> stack)
    {
        while (true)
        {
            Console.WriteLine("\n--- Стек int ---");
            Console.WriteLine("1. Добавить элемент (Push)");
            Console.WriteLine("2. Посмотреть верхний элемент (Peek)");
            Console.WriteLine("3. Извлечь верхний элемент (Pop)");
            Console.WriteLine("4. Вывести стек");
            Console.WriteLine("0. Назад");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите целое число: ");
                    int num = int.Parse(Console.ReadLine());
                    stack.Push(num);
                    Console.WriteLine("Элемент добавлен.");
                    break;
                case "2":
                    if (stack.Count > 0)
                        Console.WriteLine("Верхний элемент: " + stack.Peek());
                    else
                        stack.Peek();
                    break;
                case "3":
                    if (stack.Count > 0)
                        Console.WriteLine("Извлечён элемент: " + stack.Pop());
                    else
                        stack.Pop();
                    break;
                case "4":
                    stack.Print();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
    }

    static void WorkWithStringStack(MyStack<string> stack)
    {
        while (true)
        {
            Console.WriteLine("\n--- Стек string ---");
            Console.WriteLine("1. Добавить элемент (Push)");
            Console.WriteLine("2. Посмотреть верхний элемент (Peek)");
            Console.WriteLine("3. Извлечь верхний элемент (Pop)");
            Console.WriteLine("4. Вывести стек");
            Console.WriteLine("0. Назад");
            Console.Write("Выберите: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите строку: ");
                    string str = Console.ReadLine();
                    stack.Push(str);
                    Console.WriteLine("Элемент добавлен");
                    break;
                case "2":
                    if (stack.Count > 0)
                        Console.WriteLine("Верхний элемент: " + stack.Peek());
                    else
                        stack.Peek();
                    break;
                case "3":
                    if (stack.Count > 0)
                        Console.WriteLine("Извлечён элемент: " + stack.Pop());
                    else
                        stack.Pop();
                    break;
                case "4":
                    stack.Print();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
    }
}
