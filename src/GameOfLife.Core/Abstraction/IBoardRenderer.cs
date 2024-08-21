using GameOfLife.Core.Cells;

namespace GameOfLife.Core.Abstraction;

public interface IBoardRenderer
{
    ValueTask RenderBoard(IReadOnlyList<Cell> cells, int generation);
}

public sealed class ConsoleBoardRenderer : IBoardRenderer
{
    public ValueTask RenderBoard(IReadOnlyList<Cell> cells, int generation)
    {
        Console.Clear();
        Console.WriteLine($"Generation: {generation}");
        for (var i = 0; i < cells.Count; i++)
        {
            if (i % 100 == 0)
            {
                Console.WriteLine();
            }
            Console.Write(cells[i].State == CellState.Alive ? "X" : " ");
        }
        Console.WriteLine();
        return ValueTask.CompletedTask;
    }
}