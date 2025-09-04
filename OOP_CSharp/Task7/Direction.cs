namespace Task7;

public class Direction
{
    public string Title { get; }
    public int Passengers { get; private set; }
    private Train _train;

    public Direction(string title)
    {
        Title = title;
    }

    public void SellTickets()
    {
        Random random = new Random();
        Passengers = random.Next(200, 1500);
        Console.WriteLine($"Продано {Passengers} билетов на направление {Title}");
    }
    public void FormTrain()
    {
        _train = new Train();
        Random random = new Random();
        while (_train.FreeSeats < Passengers)
        {
            int capacity = random.Next(40, 100);
            _train.AddWagon(new Wagon(capacity));
        }

        int notSeatedPassengers = Passengers;
        foreach (Wagon trainWagon in _train.Wagons)
        {
            while (notSeatedPassengers >0 && trainWagon.FreeSeats > 0)
            {
                trainWagon.OccupiedSeats++;
                notSeatedPassengers--;
            }
        }
        
        Console.WriteLine($"Поезд сформирован: {_train.Wagons.Count} вагонов.");
    }

    public void SendTrain()
    {
        Console.WriteLine($"Поезд по направлению {Title} отправляется с {Passengers} пассажирами");
    }

}