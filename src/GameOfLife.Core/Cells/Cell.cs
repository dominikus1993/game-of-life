using Akka.Actor;
using FluentResults;
using Orleans;

namespace GameOfLife.Core.Cells;

public enum CellState : byte
{
    Dead = 0,
    Alive = 1
}

public readonly record struct Generation(int Value)
{
    public Generation Next() => new(Value: Value + 1);
    
    public static readonly Generation Zero = new(0);
    public static readonly Generation First = new(1);
}

public sealed record CheckCellState(Generation Generation);

public sealed record CheckCellStateResponse(Coordinate Coordinate, Generation Generation, CellState State);
public sealed record CheckCellStateFailedResponse(string Error);

public sealed record CellStateData(CellState LifeStatus);

public sealed record Coordinate(uint X, uint Y);
public sealed class CellActor : UntypedActor
{
    private Generation _currentGeneration;
    private IActorRef[] _neighbours = [];
    private readonly Coordinate _coordinate;
    private CellState _currentState;
    private List<CellState> _stateHistory = [];
  
    public CellActor(Coordinate coordinate, CellState initialState)
    {
        _currentGeneration = Generation.Zero;
        _coordinate = coordinate;
        _currentState = initialState;
        _stateHistory.Add(initialState);
    }

    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case Cells.CheckCellState msg:
                CheckCellState(msg);
                return;
        }
    }
    
    private void CheckCellState(Cells.CheckCellState msg)
    {
        var generation = msg.Generation;
        if (generation == _currentGeneration)
        {
            Sender.Tell(new Cells.CheckCellStateResponse(_coordinate, generation, _currentState));
            return;
        }

        if (generation.Value > _currentGeneration.Value)
        {
            Sender.Tell(new CheckCellStateFailedResponse("Generation not reached"));
        }
        
        var state = _stateHistory.ElementAtOrDefault(generation.Value);
        Sender.Tell(new Cells.CheckCellStateResponse(_coordinate, generation, state));
    }
}
