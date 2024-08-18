namespace GameOfLife.Core.Cells;

public sealed class Cell
{
    public Location Location { get; }
    
    private Cell[] Neighbors { get; }
    
    public CellState State { get; private set; }

    private Cell(Location location, CellState state, IEnumerable<Cell> cells)
    {
        Location = location;
        Neighbors = [..cells.Where(cell => cell.Location != location)];
        State = state;
    }
    
    public static Cell Create(Location location, CellState state, IEnumerable<Cell> cells)
    {
        return new Cell(location, state, cells);
    }
    
    public void UpdateState()
    {
        var aliveNeighbors = Neighbors.Count(cell => cell.State == CellState.Alive);
        State = State switch
        {
            CellState.AliveState when aliveNeighbors < 2 => CellState.Dead,
            CellState.AliveState when aliveNeighbors > 3 => CellState.Dead,
            CellState.DeadState when aliveNeighbors == 3 => CellState.Alive,
            _ => State
        };
    }
}