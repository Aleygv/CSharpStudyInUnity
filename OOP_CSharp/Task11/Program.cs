using System;
using System.Collections.Generic;
using System.Linq;
using Task11;

class Program
{
    static void Main()
    {
        Random random = new Random();

        var platoon1 = new Platoon(new List<Solider>
        {
            new Racist(100, 25, 5, random),     
            new Berserk(120, 20, 3, random),    
            new Squaddie(80, 15, 4),    
            new Strongman(90, 30, 8)    
        });

        var platoon2 = new Platoon(new List<Solider>
        {
            new Berserk(110, 18, 2, random),    
            new Racist(95, 22, 6, random),      
            new Squaddie(75, 12, 2),    
            new Squaddie(85, 14, 3),    
            new Strongman(100, 28, 7)   
        });

        Console.WriteLine("БОЙ НАЧАЛСЯ!");
        Console.WriteLine($"Взвод 1: {platoon1.CalculateAliveSoldiers()} бойцов");
        Console.WriteLine($"Взвод 2: {platoon2.CalculateAliveSoldiers()} бойцов\n");

        int round = 0;

        while (platoon1.IsPlatoonAlive() && platoon2.IsPlatoonAlive())
        {
            round++;
            Console.WriteLine($"--- РАУНД {round} ---");

            Console.WriteLine("Взвод 1 атакует Взвод 2...");
            platoon1.AttackEnemyPlatoon(platoon2);

            Console.WriteLine("Взвод 2 атакует Взвод 1...");
            platoon2.AttackEnemyPlatoon(platoon1);

            RemoveDeadSoldiers(platoon1);
            RemoveDeadSoldiers(platoon2);

            Console.WriteLine($"После раунда {round}:");
            Console.WriteLine($"Взвод 1: {platoon1.CalculateAliveSoldiers()} живых");
            Console.WriteLine($"Взвод 2: {platoon2.CalculateAliveSoldiers()} живых\n");
        }

        if (platoon1.IsPlatoonAlive())
        {
            Console.WriteLine("ПОБЕДИЛ Взвод 1!");
        }
        else if (platoon2.IsPlatoonAlive())
        {
            Console.WriteLine("ПОБЕДИЛ Взвод 2!");
        }
        else
        {
            Console.WriteLine("НИЧЬЯ — ОБА Взвода УНИЧТОЖЕНЫ!");
        }
    }

    private static void RemoveDeadSoldiers(Platoon platoon)
    {
        platoon.Soldiers.RemoveAll(s => !s.IsAlive());
    }
}