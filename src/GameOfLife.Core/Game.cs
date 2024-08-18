using GameOfLife.Core.Cells;

namespace GameOfLife.Core;

public sealed class Game
{
    private readonly int _width;
    private readonly int _height;
    private readonly ICellFactory _factory;
    private readonly ICellStateUpdater _updater;

    public Game(int width, int height, ICellFactory factory, ICellStateUpdater updater)
    {
        _width = width;
        _height = height;
        _factory = factory;
        _updater = updater;
    }
    
    public ValueTask Run(CancellationToken cancellationToken)
    {
        var board = Initialize();
        while (!cancellationToken.IsCancellationRequested)
        {
            Input(board);
            Update(board);
            Render(board);
        }
        
        return ValueTask.CompletedTask;
    }

    private void Input(Board board)
    {
    }
    
    private void Update(Board board)
    {
        board.NextGeneration(_updater);
    }
    
    private void Render(Board board)
    {
        
    }
    
    private Board Initialize()
    {
        return Board.InitializeState(_width, _height, _factory);
    }
}