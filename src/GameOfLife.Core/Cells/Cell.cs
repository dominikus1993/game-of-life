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

public sealed record CheckNextGenerations(Generation Generation);
public sealed record CheckNextGenerationsResponse(Coordinate Coordinate, Generation Generation, CellState State);
public sealed record CheckNextGenerationsFailedResponse(string Error);

public sealed record CheckCellState(Generation Generation);

public sealed record CheckCellStateResponse(Coordinate Coordinate, Generation Generation, CellState State);
public sealed record CheckCellStateFailedResponse(string Error);

public sealed record AddNeighbours(IActorRef[] Neighbours);
public sealed record NeighboursAdded(Coordinate Coordinate);
public sealed record Coordinate(uint X, uint Y);
public sealed class CellActor : UntypedActor
{
    private Generation _currentGeneration;
    private IActorRef[] _neighbours = [];
    private List<CheckCellStateResponse> _neighboursStateResponses = [];
    private readonly Coordinate _coordinate;
    private CellState _currentState;
    private List<CellState> _stateHistory = [];
    private IActorRef? _currentCheckSender;
  
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
            case CheckNextGenerations msg:
                CheckCellNextGenerations(msg);
                return;
            case Cells.CheckCellState msg:
                CheckCellState(msg);
                return;
            case CheckCellStateResponse msg:
                ProcessCheckCellStateResponse(msg);
                return;
            case AddNeighbours msg:
                AddCellNeighbours(msg);
                return;
        }
    }

    private void ProcessCheckCellStateResponse(CheckCellStateResponse msg)
    {
        _neighboursStateResponses.Add(msg);
        if (_neighboursStateResponses.Count == _neighbours.Length)
        {
            _currentCheckSender?.Tell(new CheckNextGenerationsResponse(_coordinate, _currentGeneration, _currentState));
            _currentCheckSender = null;
        }
    }

    private void CheckCellNextGenerations(CheckNextGenerations msg)
    {
        if (_currentCheckSender is not null)
        {
            Sender.Tell(new CheckCellStateFailedResponse("Already checking"));
            return;
        }
        
        _currentCheckSender = Sender;
        foreach (var neighbour in _neighbours)
        {
            neighbour.Tell(new Cells.CheckCellState(_currentGeneration));
        }
    }

    private void AddCellNeighbours(AddNeighbours msg)
    {
        _neighbours = msg.Neighbours;
        Sender.Tell(new NeighboursAdded(_coordinate));
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
