using System.Threading.Channels;

namespace _2023_24
{
    internal class Program
    {
        //Egyéni árak
        //Felnőtt belépő kedvezmények nélkül 3800 Ft
        //Kedvezményes jegyek
        //Esti belépő(du. 4 óra után) 1800 Ft
        //Eger városkártya 1500 Ft
        //Diák/nyugdíjas 1000 Ft
        //Családi árak
        //2 felnőtt + 1 gyermek 9700 Ft
        //2 felnőtt + 2 gyermek 12100 Ft
        //Minden további gyermek esetén fizetendő kedvezményes ár 2400 Ft
        static void Main(string[] args)
        {
            //EgriStrand();
            // Bonbonok();
            // KinaiKampusz();

        }
        static void EgriStrand()
        {
            string jegyTípus = "";
            string egyeniKedvezmeny = "";
            string kedvezmenyJellege = "";
            int ora = 0;
            int ar = 0;
            int gyerekekSzama = 0;
            while (!jegyTípus.Equals("egyéni") && !jegyTípus.Equals("családi"))
            {
                Console.Write("Add meg a jegy típusát (egyéni/családi): ");
                jegyTípus = Console.ReadLine();

                if (jegyTípus.Equals("egyéni"))
                {
                    while (!egyeniKedvezmeny.Equals("igen") && !egyeniKedvezmeny.Equals("nem"))
                    {
                        Console.Write("Van bármilyen kedvezménye? (igen/nem):");
                        egyeniKedvezmeny = Console.ReadLine();

                        if (egyeniKedvezmeny.Equals("igen"))
                        {

                            while (!kedvezmenyJellege.Equals("városkártya") && !kedvezmenyJellege.Equals("diák") && !kedvezmenyJellege.Equals("nyugdíjas"))
                            {
                                Console.Write("Kedvezmény jellege? (diák / nyugdíjas / városkártya):");
                                kedvezmenyJellege = Console.ReadLine();
                            }
                            switch (kedvezmenyJellege)
                            {
                                case "városkártya":
                                    ar = 1500;
                                    break;
                                case "diák":
                                    ar = 1000;
                                    break;
                                case "nyugdíjas":
                                    ar = 1000;
                                    break;
                                default:
                                    ar = 3800;
                                    break;
                            }
                            Console.WriteLine("A belépő ár {0} kedvezménnyel {1} Ft lesz", kedvezmenyJellege, ar);
                        }
                        if (egyeniKedvezmeny.Equals("nem"))
                        {
                            kedvezmenyJellege = "nincs";
                            while (ora < 9 || ora >= 19)
                            {
                                Console.Write("Kérlek add meg, hány órakor mész a strandra (9-19): ");
                                ora = int.Parse(Console.ReadLine());
                            }
                            if (ora > 16 && ora < 20)
                            {
                                ar = 1800;
                                Console.WriteLine("16 óra után {0} Ft lesz a jegy", ar);
                            }
                            else
                            {
                                ar = 3800;
                                Console.WriteLine("16 óra előtt {0} Ft lesz a jegy", ar);
                            }

                        }
                    }
                }
                if (jegyTípus.Equals("családi"))
                {
                    while (gyerekekSzama < 1 || gyerekekSzama >= 69)
                    {
                        Console.Write("Hány gyerek jön a családdal: ");
                        gyerekekSzama = int.Parse(Console.ReadLine());
                        if (gyerekekSzama.Equals(1))
                        {
                            ar = 9700;
                            Console.WriteLine("1 gyerekkel a családi belépő {0} Ft lesz", ar);
                        }
                        else if (gyerekekSzama.Equals(2))
                        {
                            ar = 12100;
                            Console.WriteLine("2 gyerekkel a családi belépő {0} Ft lesz", ar);
                        }
                        else
                        {
                            ar = 12100 + (gyerekekSzama * 2400);
                            Console.WriteLine("{1} gyerekkel a családi belépő {0} Ft lesz", ar, gyerekekSzama);
                        }
                    }
                }
            }
            Console.WriteLine($"{jegyTípus} - {kedvezmenyJellege} - {egyeniKedvezmeny} - {ar}");

        }
        static void Bonbonok()
        {
            string[] bonbonFajtak = [
                "ét", "tej", "fehér", //150Ft
                "mogyorós", "diós",   // 200Ft
                "szilvás", "pisztáciás", "nugátos", "ananászos"]; // 300Ft
            string iz = "";
            int osszegAr = 0;
            int osszegGramm = 0;
            while (iz != "végeztem")
            {
                Console.Write("Adj meg egy ízt: ");
                iz = Console.ReadLine();
                switch (iz)
                {
                    case "ét":
                        osszegAr += 150;
                        osszegGramm += 10;
                        break;
                    case "tej":
                        osszegAr += 150;
                        osszegGramm += 10;
                        break;
                    case "fehér":
                        osszegAr += 150;
                        osszegGramm += 10;
                        break;
                    case "mogyorós":
                        osszegAr += 200;
                        osszegGramm += 20;
                        break;
                    case "diós":
                        osszegAr += 200;
                        osszegGramm += 20;
                        break;
                    case "szilvás":
                        osszegAr += 300;
                        osszegGramm += 30;
                        break;
                    case "pisztáciás":
                        osszegAr += 300;
                        osszegGramm += 30;
                        break;
                    case "nugátos":
                        osszegAr += 300;
                        osszegGramm += 30;
                        break;
                    case "ananászos":
                        osszegAr += 300;
                        osszegGramm += 30;
                        break;
                    case "":
                        Console.WriteLine("Hiba! Üres mezőt nem fogad el a rendszer!");
                        break;
                    case "végeztem":
                        break;
                    default:
                        Console.WriteLine("Hiba! Ilyen bonbon nem kapható!");
                        break;
                }
            }
            double dobozokSzama = Math.Ceiling(osszegGramm / 120.0);
            double teljesAr = osszegAr + (dobozokSzama * 100.0);
            Console.WriteLine($"{dobozokSzama} megkezdett dobozok száma, {osszegGramm} g, és az ára {teljesAr} Ft");
        }
        //static void KinaiKampusz()
        //{
        //    string bemenet;
        //    int emeletSzam = 0;
        //    do
        //    {
        //        Console.Write("Add meg az emeletek számát! (10-25): ");
        //        bemenet = Console.ReadLine();
        //    }
        //    while ((emeletSzam < 10 || emeletSzam > 25) && !int.TryParse(bemenet, out emeletSzam));
        //}
    }
}
