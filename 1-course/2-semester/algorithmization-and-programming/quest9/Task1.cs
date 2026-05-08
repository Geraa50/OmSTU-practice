using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

class Specialization { public int Id; public string Name = ""; }
class Diagnosis { public int Id; public string Name = ""; }
class Doctor { public int Id; public string Fio = ""; public int SpecializationId; public string Category = ""; }
class Patient { public int Id; public string Fio = ""; public int BirthYear; }
class Visit { public DateTime Date; public int PatientId; public int DoctorId; public int DiagnosisId; }

class Program
{
    static List<Specialization> specs = new List<Specialization>();
    static List<Diagnosis> diagnoses = new List<Diagnosis>();
    static List<Doctor> doctors = new List<Doctor>();
    static List<Patient> patients = new List<Patient>();
    static List<Visit> visits = new List<Visit>();

    static int nextSpecId = 1, nextDiagId = 1, nextDocId = 1, nextPatId = 1;

    static void Main()
    {
        var menu = new (string Key, string Title, Action Action)[]
        {
            ("1", "Добавить специализацию", AddSpec),
            ("2", "Добавить диагноз", AddDiagnosis),
            ("3", "Добавить врача", AddDoctor),
            ("4", "Добавить пациента", AddPatient),
            ("5", "Добавить посещение", AddVisit),
            ("6", "Показать всё", ShowClinicAll),
            ("7", "А) Пациенты по врачу (по дате посещения)", ReportPatientsByDoctor),
            ("8", "Б) Пациенты по диагнозу", ReportPatientsByDiagnosis),
            ("9", "В) Пациенты по дате посещения", ReportPatientsByVisitDate),
            ("10", "Г) Врачи по специализации", ReportDoctorsBySpec),
        };

        while (true)
        {
            Console.WriteLine("\nполиклиника");
            menu.ToList().ForEach(m => Console.WriteLine(m.Key + " = " + m.Title));
            Console.WriteLine("0 = Выход");
            string c = Console.ReadLine() ?? "";
            if (c == "0") return;
            menu.FirstOrDefault(m => m.Key == c).Action?.Invoke();
        }
    }

    static void AddSpec()
    {
        string name = (Ask("Наименование специализации: ")).Trim();
        if (string.IsNullOrEmpty(name)) { Console.WriteLine("Пустое имя"); return; }
        if (specs.Any(s => s.Name == name)) { Console.WriteLine("Уже есть"); return; }
        int id = nextSpecId++;
        specs.Add(new Specialization { Id = id, Name = name });
        Console.WriteLine("Добавлено, id = " + id);
    }

    static void AddDiagnosis()
    {
        string name = (Ask("Наименование диагноза: ")).Trim();
        if (string.IsNullOrEmpty(name)) { Console.WriteLine("Пустое имя"); return; }
        if (diagnoses.Any(d => d.Name == name)) { Console.WriteLine("Уже есть"); return; }
        int id = nextDiagId++;
        diagnoses.Add(new Diagnosis { Id = id, Name = name });
        Console.WriteLine("Добавлено, id = " + id);
    }

    static void AddDoctor()
    {
        int sid = ReadInt("Id специализации: ");
        if (!specs.Any(s => s.Id == sid)) { Console.WriteLine("Специализация не найдена"); return; }
        string fio = Ask("ФИО врача: ");
        string cat = Ask("Категория (первая / вторая и т.д.): ");
        int id = nextDocId++;
        doctors.Add(new Doctor { Id = id, Fio = fio, SpecializationId = sid, Category = cat });
        Console.WriteLine("Врач добавлен, id = " + id);
    }

    static void AddPatient()
    {
        string fio = Ask("ФИО пациента: ");
        int year = ReadInt("Год рождения: ");
        int id = nextPatId++;
        patients.Add(new Patient { Id = id, Fio = fio, BirthYear = year });
        Console.WriteLine("Пациент добавлен, id = " + id);
    }

    static void AddVisit()
    {
        if (!patients.Any() || !doctors.Any() || !diagnoses.Any())
        { Console.WriteLine("Нужны пациенты, врачи и диагнозы"); return; }
        int pid = ReadInt("Id пациента: ");
        if (!patients.Any(p => p.Id == pid)) { Console.WriteLine("Пациент не найден"); return; }
        int did = ReadInt("Id врача: ");
        if (!doctors.Any(d => d.Id == did)) { Console.WriteLine("Врач не найден"); return; }
        int dg = ReadInt("Id диагноза: ");
        if (!diagnoses.Any(x => x.Id == dg)) { Console.WriteLine("Диагноз не найден"); return; }
        DateTime dt;
        while (true)
        {
            try
            {
                dt = DateTime.Parse(Ask("Дата посещения (дд.мм.гггг): "), CultureInfo.GetCultureInfo("ru-RU"));
                break;
            }
            catch { Console.WriteLine("Неверный формат даты"); }
        }
        visits.Add(new Visit { Date = dt.Date, PatientId = pid, DoctorId = did, DiagnosisId = dg });
        Console.WriteLine("Посещение записано");
    }

    static void ShowClinicAll()
    {
        Console.WriteLine("\n--- Специализации ---");
        PrintLines(specs.Select(s => $"{s.Id}: {s.Name}"), "нет");
        Console.WriteLine("\n--- Диагнозы ---");
        PrintLines(diagnoses.Select(d => $"{d.Id}: {d.Name}"), "нет");
        Console.WriteLine("\n--- Врачи ---");
        PrintLines(doctors.Select(d =>
        {
            string sn = specs.FirstOrDefault(x => x.Id == d.SpecializationId)?.Name ?? "?";
            return $"{d.Id}: {d.Fio} | {sn} | {d.Category}";
        }), "нет");
        Console.WriteLine("\n--- Пациенты ---");
        PrintLines(patients.Select(p => $"{p.Id}: {p.Fio}, {p.BirthYear} г.р."), "нет");
        Console.WriteLine("\n--- Посещения ---");
        PrintLines(visits.OrderBy(v => v.Date).Select(v =>
        {
            string pf = patients.FirstOrDefault(x => x.Id == v.PatientId)?.Fio ?? "?";
            string df = doctors.FirstOrDefault(x => x.Id == v.DoctorId)?.Fio ?? "?";
            string dg = diagnoses.FirstOrDefault(x => x.Id == v.DiagnosisId)?.Name ?? "?";
            return $"{v.Date:dd.MM.yyyy} | {pf} | {df} | {dg}";
        }), "нет");
    }

    static string SpecName(int id) => specs.FirstOrDefault(s => s.Id == id)?.Name ?? "?";
    static string DiagName(int id) => diagnoses.FirstOrDefault(d => d.Id == id)?.Name ?? "?";
    static string PatFio(int id) => patients.FirstOrDefault(p => p.Id == id)?.Fio ?? "?";
    static string DocFio(int id) => doctors.FirstOrDefault(d => d.Id == id)?.Fio ?? "?";

    static void ReportPatientsByDoctor()
    {
        if (!visits.Any()) { Console.WriteLine("Нет посещений"); return; }
        foreach (var g in visits.GroupBy(v => v.DoctorId).OrderBy(g => DocFio(g.Key)))
        {
            Console.WriteLine($"\nВрач: {DocFio(g.Key)} (id {g.Key})");
            foreach (var v in g.OrderBy(x => x.Date))
                Console.WriteLine($"  {v.Date:dd.MM.yyyy} | {PatFio(v.PatientId)} | диагноз: {DiagName(v.DiagnosisId)}");
        }
    }

    static void ReportPatientsByDiagnosis()
    {
        if (!visits.Any()) { Console.WriteLine("Нет посещений"); return; }
        foreach (var g in visits.GroupBy(v => v.DiagnosisId).OrderBy(g => DiagName(g.Key)))
        {
            Console.WriteLine($"\nДиагноз: {DiagName(g.Key)} (id {g.Key})");
            foreach (var v in g.OrderBy(x => x.Date))
                Console.WriteLine($"  {v.Date:dd.MM.yyyy} | {PatFio(v.PatientId)} | врач: {DocFio(v.DoctorId)}");
        }
    }

    static void ReportPatientsByVisitDate()
    {
        if (!visits.Any()) { Console.WriteLine("Нет посещений"); return; }
        foreach (var g in visits.GroupBy(v => v.Date).OrderBy(g => g.Key))
        {
            Console.WriteLine($"\nДата: {g.Key:dd.MM.yyyy}");
            foreach (var v in g.OrderBy(x => PatFio(x.PatientId)))
                Console.WriteLine($"  {PatFio(v.PatientId)} | {DocFio(v.DoctorId)} | {DiagName(v.DiagnosisId)}");
        }
    }

    static void ReportDoctorsBySpec()
    {
        if (!doctors.Any()) { Console.WriteLine("Нет врачей"); return; }
        foreach (var g in doctors.GroupBy(d => d.SpecializationId).OrderBy(g => SpecName(g.Key)))
        {
            Console.WriteLine($"\nСпециализация: {SpecName(g.Key)} (id {g.Key})");
            foreach (var d in g.OrderBy(x => x.Fio))
                Console.WriteLine($"  {d.Fio} | категория: {d.Category} (id врача {d.Id})");
        }
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
