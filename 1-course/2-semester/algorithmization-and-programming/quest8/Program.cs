using System;
using System.Collections.Generic;

class Plane
{
    public string Type;
    public int Seats;
    public double Capacity;
    public int Range;
}

class SoldTickets
{
    public int FlightNumber;
    public int TicketsSold;
}

class Flight
{
    public int FlightNumber;
    public string PlaneType;
    public DateTime DepartureTime;
    public string DeparturePoint;
    public string ArrivalPoint;
}

class Distance
{
    public string CityA;
    public string CityB;
    public int Km;
}

class Program
{
    static List<Plane> planes = new List<Plane>();
    static List<SoldTickets> soldTickets = new List<SoldTickets>();
    static List<Flight> flights = new List<Flight>();
    static List<Distance> distances = new List<Distance>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nаэропорт");
            Console.WriteLine("1 = Добавить самолёт (тип)");
            Console.WriteLine("2 = Добавить рейс");
            Console.WriteLine("3 = Добавить запись о продаже билетов");
            Console.WriteLine("4 = Добавить расстояние между городами");
            Console.WriteLine("5 = Показать всё");
            Console.WriteLine("6 = Какие типы самолётов могут заменить на выбранном рейсе");
            Console.WriteLine("7 = Рейсы с полной загрузкой самолётов");
            Console.WriteLine("8 = Средняя загрузка по каждому типу самолёта");
            Console.WriteLine("9 = Самолёты, вылетающие из заданного пункта (сортировка по времени)");
            Console.WriteLine("10 = Самолёты, прилетающие в заданный пункт");
            Console.WriteLine("11 = Самолёты, сгруппированные по часу вылета");
            Console.WriteLine("0 = Выход");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddPlane(); break;
                case "2": AddFlight(); break;
                case "3": AddSoldTickets(); break;
                case "4": AddDistance(); break;
                case "5": ShowAll(); break;
                case "6": ShowReplacements(); break;
                case "7": ShowFullLoadedFlights(); break;
                case "8": ShowAverageLoadByType(); break;
                case "9": ShowDepartingFromPoint(); break;
                case "10": ShowArrivingToPoint(); break;
                case "11": ShowGroupedByHour(); break;
                case "0": return;
            }
        }
    }

    static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            bool isValid = true;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] < '0' || input[i] > '9')
                {
                    isValid = false;
                    break;
                }
            }
            if (isValid && input.Length > 0)
            {
                return int.Parse(input);
            }
            Console.WriteLine("Нужно ввести только цифры");
        }
    }

    static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            try
            {
                return double.Parse(input.Replace('.', ','));
            }
            catch
            {
                Console.WriteLine("Неверный формат числа");
            }
        }
    }

    static void AddPlane()
    {
        Plane plane = new Plane();

        Console.Write("Введите тип самолёта: ");
        string type = Console.ReadLine();

        for (int i = 0; i < planes.Count; i++)
        {
            if (planes[i].Type == type)
            {
                Console.WriteLine("Самолёт с таким типом уже существует!");
                return;
            }
        }
        plane.Type = type;

        plane.Seats = ReadInt("Введите количество мест: ");
        plane.Capacity = ReadDouble("Введите грузоподъёмность (тонн): ");
        plane.Range = ReadInt("Введите дальность полёта (км): ");

        planes.Add(plane);
        Console.WriteLine("Ура, победа, самолёт добавлен!");
    }

    static void AddFlight()
    {
        Flight flight = new Flight();

        int number = ReadInt("Введите номер рейса: ");
        for (int i = 0; i < flights.Count; i++)
        {
            if (flights[i].FlightNumber == number)
            {
                Console.WriteLine("Рейс с таким номером уже существует!");
                return;
            }
        }
        flight.FlightNumber = number;

        Console.Write("Введите тип самолёта: ");
        string type = Console.ReadLine();
        bool typeExists = false;
        for (int i = 0; i < planes.Count; i++)
        {
            if (planes[i].Type == type)
            {
                typeExists = true;
                break;
            }
        }
        if (!typeExists)
        {
            Console.WriteLine("Такого типа самолёта нет в списке!");
            return;
        }
        flight.PlaneType = type;

        while (true)
        {
            Console.Write("Введите время вылета (дд.мм.гггг чч:мм): ");
            string input = Console.ReadLine();
            try
            {
                flight.DepartureTime = DateTime.Parse(input);
                break;
            }
            catch
            {
                Console.WriteLine("Неверный формат даты! Попробуйте ещё раз");
            }
        }

        Console.Write("Введите пункт вылета: ");
        flight.DeparturePoint = Console.ReadLine();

        Console.Write("Введите пункт прилёта: ");
        flight.ArrivalPoint = Console.ReadLine();

        flights.Add(flight);
        Console.WriteLine("Ура, победа, рейс добавлен!");
    }

    static void AddSoldTickets()
    {
        int number = ReadInt("Введите номер рейса: ");

        bool flightExists = false;
        for (int i = 0; i < flights.Count; i++)
        {
            if (flights[i].FlightNumber == number)
            {
                flightExists = true;
                break;
            }
        }
        if (!flightExists)
        {
            Console.WriteLine("Рейс не найден!");
            return;
        }

        for (int i = 0; i < soldTickets.Count; i++)
        {
            if (soldTickets[i].FlightNumber == number)
            {
                Console.WriteLine("Запись о билетах на этот рейс уже существует!");
                return;
            }
        }

        SoldTickets st = new SoldTickets();
        st.FlightNumber = number;
        st.TicketsSold = ReadInt("Введите количество проданных билетов: ");
        soldTickets.Add(st);
        Console.WriteLine("Запись добавлена");
    }

    static void AddDistance()
    {
        Distance d = new Distance();

        Console.Write("Введите первый город: ");
        d.CityA = Console.ReadLine();

        Console.Write("Введите второй город: ");
        d.CityB = Console.ReadLine();

        for (int i = 0; i < distances.Count; i++)
        {
            if ((distances[i].CityA == d.CityA && distances[i].CityB == d.CityB) ||
                (distances[i].CityA == d.CityB && distances[i].CityB == d.CityA))
            {
                Console.WriteLine("Такое расстояние уже есть!");
                return;
            }
        }

        d.Km = ReadInt("Введите расстояние (км): ");
        distances.Add(d);
        Console.WriteLine("Расстояние добавлено");
    }

    static int FindDistance(string cityA, string cityB)
    {
        for (int i = 0; i < distances.Count; i++)
        {
            if ((distances[i].CityA == cityA && distances[i].CityB == cityB) ||
                (distances[i].CityA == cityB && distances[i].CityB == cityA))
            {
                return distances[i].Km;
            }
        }
        return -1;
    }

    static Plane FindPlane(string type)
    {
        for (int i = 0; i < planes.Count; i++)
        {
            if (planes[i].Type == type)
            {
                return planes[i];
            }
        }
        return null;
    }

    static int FindSoldTickets(int flightNumber)
    {
        for (int i = 0; i < soldTickets.Count; i++)
        {
            if (soldTickets[i].FlightNumber == flightNumber)
            {
                return soldTickets[i].TicketsSold;
            }
        }
        return -1;
    }

    static void ShowAll()
    {
        Console.WriteLine("\n--- Самолёты ---");
        if (planes.Count == 0) Console.WriteLine("нет");
        for (int i = 0; i < planes.Count; i++)
        {
            Console.WriteLine("Тип: " + planes[i].Type +
                              " | Мест: " + planes[i].Seats +
                              " | Грузоподъёмность: " + planes[i].Capacity +
                              " т | Дальность: " + planes[i].Range + " км");
        }

        Console.WriteLine("\n--- Рейсы ---");
        if (flights.Count == 0) Console.WriteLine("нет");
        for (int i = 0; i < flights.Count; i++)
        {
            int sold = FindSoldTickets(flights[i].FlightNumber);
            Console.WriteLine("Рейс: " + flights[i].FlightNumber +
                              " | Тип: " + flights[i].PlaneType +
                              " | Вылет: " + flights[i].DepartureTime +
                              " | " + flights[i].DeparturePoint + " -> " + flights[i].ArrivalPoint +
                              " | Билетов продано: " + (sold == -1 ? "нет данных" : sold.ToString()));
        }

        Console.WriteLine("\n--- Расстояния ---");
        if (distances.Count == 0) Console.WriteLine("нет");
        for (int i = 0; i < distances.Count; i++)
        {
            Console.WriteLine(distances[i].CityA + " - " + distances[i].CityB + " : " + distances[i].Km + " км");
        }
    }

    static void ShowReplacements()
    {
        if (flights.Count == 0)
        {
            Console.WriteLine("Нет рейсов");
            return;
        }

        int number = ReadInt("Введите номер рейса: ");

        Flight flight = null;
        for (int i = 0; i < flights.Count; i++)
        {
            if (flights[i].FlightNumber == number)
            {
                flight = flights[i];
                break;
            }
        }
        if (flight == null)
        {
            Console.WriteLine("Рейс не найден");
            return;
        }

        int dist = FindDistance(flight.DeparturePoint, flight.ArrivalPoint);
        if (dist == -1)
        {
            Console.WriteLine("Расстояние между " + flight.DeparturePoint + " и " + flight.ArrivalPoint + " неизвестно");
            return;
        }

        int soldOnFlight = FindSoldTickets(flight.FlightNumber);
        if (soldOnFlight == -1)
        {
            Console.WriteLine("Нет данных о проданных билетах на этот рейс. Замена невозможна");
            return;
        }

        Console.WriteLine("Рейс " + flight.FlightNumber + " (" + flight.DeparturePoint + " -> " + flight.ArrivalPoint + "): " +
                          dist + " км, билетов продано: " + soldOnFlight);
        Console.WriteLine("Возможные замены:");

        bool found = false;
        for (int i = 0; i < planes.Count; i++)
        {
            if (planes[i].Type == flight.PlaneType) continue;
            if (planes[i].Range >= dist && planes[i].Seats >= soldOnFlight)
            {
                Console.WriteLine("  " + planes[i].Type +
                                  " (мест: " + planes[i].Seats +
                                  ", дальность: " + planes[i].Range + " км)");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Замены нет");
        }
    }

    static void ShowFullLoadedFlights()
    {
        bool found = false;
        for (int i = 0; i < flights.Count; i++)
        {
            Plane p = FindPlane(flights[i].PlaneType);
            int sold = FindSoldTickets(flights[i].FlightNumber);
            if (p != null && sold != -1 && sold == p.Seats)
            {
                Console.WriteLine("Рейс " + flights[i].FlightNumber +
                                  " (" + flights[i].DeparturePoint + " -> " + flights[i].ArrivalPoint + ")" +
                                  " | Тип: " + p.Type + " | Мест: " + p.Seats + " | Продано: " + sold);
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("Нет рейсов с полной загрузкой");
        }
    }

    static void ShowAverageLoadByType()
    {
        if (planes.Count == 0)
        {
            Console.WriteLine("Нет самолётов");
            return;
        }

        for (int i = 0; i < planes.Count; i++)
        {
            double total = 0;
            int count = 0;
            for (int j = 0; j < flights.Count; j++)
            {
                if (flights[j].PlaneType == planes[i].Type)
                {
                    int sold = FindSoldTickets(flights[j].FlightNumber);
                    if (sold != -1)
                    {
                        total += (double)sold / planes[i].Seats * 100.0;
                        count++;
                    }
                }
            }
            if (count == 0)
            {
                Console.WriteLine("Тип: " + planes[i].Type + " - нет данных о рейсах");
            }
            else
            {
                double avg = total / count;
                Console.WriteLine("Тип: " + planes[i].Type + " - средняя загрузка: " + avg.ToString("F2") + " % (по " + count + " рейсам)");
            }
        }
    }

    static void ShowDepartingFromPoint()
    {
        Console.Write("Введите пункт вылета: ");
        string point = Console.ReadLine();

        List<Flight> result = new List<Flight>();
        for (int i = 0; i < flights.Count; i++)
        {
            if (flights[i].DeparturePoint == point)
            {
                result.Add(flights[i]);
            }
        }

        if (result.Count == 0)
        {
            Console.WriteLine("Рейсов из этого пункта нет");
            return;
        }

        for (int i = 0; i < result.Count - 1; i++)
        {
            for (int j = 0; j < result.Count - 1 - i; j++)
            {
                if (result[j].DepartureTime > result[j + 1].DepartureTime)
                {
                    Flight tmp = result[j];
                    result[j] = result[j + 1];
                    result[j + 1] = tmp;
                }
            }
        }

        Console.WriteLine("Рейсы из пункта " + point + " (отсортированы по времени):");
        for (int i = 0; i < result.Count; i++)
        {
            Console.WriteLine("  Рейс " + result[i].FlightNumber +
                              " | Вылет: " + result[i].DepartureTime +
                              " | -> " + result[i].ArrivalPoint +
                              " | Тип: " + result[i].PlaneType);
        }
    }

    static void ShowArrivingToPoint()
    {
        Console.Write("Введите пункт прилёта: ");
        string point = Console.ReadLine();

        bool found = false;
        for (int i = 0; i < flights.Count; i++)
        {
            if (flights[i].ArrivalPoint == point)
            {
                Console.WriteLine("  Рейс " + flights[i].FlightNumber +
                                  " | Вылет: " + flights[i].DepartureTime +
                                  " | Из: " + flights[i].DeparturePoint +
                                  " | Тип: " + flights[i].PlaneType);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Рейсов в этот пункт нет");
        }
    }

    static void ShowGroupedByHour()
    {
        if (flights.Count == 0)
        {
            Console.WriteLine("Нет рейсов");
            return;
        }

        List<int> hours = new List<int>();
        for (int i = 0; i < flights.Count; i++)
        {
            int h = flights[i].DepartureTime.Hour;
            bool already = false;
            for (int j = 0; j < hours.Count; j++)
            {
                if (hours[j] == h)
                {
                    already = true;
                    break;
                }
            }
            if (!already) hours.Add(h);
        }

        for (int i = 0; i < hours.Count - 1; i++)
        {
            for (int j = 0; j < hours.Count - 1 - i; j++)
            {
                if (hours[j] > hours[j + 1])
                {
                    int tmp = hours[j];
                    hours[j] = hours[j + 1];
                    hours[j + 1] = tmp;
                }
            }
        }

        for (int i = 0; i < hours.Count; i++)
        {
            Console.WriteLine("\nЧас вылета: " + hours[i] + ":00");
            for (int j = 0; j < flights.Count; j++)
            {
                if (flights[j].DepartureTime.Hour == hours[i])
                {
                    Console.WriteLine("  Рейс " + flights[j].FlightNumber +
                                      " | " + flights[j].DepartureTime +
                                      " | " + flights[j].DeparturePoint + " -> " + flights[j].ArrivalPoint +
                                      " | Тип: " + flights[j].PlaneType);
                }
            }
        }
    }
}
