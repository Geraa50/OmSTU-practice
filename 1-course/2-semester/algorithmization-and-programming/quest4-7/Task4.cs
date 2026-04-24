using System;
using System.Collections.Generic;

class MyStack
{
    private List<char> items = new List<char>();

    public void Push(char item)
    {
        items.Add(item);
    }

    public char Pop()
    {
        if (items.Count == 0)
        {
            return '\0';
        }
        char item = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        return item;
    }

    public char Peek()
    {
        if (items.Count == 0)
        {
            return '\0';
        }
        return items[items.Count - 1];
    }

    public bool IsEmpty()
    {
        return items.Count == 0;
    }
}

class Program
{
    static void Main()
    {
        string expression = Console.ReadLine();

        MyStack stack = new MyStack();
        bool isValid = true;

        for (int i = 0; i < expression.Length; i++)
        {
            char c = expression[i];

            if (c == '(' || c == '[' || c == '{')
            {
                stack.Push(c);
            }
            else if (c == ')' || c == ']' || c == '}')
            {
                if (stack.IsEmpty())
                {
                    Console.WriteLine("Ошибка!!Ё!Ё!Ё! Найдена закрывающая скобка '" + c +
                                      "' на позиции " + (i + 1) + ", но нет соответствующей открывающей");
                    isValid = false;
                    break;
                }

                char top = stack.Peek();

                if ((c == ')' && top == '(') ||
                    (c == ']' && top == '[') ||
                    (c == '}' && top == '{'))
                {
                    stack.Pop();
                }
                else
                {
                    Console.WriteLine("Ошибка! Скобка '" + c + "' на позиции " + (i + 1) +
                                      " не соответствует открывающей скобке '" + top + "'@!@!@!@");
                    isValid = false;
                    break;
                }
            }
        }

        if (isValid && !stack.IsEmpty())
        {
            Console.WriteLine("Ошибка! Остались незакрытые скобки");
            isValid = false;
        }

        if (isValid)
        {
            Console.WriteLine("Выражение корректно! Все скобки расставлены правильно");
        }
    }
}
