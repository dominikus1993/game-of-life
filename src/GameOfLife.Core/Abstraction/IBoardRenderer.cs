using GameOfLife.Core.Cells;

namespace GameOfLife.Core.Abstraction;

public interface IBoardRenderer : IObserver<BoardState>
{
    ValueTask RenderBoard();
}

public sealed class ConsoleBoardRenderer : IBoardRenderer
{
    private BoardState _state = BoardState.Empty;
    
    public ValueTask RenderBoard()
    {
        Console.Clear();
        Console.WriteLine($"Generation: {_state.Generation}");
        for (var i = 0; i < _state.Cells.Count; i++)
        {
            if (i % _state.Size.Width == 0)
            {
                Console.WriteLine();
            }
            Console.Write(_state.Cells[i].State == CellState.Alive ? "X" : " ");
        }
        Console.WriteLine();
        return ValueTask.CompletedTask;
    }

    public void OnCompleted()
    {
        Console.WriteLine("Game Over");
    }

    public void OnError(Exception error)
    {
        Console.WriteLine("Error");
    }

    public void OnNext(BoardState value)
    {
        _state = value;
    }
}