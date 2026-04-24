using System;
using System.Collections.Generic;
using System.Linq;

class Plane { public string Type; public int Seats; public double Capacity; public int Range; }
class SoldTickets { public int FlightNumber; public int TicketsSold; }
class Flight { public int FlightNumber; public string PlaneType; public DateTime DepartureTime; public string DeparturePoint; public string ArrivalPoint; }
class Distance { public string CityA; public string CityB; public int Km; }

class Program
{
    static List<Plane> planes = new List<Plane>();
    static List<SoldTickets> soldTickets = new List<SoldTickets>();
    static List<Flight> flights = new List<Flight>();
    static List<Distance> distances = new List<Distance>();

    static void Main()
    {
        var menu = new (string Key, string Title, Action Action)[]
        {
            ("1", "Добавить самолёт (тип)", AddPlane),
            ("2", "Добавить рейс", AddFlight),
            ("3", "Добавить запись о продаже билетов", AddSoldTickets),
            ("4", "Добавить расстояние между городами", AddDistance),
            ("5", "Показать всё", ShowAll),
            ("6", "Какие типы самолётов могут заменить на выбранном рейсе", ShowReplacements),
            ("7", "Рейсы с полной загрузкой самолётов", ShowFullLoadedFlights),
            ("8", "Средняя загрузка по каждому типу самолёта", ShowAverageLoadByType),
            ("9", "Самолёты, вылетающие из заданного пункта (сортировка по времени)", ShowDepartingFromPoint),
            ("10", "Самолёты, прилетающие в заданный пункт", ShowArrivingToPoint),
            ("11", "Самолёты, сгруппированные по часу вылета", ShowGroupedByHour),
        };

        while (true)
        {
            Console.WriteLine("\nаэропорт");
            menu.ToList().ForEach(m => Console.WriteLine(m.Key + " = " + m.Title));
            Console.WriteLine("0 = Выход");
            string choice = Console.ReadLine();
            if (choice == "0") return;
            menu.FirstOrDefault(m => m.Key == choice).Action?.Invoke();
        }
    }

    static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input) && input.All(char.IsDigit)) return int.Parse(input);
            Console.WriteLine("Нужно ввести только цифры");
        }
    }

    static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            try { return double.Parse(Console.ReadLine().Replace('.', ',')); }
            catch { Console.WriteLine("Неверный формат числа"); }
        }
    }

    static string Ask(string prompt) { Console.Write(prompt); return Console.ReadLine(); }

    static void AddPlane()
    {
        string type = Ask("Введите тип самолёта: ");
        if (planes.Any(p => p.Type == type)) { Console.WriteLine("Самолёт с таким типом уже существует!"); return; }
        planes.Add(new Plane
        {
            Type = type,
            Seats = ReadInt("Введите количество мест: "),
            Capacity = ReadDouble("Введите грузоподъёмность (тонн): "),
            Range = ReadInt("Введите дальность полёта (км): ")
        });
        Console.WriteLine("Ура, победа, самолёт добавлен!");
    }

    static void AddFlight()
    {
        int number = ReadInt("Введите номер рейса: ");
        if (flights.Any(f => f.FlightNumber == number)) { Console.WriteLine("Рейс с таким номером уже существует!"); return; }
        string type = Ask("Введите тип самолёта: ");
        if (!planes.Any(p => p.Type == type)) { Console.WriteLine("Такого типа самолёта нет в списке!"); return; }
        DateTime dt;
        while (true)
        {
            try { dt = DateTime.Parse(Ask("Введите время вылета (дд.мм.гггг чч:мм): ")); break; }
            catch { Console.WriteLine("Неверный формат даты! Попробуйте ещё раз"); }
        }
        flights.Add(new Flight { FlightNumber = number, PlaneType = type, DepartureTime = dt,
            DeparturePoint = Ask("Введите пункт вылета: "), ArrivalPoint = Ask("Введите пункт прилёта: ") });
        Console.WriteLine("Ура, победа, рейс добавлен!");
    }

    static void AddSoldTickets()
    {
        int number = ReadInt("Введите номер рейса: ");
        if (!flights.Any(f => f.FlightNumber == number)) { Console.WriteLine("Рейс не найден!"); return; }
        if (soldTickets.Any(s => s.FlightNumber == number)) { Console.WriteLine("Запись о билетах на этот рейс уже существует!"); return; }
        soldTickets.Add(new SoldTickets { FlightNumber = number, TicketsSold = ReadInt("Введите количество проданных билетов: ") });
        Console.WriteLine("Запись добавлена");
    }

    static void AddDistance()
    {
        string a = Ask("Введите первый город: "), b = Ask("Введите второй город: ");
        if (distances.Any(x => (x.CityA == a && x.CityB == b) || (x.CityA == b && x.CityB == a)))
        { Console.WriteLine("Такое расстояние уже есть!"); return; }
        distances.Add(new Distance { CityA = a, CityB = b, Km = ReadInt("Введите расстояние (км): ") });
        Console.WriteLine("Расстояние добавлено");
    }

    static int FindDistance(string a, string b) => distances
        .Where(x => (x.CityA == a && x.CityB == b) || (x.CityA == b && x.CityB == a))
        .Select(x => x.Km).DefaultIfEmpty(-1).First();

    static Plane FindPlane(string type) => planes.FirstOrDefault(p => p.Type == type);

    static int FindSoldTickets(int n) => soldTickets
        .Where(s => s.FlightNumber == n).Select(s => s.TicketsSold).DefaultIfEmpty(-1).First();

    static void PrintOr(IEnumerable<string> lines, string empty)
    {
        var list = lines.ToList();
        if (!list.Any()) Console.WriteLine(empty); else list.ForEach(Console.WriteLine);
    }

    static void ShowAll()
    {
        Console.WriteLine("\n--- Самолёты ---");
        PrintOr(planes.Select(p => $"Тип: {p.Type} | Мест: {p.Seats} | Грузоподъёмность: {p.Capacity} т | Дальность: {p.Range} км"), "нет");

        Console.WriteLine("\n--- Рейсы ---");
        PrintOr(flights.Select(f => $"Рейс: {f.FlightNumber} | Тип: {f.PlaneType} | Вылет: {f.DepartureTime} | " +
            $"{f.DeparturePoint} -> {f.ArrivalPoint} | Билетов продано: " +
            (FindSoldTickets(f.FlightNumber) is int s && s == -1 ? "нет данных" : s.ToString())), "нет");

        Console.WriteLine("\n--- Расстояния ---");
        PrintOr(distances.Select(d => $"{d.CityA} - {d.CityB} : {d.Km} км"), "нет");
    }

    static void ShowReplacements()
    {
        if (!flights.Any()) { Console.WriteLine("Нет рейсов"); return; }
        var flight = flights.FirstOrDefault(f => f.FlightNumber == ReadInt("Введите номер рейса: "));
        if (flight == null) { Console.WriteLine("Рейс не найден"); return; }
        int dist = FindDistance(flight.DeparturePoint, flight.ArrivalPoint);
        if (dist == -1) { Console.WriteLine($"Расстояние между {flight.DeparturePoint} и {flight.ArrivalPoint} неизвестно"); return; }
        int sold = FindSoldTickets(flight.FlightNumber);
        if (sold == -1) { Console.WriteLine("Нет данных о проданных билетах на этот рейс. Замена невозможна"); return; }

        Console.WriteLine($"Рейс {flight.FlightNumber} ({flight.DeparturePoint} -> {flight.ArrivalPoint}): {dist} км, билетов продано: {sold}");
        Console.WriteLine("Возможные замены:");
        PrintOr(planes.Where(p => p.Type != flight.PlaneType && p.Range >= dist && p.Seats >= sold)
            .Select(p => $"  {p.Type} (мест: {p.Seats}, дальность: {p.Range} км)"), "Замены нет");
    }

    static void ShowFullLoadedFlights() => PrintOr(
        flights.Select(f => new { f, p = FindPlane(f.PlaneType), sold = FindSoldTickets(f.FlightNumber) })
            .Where(x => x.p != null && x.sold != -1 && x.sold == x.p.Seats)
            .Select(x => $"Рейс {x.f.FlightNumber} ({x.f.DeparturePoint} -> {x.f.ArrivalPoint}) | Тип: {x.p.Type} | Мест: {x.p.Seats} | Продано: {x.sold}"),
        "Нет рейсов с полной загрузкой");

    static void ShowAverageLoadByType()
    {
        if (!planes.Any()) { Console.WriteLine("Нет самолётов"); return; }
        planes.Select(p => new
        {
            p.Type,
            loads = flights.Where(f => f.PlaneType == p.Type)
                .Select(f => FindSoldTickets(f.FlightNumber))
                .Where(s => s != -1)
                .Select(s => (double)s / p.Seats * 100.0).ToList()
        })
        .Select(r => !r.loads.Any()
            ? $"Тип: {r.Type} - нет данных о рейсах"
            : $"Тип: {r.Type} - средняя загрузка: {r.loads.Average():F2} % (по {r.loads.Count} рейсам)")
        .ToList().ForEach(Console.WriteLine);
    }

    static void ShowDepartingFromPoint()
    {
        string point = Ask("Введите пункт вылета: ");
        var res = flights.Where(f => f.DeparturePoint == point).OrderBy(f => f.DepartureTime).ToList();
        if (!res.Any()) { Console.WriteLine("Рейсов из этого пункта нет"); return; }
        Console.WriteLine($"Рейсы из пункта {point} (отсортированы по времени):");
        res.Select(f => $"  Рейс {f.FlightNumber} | Вылет: {f.DepartureTime} | -> {f.ArrivalPoint} | Тип: {f.PlaneType}")
           .ToList().ForEach(Console.WriteLine);
    }

    static void ShowArrivingToPoint()
    {
        string point = Ask("Введите пункт прилёта: ");
        PrintOr(flights.Where(f => f.ArrivalPoint == point)
            .Select(f => $"  Рейс {f.FlightNumber} | Вылет: {f.DepartureTime} | Из: {f.DeparturePoint} | Тип: {f.PlaneType}"),
            "Рейсов в этот пункт нет");
    }

    static void ShowGroupedByHour()
    {
        if (!flights.Any()) { Console.WriteLine("Нет рейсов"); return; }
        foreach (var g in flights.GroupBy(f => f.DepartureTime.Hour).OrderBy(g => g.Key))
        {
            Console.WriteLine($"\nЧас вылета: {g.Key}:00");
            g.Select(f => $"  Рейс {f.FlightNumber} | {f.DepartureTime} | {f.DeparturePoint} -> {f.ArrivalPoint} | Тип: {f.PlaneType}")
             .ToList().ForEach(Console.WriteLine);
        }
    }
}
