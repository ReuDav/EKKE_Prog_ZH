using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        // ===== 1. Feladat =====
        RAMSlots();

        // ===== 2. Feladat =====
        FishingContest();

        // ===== 3. Feladat =====
        JediTask();
    }

    static void RAMSlots()
    {
        Console.Write("Hány RAM slot van az alaplapon? (2-6): ");
        int slots = int.Parse(Console.ReadLine());
        while (slots < 2 || slots > 6)
        {
            Console.Write("Érvénytelen. Adjon meg egy számot 2 és 6 között: ");
            slots = int.Parse(Console.ReadLine());
        }

        int totalGB = 0;
        int filledSlots = 0;

        for (int i = 0; i < slots; i++)
        {
            Console.Write($"Slot {i + 1} RAM méret (2,4,6,8 GB): ");
            int ram = int.Parse(Console.ReadLine());
            while (ram != 2 && ram != 4 && ram != 6 && ram != 8)
            {
                Console.Write("Érvénytelen méret. Újra: ");
                ram = int.Parse(Console.ReadLine());
            }

            if (totalGB + ram <= 32)
            {
                totalGB += ram;
                filledSlots++;
            }
            else
            {
                Console.WriteLine("Túllépné a 32 GB-ot, ezért ezt a modult nem illesztjük be.");
                break;
            }
        }

        Console.WriteLine($"Összesen {filledSlots} slot töltve, {totalGB} GB RAM összesen.\n");
    }

    static void FishingContest()
    {
        Console.Write("Versenyzők száma (15-100): ");
        int n = int.Parse(Console.ReadLine());
        while (n < 15 || n > 100)
        {
            Console.Write("Érvénytelen. Adjon meg 15 és 100 közötti számot: ");
            n = int.Parse(Console.ReadLine());
        }

        int[] weights = new int[n];
        Random rnd = new Random();
        for (int i = 0; i < n; i++)
        {
            weights[i] = rnd.Next(1500, 25001);
        }

        // átlag számolása
        long sum = 0;
        for (int i = 0; i < n; i++)
            sum += weights[i];

        double average = (double)sum / n;
        Console.WriteLine($"A halak átlagos tömege: {average:F2} g");

        // legnagyobb hal
        int maxWeight = weights[0];
        int maxIndex = 0;
        for (int i = 1; i < n; i++)
        {
            if (weights[i] > maxWeight)
            {
                maxWeight = weights[i];
                maxIndex = i;
            }
        }

        double maxKg = ToKg(maxWeight);
        Console.WriteLine($"Legnagyobb hal: {maxKg:F2} kg, versenyző: {maxIndex + 1}");

        // 8kg alatti halak
        int totalSell = 0;
        for (int i = 0; i < n; i++)
        {
            if (ToKg(weights[i]) < 8.0)
                totalSell += weights[i];
        }
        double revenue = (double)totalSell / 1000 * 2350;
        Console.WriteLine($"Bevétel (8kg alatt eladott halak): {revenue:F2} Ft\n");
    }

    static double ToKg(int grams)
    {
        return grams / 1000.0;
    }

    static void JediTask()
    {
        string path = "jedik.csv";
        if (!File.Exists(path))
        {
            Console.WriteLine("Nincs jedik.csv fájl.");
            return;
        }

        List<Jedi> jediList = new List<Jedi>();
        using (StreamReader reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split(';');

                Jedi j = new Jedi();
                j.Name = parts[0];
                j.Midichlorians = int.Parse(parts[1]);
                j.SaberColor = parts[2];
                j.IsCouncilMember = parts[3].ToLower() == "tanácstag";

                jediList.Add(j);
            }
        }

        // 3.2 átlag midiklorián
        double total = 0;
        for (int i = 0; i < jediList.Count; i++)
            total += jediList[i].Midichlorians;
        double avg = total / jediList.Count;
        Console.WriteLine($"Jedik átlagos midikloriánszáma: {avg:F2}");

        // 3.4 kék kardosok
        List<Jedi> blueSabers = FilterBySaber(jediList, "kék");
        Console.WriteLine("\nKék kardos jedik:");
        foreach (var j in blueSabers)
            Console.WriteLine(j);

        // 3.5 kardszínek szerinti legkisebb midiklorián
        Dictionary<string, Jedi> lowestByColor = new Dictionary<string, Jedi>();
        foreach (var j in jediList)
        {
            if (!lowestByColor.ContainsKey(j.SaberColor))
                lowestByColor[j.SaberColor] = j;
            else if (j.Midichlorians < lowestByColor[j.SaberColor].Midichlorians)
                lowestByColor[j.SaberColor] = j;
        }

        Console.WriteLine("\nKardszínenként a legalacsonyabb midiklorián:");
        foreach (var color in lowestByColor.Keys)
        {
            Jedi jj = lowestByColor[color];
            Console.WriteLine($"{color}: {jj.Name}, tanácstag? {jj.IsCouncilMember}");
        }
    }

    static List<Jedi> FilterBySaber(List<Jedi> list, string color)
    {
        List<Jedi> result = new List<Jedi>();
        foreach (var j in list)
        {
            if (j.SaberColor == color)
                result.Add(j);
        }
        return result;
    }
}

class Jedi
{
    public string Name;
    public int Midichlorians;
    public string SaberColor;
    public bool IsCouncilMember;

    public override string ToString()
    {
        return $"{Name}, {Midichlorians} midikl., szín: {SaberColor}, tanácstag: {IsCouncilMember}";
    }
}
