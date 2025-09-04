namespace Task7;

public class Wagon
{
    public int AllSeats { get; }
    public int OccupiedSeats { get; set; }
    public int FreeSeats => AllSeats - OccupiedSeats;

    public Wagon(int allSeats)
    {
        AllSeats = allSeats;
        OccupiedSeats = 0;
    }
}