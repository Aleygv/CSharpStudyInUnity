using System;
using Task9;

public class Programm
{
    public static void Main(string[] args)
    {
        bool isWorking = true;
        Random random = new Random();

        while (isWorking)
        {
            // Главное меню
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║         ДОБРО ПОЖАЛОВАТЬ В КОЛИЗЕЙ!      ║");
            Console.WriteLine("║     Здесь сражаются только самые смелые! ║");
            Console.WriteLine("╚══════════════════════════════════════════╝\n");

            Console.WriteLine("[1] Посмотреть бой");
            Console.WriteLine("[2] Выход\n");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Warrior warrior1 = SelectWarrior("первого", random);
                if (warrior1 == null) continue; // если пользователь вернулся назад

                Warrior warrior2 = SelectWarrior("второго", random);
                if (warrior2 == null) continue;

                // Подтверждение выбора
                Console.Clear();
                Console.WriteLine("Выбраны бойцы:\n");
                warrior1.ShowStats();
                Console.WriteLine();
                warrior2.ShowStats();
                Console.WriteLine("\n[Enter] Начать бой...");
                Console.ReadKey();
                Console.Clear();

                // Начинаем бой!
                Battle(warrior1, warrior2, random);

                Console.WriteLine("\n[Enter] Вернуться в меню...");
                Console.ReadKey();
            }
            else if (choice == "2")
            {
                Console.WriteLine("\nСпасибо за игру! До встречи в Колизее!");
                isWorking = false;
            }
            else
            {
                Console.WriteLine("\nНеверный выбор. Нажмите Enter для повтора...");
                Console.ReadKey();
            }
        }
    }

    // Метод выбора бойца
    private static Warrior? SelectWarrior(string order, Random random)
    {
        while (true)
        {
            // Не очищаем здесь! Мы уже в цикле выбора, и меню должно быть "плавным"
            Console.WriteLine($"Выберете {order} бойца:\n");
            Console.WriteLine("[1] Счастливчик      — шанс на двойной урон");
            Console.WriteLine("[2] Прогнозист        — каждый 3-й удар двойной урон");
            Console.WriteLine("[3] Вампир            — восстанавливает здоровье после удара");
            Console.WriteLine("[4] Маг               — огненный шар (мана), повышенный урон");
            Console.WriteLine("[5] Трюкач            — шанс избежать атаки");
            Console.WriteLine("[0] Назад\n");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var lucky = new LuckyGuy("Счастливчик", 100, 25, 10, random);
                    if (ConfirmSelection(lucky)) return lucky;
                    break;
                case "2":
                    var foreseer = new Forecaster("Прогнозист", 90, 20, 15);
                    if (ConfirmSelection(foreseer)) return foreseer;
                    break;
                case "3":
                    var vampire = new Vampire("Вампир", 120, 18, 12, random);
                    if (ConfirmSelection(vampire)) return vampire;
                    break;
                case "4":
                    var mage = new Wizard("Маг", 80, 30, 8, 20);
                    if (ConfirmSelection(mage)) return mage;
                    break;
                case "5":
                    var trickster = new Trickster("Трюкач", 95, 22, 5, random);
                    if (ConfirmSelection(trickster)) return trickster;
                    break;
                case "0":
                    return null; // назад
                default:
                    Console.WriteLine("\nНеверный выбор. Нажмите Enter для повтора...");
                    Console.ReadKey();
                    continue;
            }
        }
    }

    // Подтверждение выбора бойца
    private static bool ConfirmSelection(Warrior warrior)
    {
        // Не очищаем! Мы уже видим список бойцов — просто показываем его ещё раз
        Console.WriteLine(); // Пустая строка для красоты
        warrior.ShowStats();
        Console.WriteLine("\n[1] Подтвердить выбор");
        Console.WriteLine("[2] Назад\n");

        string choice = Console.ReadLine();
        return choice == "1";
    }

    // Логика боя
    private static void Battle(Warrior attacker, Warrior defender, Random random)
    {
        int round = 1;
        Console.WriteLine("⚔️   БОЙ НАЧАЛСЯ! ⚔️\n");

        while (attacker.IsAlive() && defender.IsAlive())
        {
            Console.WriteLine($"--- Раунд {round} ---");

            // Атака первого бойца
            attacker.DealDamage(defender);
            if (!defender.IsAlive()) break;

            // Атака второго бойца
            defender.DealDamage(attacker);
            if (!attacker.IsAlive()) break;

            Console.WriteLine();
            round++;
        }

        // Вывод результата
        Console.WriteLine("=== БОЙ ЗАВЕРШЕН ===\n");

        if (attacker.IsAlive() && !defender.IsAlive())
            Console.WriteLine($"🏆 ПОБЕДИТЕЛЬ: {attacker}"); // Уже toString() возвращает имя!
        else if (!attacker.IsAlive() && defender.IsAlive())
            Console.WriteLine($"🏆 ПОБЕДИТЕЛЬ: {defender}");
        else
            Console.WriteLine("🤝 НИЧЬЯ! Оба бойца повержены...");

        // Ограничиваем здоровье нулём при выводе — так естественнее
        int finalHealth1 = Math.Max(0, attacker.Health);
        int finalHealth2 = Math.Max(0, defender.Health);

        Console.WriteLine($"\n{attacker} | Здоровье: {finalHealth1}");
        Console.WriteLine($"{defender} | Здоровье: {finalHealth2}");
    }
}