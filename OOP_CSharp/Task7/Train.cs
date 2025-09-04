namespace Task7;

public class Train
{
    public List<Wagon> Wagons = new List<Wagon>();
    public int AllSeats => Wagons.Sum(p => AllSeats);
    public int OccupiedSeats => Wagons.Sum(p => p.OccupiedSeats);
    public int FreeSeats => Wagons.Sum(p => p.FreeSeats);

    public void AddWagon(Wagon wagon)
    {
        Wagons.Add(wagon);
    }
}