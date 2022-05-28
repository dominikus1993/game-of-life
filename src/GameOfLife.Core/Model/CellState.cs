namespace GameOfLife.Core.Model;

public class Cell
{
    internal enum CellState : byte
    {
        Alive,
        Dead
    }

    internal readonly CellState _state;

    public bool IsAlive => _state == CellState.Alive;

    
}