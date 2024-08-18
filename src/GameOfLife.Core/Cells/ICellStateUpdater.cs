namespace GameOfLife.Core.Cells;

public interface ICellStateUpdater
{
    CellState UpdateState(CellState state, IReadOnlyList<Cell> neighbors);
}