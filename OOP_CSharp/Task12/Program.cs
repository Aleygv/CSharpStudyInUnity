using Task12;

using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var aviary1 = new Aviary(new List<Animal>
        {
            new Animal(AnimalType.Wolf, "Мужской", "Аууу"),
            new Animal(AnimalType.Wolf, "Женский", "Аууу")
        });

        var aviary2 = new Aviary(new List<Animal>
        {
            new Animal(AnimalType.Goose, "Мужской", "Га-га-га"),
            new Animal(AnimalType.Goose, "Мужской", "Га-га-га"),
            new Animal(AnimalType.Goose, "Женский", "Га-га-га")
        });

        var aviary3 = new Aviary(new List<Animal>
        {
            new Animal(AnimalType.Tiger, "Мужской", "Ррррр"),
            new Animal(AnimalType.Tiger, "Женский", "Ррррр")
        });

        var aviary4 = new Aviary(new List<Animal>
        {
            new Animal(AnimalType.Clown, "Неизвестен", "Ха-ха-ха"),
            new Animal(AnimalType.Clown, "Мужской", "Зачёт!")
        });

        Aviary[] aviaries = { aviary1, aviary2, aviary3, aviary4 };

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ЗООПАРК ===\n");
            Console.WriteLine("[1] Выбрать вольер для просмотра");
            Console.WriteLine("[2] Выход\n");

            Console.Write("Выберите опцию: ");
            string input = Console.ReadLine();

            if (input == "2")
            {
                Console.WriteLine("До свидания!");
                break;
            }
            else if (input == "1")
            {
                ShowAviariesMenu(aviaries);
            }
            else
            {
                Console.WriteLine("\nНеверный выбор. Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }

    private static void ShowAviariesMenu(Aviary[] aviaries)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ВЫБОР ВОЛЬЕРА ===\n");

            for (int i = 0; i < aviaries.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] Подойти к вольеру с {aviaries[i].DefineAnimalType()}");
            }

            Console.WriteLine("[0] Назад\n");

            Console.Write("Выберите вольер: ");
            string input = Console.ReadLine();

            if (input == "0")
            {
                return;
            }
            else if (int.TryParse(input, out int choice) && choice >= 1 && choice <= aviaries.Length)
            {
                Console.Clear();
                Console.WriteLine($"=== ВОЛЬЕР {choice} ===\n");
                aviaries[choice - 1].ShowAviaryInfo();

                Console.WriteLine("\nНажмите любую клавишу для возврата...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nНеверный выбор. Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }
}