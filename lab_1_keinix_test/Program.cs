using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using RecordLib;

class Program
{
    static List<Record> records = new List<Record>();
    const string FilePath = "import.csv";

    static void Main()
    {
        LoadData();
        while (true)
        {
            Console.Clear();
            ShowMenu();
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Escape)
                break;

            switch (key)
            {
                case ConsoleKey.D1:
                    ShowAllRecords();
                    break;
                case ConsoleKey.D2:
                    ShowRecordByNumber();
                    break;
                case ConsoleKey.D3:
                    AddRecord();
                    break;
                case ConsoleKey.D4:
                    DeleteRecord();
                    break;
                case ConsoleKey.D5:
                    SaveData();
                    break;
            }
        }
    }

    public static string[] actions = {
        "Вывести все записи",
        "Вывести запись по номеру",
        "Добавить запись",
        "Удалить запись",
        "Сохранить данные в файл"
    };

    static void ShowMenu()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {actions[i]}");
        }
        Console.WriteLine("Нажмите Esc для выхода");
    }

    static void LoadData()
    {
        if (File.Exists(FilePath))
        {
            var lines = File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                var record = new Record
                {
                    TextField1 = parts[0],
                    TextField2 = parts[1],
                    NumericField = double.Parse(parts[2]),
                    BooleanField = bool.Parse(parts[3])
                };
                records.Add(record);
            }
        }
    }

    static void ShowAllRecords()
    {
        foreach (var record in records)
        {
            Console.WriteLine(record);
        }
        Console.WriteLine("Нажмите любую клавишу...");
        Console.ReadKey();
    }

    static void ShowRecordByNumber()
    {
        Console.Write("Введите номер записи: ");
        int index;
        string input = Console.ReadLine()!;

        // Попытка парсинга строки в целое число
        bool isParsed = int.TryParse(input, out index);

        // Проверяем, успешно ли парсился ввод
        if (!isParsed)
        {
            Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
        }
        else if (index < 1)
        {
            Console.WriteLine("Номер записи не может быть отрицательным или нулем.");
        }
        else if (index >= records.Count + 1)
        {
            Console.WriteLine("Номер записи превышает количество доступных записей.");
        }
        else
        {
            Console.WriteLine(records[index - 1]); // Здесь выводим запись по индексу
        }

        Console.WriteLine("Нажмите любую клавишу...");
        Console.ReadKey();
    }



    static void AddRecord()
    {
        var record = new Record();
        Console.Write("Введите первый текстовое поле: ");
        record.TextField1 = Console.ReadLine();
        Console.Write("Введите второй текстовое поле: ");
        record.TextField2 = Console.ReadLine();
        Console.Write("Введите числовое поле: ");
        double numeric;
        while (!double.TryParse(Console.ReadLine(), out numeric))
        {
            Console.Write("Некорректный ввод. Повторите ввод числового поля: ");
        }
        record.NumericField = numeric;
        Console.Write("Введите логическое поле (true/false): ");
        bool boolean;
        while (!bool.TryParse(Console.ReadLine(), out boolean))
        {
            Console.Write("Некорректный ввод. Повторите ввод логического поля: ");
        }
        record.BooleanField = boolean;
        records.Add(record);
    }

    static void DeleteRecord()
    {
        Console.Write("Введите номер записи для удаления: ");
        int index;
        if (int.TryParse(Console.ReadLine(), out index) && index >= 0 && index < records.Count)
        {
            records.RemoveAt(index);
            Console.WriteLine("Запись удалена.");
        }
        else
        {
            Console.WriteLine("Некорректный номер записи.");
        }
        Console.WriteLine("Нажмите любую клавишу...");
        Console.ReadKey();
    }

    static void SaveData()
    {
        File.WriteAllLines(FilePath, records.Select(r => r.ToString()));
        Console.WriteLine("Данные сохранены.");
        Console.ReadKey();
    }
}