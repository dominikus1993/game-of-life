using System.Diagnostics.CodeAnalysis;
using GameOfLife.Core.Abstraction;
using GameOfLife.Core.Cells;

namespace GameOfLife.Core;


public sealed class Board
{
    private IReadOnlyList<Cell> Cells { get; }
    private int Generation { get; set; }

    private Board(IReadOnlyList<Cell> cells)
    {
        Cells = cells;
        Generation = 0;
    }
    
    public static Board InitializeState(int width, int height, ICellFactory factory)
    {
        var cells = factory.CreateCells(width, height);
        return new Board(cells);
    }


    public void NextGeneration(ICellStateUpdater updater)
    {
        foreach (var cell in Cells)
        {
            cell.UpdateState(updater);
        }
        Generation += 1;
    }
    
    public ValueTask Render(IBoardRenderer renderer)
    {
        return renderer.RenderBoard(Cells, Generation);
    }
}