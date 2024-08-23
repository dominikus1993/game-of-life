using GameOfLife.Core.Abstraction;
using GameOfLife.Core.Cells;

namespace GameOfLife.Core;

public sealed class Game : IDisposable
{
    private readonly int _width;
    private readonly int _height;
    private readonly ICellFactory _factory;
    private readonly ICellStateUpdater _updater;
    private readonly IBoardRenderer _boardRenderer;
    private IDisposable _subscription;

    public Game(int width, int height, ICellFactory factory, ICellStateUpdater updater, IBoardRenderer boardRenderer)
    {
        _width = width;
        _height = height;
        _factory = factory;
        _updater = updater;
        _boardRenderer = boardRenderer;
    }
    
    public async ValueTask Run(CancellationToken cancellationToken)
    {
        var board = Initialize();
        while (!cancellationToken.IsCancellationRequested)
        {
            await Input(board);
            await Update(board);
            await Render(board);
        }
    }

    private ValueTask Input(Board board)
    {
        return ValueTask.CompletedTask;
    }
    
    private ValueTask Update(Board board)
    {
        board.NextGeneration(_updater);
        return ValueTask.CompletedTask;
    }
    
    private async ValueTask Render(Board board)
    {
        await _boardRenderer.RenderBoard();
    }
    
    private Board Initialize()
    {
        var res = Board.InitializeState(new BoardSize(_width, _height), _factory);

        _subscription = res.Subscribe(_boardRenderer);

        return res;
    }

    public void Dispose()
    {
        _subscription.Dispose();
    }
}