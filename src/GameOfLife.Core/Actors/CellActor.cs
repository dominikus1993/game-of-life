namespace GameOfLife.Core.Actors;
using Akka;
using Akka.Actor;

public class CellActor : ReceiveActor
{
    public CellActor()
    {
        Receive<CellActor.CellState>(state =>
        {
            Console.WriteLine($"CellActor: {state.X}, {state.Y}");
        });
    }

    public class CellState
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}