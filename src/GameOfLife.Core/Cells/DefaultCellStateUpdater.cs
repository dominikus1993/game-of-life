namespace GameOfLife.Core.Cells;

public sealed class DefaultCellStateUpdater : ICellStateUpdater
{
    public static readonly DefaultCellStateUpdater Instance = new DefaultCellStateUpdater();
    
    private DefaultCellStateUpdater()
    {
    }

    public CellState UpdateState(CellState state, IReadOnlyList<Cell> neighbors)
    {
        var aliveNeighbors = neighbors.Count(static cell => cell.State == CellState.Alive);
        return state switch
        {
            CellState.AliveState when aliveNeighbors < 2 => CellState.Dead,
            CellState.AliveState when aliveNeighbors > 3 => CellState.Dead,
            CellState.DeadState when aliveNeighbors == 3 => CellState.Alive,
            _ => state
        };
    }
}