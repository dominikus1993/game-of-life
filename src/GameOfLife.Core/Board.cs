using GameOfLife.Core.Cells;

namespace GameOfLife.Core;


public sealed class Board
{
    private List<Cell> Cells { get; }

    private Board(List<Cell> cells)
    {
        Cells = cells;
    }
    
    public static Board InitializeState()
    {
        return new Board([]);
    }
}