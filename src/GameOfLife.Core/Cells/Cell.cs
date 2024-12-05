using Akka.Actor;

namespace GameOfLife.Core.Cells;

public sealed class Cell
{
    public Location Location { get; }

    private IReadOnlyList<Cell> Neighbors { get; set; } = [];
    
    public CellState State { get; private set; }

    private Cell(Location location, CellState state)
    {
        Location = location;
        State = state;
    }
    
    public void AddNeighbors(IReadOnlyList<Cell> neighbors)
    {
        Neighbors = neighbors;
    }
    
    public static Cell Create(Location location, CellState state)
    {
        return new Cell(location, state);
    }
    
    public void UpdateState(ICellStateUpdater cellStateUpdater)
    {
        State = cellStateUpdater.UpdateState(State, Neighbors);
    }
}

public sealed class AddNeighboursMessage
{
    public IReadOnlyList<IActorRef> Neighbors { get; }

    public AddNeighboursMessage(IReadOnlyList<IActorRef> neighbors)
    {
        Neighbors = neighbors;
    }
}

public sealed class CellActor : ReceiveActor
{
    private uint _generation = 0;
    private Cell _state;
    private IReadOnlyList<IActorRef> _neighbors = [];
    
}