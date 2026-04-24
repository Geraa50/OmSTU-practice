// Подключаем System для консольного ввода/вывода
using System;
// Подключаем System.Collections.Generic для работы с List<T>
using System.Collections.Generic;

// Обобщённый класс MyStack<T> — имитация стека с использованием List
// T — это параметр типа (обобщённый тип): вместо T подставляется конкретный тип (int, string и т.д.)
class MyStack<T>
{
    // Внутренний список для хранения элементов стека
    // private — доступ только внутри этого класса
    private List<T> items = new List<T>();

    // Метод Push — добавление элемента на вершину стека
    // В списке вершина стека — это последний элемент
    public void Push(T item)
    {
        // Добавляем элемент в конец списка (это и будет вершина стека)
        items.Add(item);
    }

    // Метод Peek — просмотр верхнего элемента БЕЗ удаления
    public T Peek()
    {
        // Проверяем, не пуст ли стек
        if (items.Count == 0)
        {
            Console.WriteLine("Стек пуст! Невозможно посмотреть элемент");
            // default(T) возвращает значение по умолчанию для типа T
            // Для int это 0, для string это null
            return default(T);
        }
        // Возвращаем последний элемент списка (вершину стека)
        // items.Count - 1 — индекс последнего элемента
        return items[items.Count - 1];
    }

    // Метод Pop — извлечение верхнего элемента С удалением
    public T Pop()
    {
        // Проверяем, не пуст ли стек
        if (items.Count == 0)
        {
            Console.WriteLine("Стек пуст!@!@!@!!!! Невозможно извлечь элемент");
            return default(T);
        }
        // Сохраняем верхний элемент в переменную
        T item = items[items.Count - 1];
        // Удаляем последний элемент из списка
        items.RemoveAt(items.Count - 1);
        // Возвращаем сохранённый элемент
        return item;
    }

    // Свойство Count — возвращает количество элементов в стеке
    public int Count
    {
        get { return items.Count; }
    }

    // Метод Print — выводит все элементы стека от вершины к основанию
    public void Print()
    {
        if (items.Count == 0)
        {
            Console.WriteLine("Стек пуст");
            return;
        }
        Console.WriteLine("Элементы стека (сверху вниз):");
        // Идём от последнего элемента к первому — от вершины к основанию
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
        // Создаём два объекта стека: один для целых чисел, другой для строк
        // Это и есть использование обобщённого класса с разными типами
        MyStack<int> intStack = new MyStack<int>();
        MyStack<string> stringStack = new MyStack<string>();

        // Главное меню программы
        while (true)
        {
            Console.WriteLine("\nменю");
            Console.WriteLine("1 = Работа со стеком int");
            Console.WriteLine("2 = Работа со стеком string");
            Console.WriteLine("0 = Выход");

            string choice = Console.ReadLine();

            // В зависимости от выбора вызываем нужный метод
            if (choice == "1")
                WorkWithIntStack(intStack);      // Передаём стек int
            else if (choice == "2")
                WorkWithStringStack(stringStack); // Передаём стек string
            else if (choice == "0")
                break; // Выходим из программы
            else
                Console.WriteLine("Неверный выбор");
        }
    }

    // Метод для работы со стеком целых чисел
    static void WorkWithIntStack(MyStack<int> stack)
    {
        while (true)
        {
            // Подменю для стека int
            Console.WriteLine("\n--- Стек int ---");
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
                    // Просим ввести число и добавляем его в стек
                    Console.Write("Введите целое число: ");
                    int num = int.Parse(Console.ReadLine());
                    stack.Push(num);
                    Console.WriteLine("Элемент добавлен.");
                    break;
                case "2":
                    // Показываем верхний элемент без удаления
                    // Сначала проверяем count, чтобы не выводить default значение
                    if (stack.Count > 0)
                        Console.WriteLine("Верхний элемент: " + stack.Peek());
                    else
                        stack.Peek(); // Метод сам выведет сообщение о пустом стеке
                    break;
                case "3":
                    // Извлекаем верхний элемент с удалением
                    if (stack.Count > 0)
                        Console.WriteLine("Извлечён элемент: " + stack.Pop());
                    else
                        stack.Pop(); // Метод сам выведет сообщение о пустом стеке
                    break;
                case "4":
                    // Выводим все элементы стека
                    stack.Print();
                    break;
                case "0":
                    return; // Возврат в главное меню
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
    }

    // Метод для работы со стеком строк — аналогичен методу для int
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
                    // Просим ввести строку и добавляем в стек
                    Console.Write("Введите строку: ");
                    string str = Console.ReadLine();
                    stack.Push(str);
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
}
