using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

class Client { public int Id; public string Fio = ""; public string AccountNumber = ""; }
class OperationKind { public int Id; public string Name = ""; }
class MoneyMovement { public int ClientId; public int OperationId; public DateTime Date; public decimal Amount; }

class Program
{
    static List<Client> clients = new List<Client>();
    static List<OperationKind> operations = new List<OperationKind>();
    static List<MoneyMovement> movements = new List<MoneyMovement>();

    static int nextClientId = 1, nextOpId = 1;

    static void Main()
    {
        SeedBankOperations();

        var menu = new (string Key, string Title, Action Action)[]
        {
            ("1", "Добавить клиента", AddClient),
            ("2", "Добавить наименование операции (тип)", AddOperationKind),
            ("3", "Добавить движение денег (операцию по счёту)", AddMoneyMovement),
            ("4", "Клиенты с ненулевым балансом", ShowNonZeroBalance),
            ("5", "Клиенты с нулевым балансом", ShowZeroBalance),
            ("6", "Движения по клиенту (по дате)", ShowMovementsByClient),
            ("7", "Показать всё", ShowBankAll),
        };

        while (true)
        {
            Console.WriteLine("\nбанки");
            menu.ToList().ForEach(m => Console.WriteLine(m.Key + " = " + m.Title));
            Console.WriteLine("0 = Выход");
            string c = Console.ReadLine() ?? "";
            if (c == "0") return;
            menu.FirstOrDefault(m => m.Key == c).Action?.Invoke();
        }
    }

    static void SeedBankOperations()
    {
        if (operations.Any()) return;
        operations.Add(new OperationKind { Id = nextOpId++, Name = "поступление" });
        operations.Add(new OperationKind { Id = nextOpId++, Name = "списание" });
    }

    static void AddClient()
    {
        string fio = Ask("ФИО: ");
        string acc = (Ask("Номер счёта: ")).Trim();
        if (clients.Any(x => x.AccountNumber == acc)) { Console.WriteLine("Такой счёт уже есть"); return; }
        int id = nextClientId++;
        clients.Add(new Client { Id = id, Fio = fio, AccountNumber = acc });
        Console.WriteLine("Клиент добавлен, id = " + id);
    }

    static void AddOperationKind()
    {
        string name = (Ask("Наименование операции (например, поступление / списание): ")).Trim();
        if (string.IsNullOrEmpty(name)) { Console.WriteLine("Пустое имя"); return; }
        if (operations.Any(o => string.Equals(o.Name, name, StringComparison.OrdinalIgnoreCase)))
        { Console.WriteLine("Такое наименование уже есть"); return; }
        int id = nextOpId++;
        operations.Add(new OperationKind { Id = id, Name = name });
        Console.WriteLine("Тип добавлен, id = " + id);
    }

    static decimal MovementDelta(MoneyMovement m)
    {
        var op = operations.FirstOrDefault(o => o.Id == m.OperationId);
        if (op == null) return 0;
        string n = op.Name.ToLowerInvariant();
        if (n.Contains("списан")) return -m.Amount;
        if (n.Contains("поступ")) return m.Amount;
        return 0;
    }

    static decimal Balance(int clientId) =>
        movements.Where(m => m.ClientId == clientId).Sum(MovementDelta);

    static void AddMoneyMovement()
    {
        if (!clients.Any()) { Console.WriteLine("Сначала добавьте клиента"); return; }
        if (!operations.Any()) { Console.WriteLine("Нет типов операций"); return; }
        int cid = ReadInt("Id клиента: ");
        if (!clients.Any(c => c.Id == cid)) { Console.WriteLine("Клиент не найден"); return; }

        Console.WriteLine("Типы операций:");
        operations.ForEach(o => Console.WriteLine($"  {o.Id}: {o.Name}"));
        int oid = ReadInt("Id операции: ");
        var op = operations.FirstOrDefault(o => o.Id == oid);
        if (op == null) { Console.WriteLine("Операция не найдена"); return; }

        DateTime dt;
        while (true)
        {
            try
            {
                dt = DateTime.Parse(Ask("Дата операции (дд.мм.гггг): "), CultureInfo.GetCultureInfo("ru-RU"));
                break;
            }
            catch { Console.WriteLine("Неверный формат даты"); }
        }

        decimal sum;
        while (true)
        {
            Console.Write("Сумма (> 0): ");
            string s = (Console.ReadLine() ?? "").Replace(',', '.');
            if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out sum) && sum > 0) break;
            Console.WriteLine("Нужно положительное число");
        }

        decimal current = Balance(cid);
        string low = op.Name.ToLowerInvariant();
        decimal delta;
        if (low.Contains("списан")) delta = -sum;
        else if (low.Contains("поступ")) delta = sum;
        else
        {
            Console.WriteLine("Неизвестный тип: в наименовании должно быть «поступление» или «списание».");
            return;
        }

        if (current + delta < 0)
        {
            Console.WriteLine("Операция отклонена: доходы минус расходы стали бы отрицательными.");
            Console.WriteLine($"Текущий баланс: {current}; после операции было бы: {current + delta}");
            return;
        }

        movements.Add(new MoneyMovement { ClientId = cid, OperationId = oid, Date = dt.Date, Amount = sum });
        Console.WriteLine("Движение записано");
    }

    static void ShowNonZeroBalance()
    {
        var lines = clients
            .Select(c => new { c, b = Balance(c.Id) })
            .Where(x => x.b != 0)
            .OrderBy(x => x.c.Fio)
            .Select(x => $"{x.c.Id}: {x.c.Fio} | счёт {x.c.AccountNumber} | баланс: {x.b}");
        PrintLines(lines, "Нет клиентов с ненулевым балансом");
    }

    static void ShowZeroBalance()
    {
        if (!clients.Any()) { Console.WriteLine("Нет клиентов"); return; }
        var zero = clients.Where(c => Balance(c.Id) == 0).OrderBy(c => c.Fio).ToList();
        PrintLines(zero.Select(c => $"{c.Id}: {c.Fio} | счёт {c.AccountNumber}"), "Нет клиентов с нулевым балансом");
    }

    static void ShowMovementsByClient()
    {
        if (!clients.Any()) { Console.WriteLine("Нет клиентов"); return; }
        int cid = ReadInt("Id клиента: ");
        if (!clients.Any(c => c.Id == cid)) { Console.WriteLine("Клиент не найден"); return; }
        var list = movements.Where(m => m.ClientId == cid).OrderBy(m => m.Date).ToList();
        if (!list.Any()) { Console.WriteLine("Движений нет"); return; }
        foreach (var m in list)
        {
            string on = operations.FirstOrDefault(o => o.Id == m.OperationId)?.Name ?? "?";
            Console.WriteLine($"{m.Date:dd.MM.yyyy} | {on} | сумма {m.Amount} | изменение баланса: {MovementDelta(m)}");
        }
    }

    static void ShowBankAll()
    {
        Console.WriteLine("\n--- Клиенты ---");
        PrintLines(clients.Select(c => $"{c.Id}: {c.Fio} | {c.AccountNumber} | баланс: {Balance(c.Id)}"), "нет");
        Console.WriteLine("\n--- Типы операций ---");
        PrintLines(operations.Select(o => $"{o.Id}: {o.Name}"), "нет");
        Console.WriteLine("\n--- Движения ---");
        PrintLines(movements.OrderBy(m => m.Date).ThenBy(m => m.ClientId).Select(m =>
        {
            string on = operations.FirstOrDefault(o => o.Id == m.OperationId)?.Name ?? "?";
            string cf = clients.FirstOrDefault(c => c.Id == m.ClientId)?.Fio ?? "?";
            return $"{m.Date:dd.MM.yyyy} | клиент {cf} | {on} | {m.Amount}";
        }), "нет");
    }

    static string Ask(string prompt) { Console.Write(prompt); return Console.ReadLine() ?? ""; }

    static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";
            if (input.All(char.IsDigit) && input.Length > 0) return int.Parse(input);
            Console.WriteLine("Нужно ввести только цифры");
        }
    }

    static void PrintLines(IEnumerable<string> lines, string empty)
    {
        var list = lines.ToList();
        if (!list.Any()) Console.WriteLine(empty);
        else list.ForEach(Console.WriteLine);
    }
}
