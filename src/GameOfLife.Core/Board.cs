using System.Diagnostics.CodeAnalysis;
using GameOfLife.Core.Abstraction;
using GameOfLife.Core.Cells;

namespace GameOfLife.Core;

public readonly record struct BoardSize(int Width, int Height);
public sealed class BoardState
{
    public  IReadOnlyList<Cell> Cells { get; }
    public  int Generation { get; }
    
    public BoardSize Size { get; }
    
    public BoardState(IReadOnlyList<Cell> cells, int generation, BoardSize size)
    {
        Cells = cells;
        Generation = generation;
        Size = size;
    }
    
    public static readonly BoardState Empty = new([], 0, new BoardSize());
}

public sealed class Board : IObservable<BoardState>
{
    private IReadOnlyList<Cell> Cells { get; }
    private int Generation { get; set; }
    private List<IObserver<BoardState>> Observers { get; } = [];

    private readonly BoardSize _boardSize;

    private Board(IReadOnlyList<Cell> cells, BoardSize size)
    {
        Cells = cells;
        Generation = 0;
        _boardSize = size;
    }
    
    public static Board InitializeState(BoardSize size, ICellFactory factory)
    {
        var cells = factory.CreateCells(size.Width, size.Height);
        return new Board(cells, size);
    }


    public void NextGeneration(ICellStateUpdater updater)
    {
        foreach (var cell in Cells)
        {
            cell.UpdateState(updater);
        }
        Generation += 1;
        Notify();
    }
    
    private void Notify()
    {
        var state = new BoardState(Cells, Generation, _boardSize);
        foreach (var observer in Observers)
        {
            observer.OnNext(state);
        }
    }

    public IDisposable Subscribe(IObserver<BoardState> observer)
    {
        Observers.Add(observer);
        return new Unsubscriber(Observers, observer);
    }
    
    private sealed class Unsubscriber : IDisposable
    {
        private readonly List<IObserver<BoardState>>_observers;
        private readonly IObserver<BoardState>? _observer;

        public Unsubscriber(List<IObserver<BoardState>> observers, IObserver<BoardState> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }

}