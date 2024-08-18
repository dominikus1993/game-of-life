using System.Diagnostics.CodeAnalysis;
using GameOfLife.Core.Cells;

namespace GameOfLife.Core;


public sealed class Board
{
    private IReadOnlyList<Cell> Cells { get; }

    private Board(IReadOnlyList<Cell> cells)
    {
        Cells = cells;
    }
    
    public Board InitializeState(int width, int height, ICellFactory factory)
    {
        var cells = factory.CreateCells(width, height);
        return new Board(cells);
    }
}