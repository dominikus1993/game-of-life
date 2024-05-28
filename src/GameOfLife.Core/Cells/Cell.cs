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
    
    public static readonly Generation First = new(0);
}

public interface ICellGrain
{
    Task<bool> IsAlive(Generation generation);
    Task AddNeighbours(ICellGrain[] neighbours);
    
    Task<Generation> NextGeneration();
    
}

public sealed class Cell : Grain, ICellGrain
{
    private Generation _currentGeneration;
    private ICellGrain[] _neighbours = [];
    private CellState _currentState;
    private List<CellState> _stateHistory = [];

    public Cell(CellState cellState)
    {
        _currentState = cellState;
        _stateHistory.Add(cellState);
        _currentGeneration = Generation.First;
    }
    
    public Task<bool> IsAlive(Generation generation)
    {
        if (generation == _currentGeneration)
        {
            return Task.FromResult(_currentState == CellState.Alive);
        }

        if (generation.Value > _currentGeneration.Value)
        {
            throw new InvalidOperationException("Cannot check for a future generation");
        }
        
        var state = _stateHistory.ElementAtOrDefault(generation.Value);
        return Task.FromResult(state == CellState.Alive);
    }

    public Task AddNeighbours(ICellGrain[] neighbours)
    {
        _neighbours = neighbours;
        return Task.CompletedTask;
    }

    public async Task<Generation> NextGeneration()
    {
        if (_neighbours is {Length: 0})
        {
            throw new InvalidOperationException("No neighbours found");
        }
        
        var neighboursLifeStatusTasks = _neighbours.Select(async n => await n.IsAlive(_currentGeneration));
        var neighboursLifeStatus = await Task.WhenAll(neighboursLifeStatusTasks);
        
        var aliveNeighbours = neighboursLifeStatus.Count(s => s);

        if (_currentState == CellState.Alive)
        {
            if (aliveNeighbours is < 2 or > 3)
            {
                _currentState = CellState.Dead;
            }
        }
        else
        {
            if (aliveNeighbours == 3)
            {
                _currentState = CellState.Alive;
            }
        }
        
        _currentGeneration = _currentGeneration.Next();
        _stateHistory.Add(_currentState);
        return _currentGeneration;
    }
}
