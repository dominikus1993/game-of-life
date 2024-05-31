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
    
    public static readonly Generation First = new(0);
}

public interface ICellGrain
{
    Task<Result<bool>> IsAlive(Generation generation);
    Task<Result> AddNeighbours(ICellGrain[] neighbours);
    
    Task<Result<Generation>> NextGeneration();
    
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
    
    public Task<Result<bool>> IsAlive(Generation generation)
    {
        if (generation == _currentGeneration)
        {
            return Task.FromResult(Result.Ok(_currentState == CellState.Alive));
        }

        if (generation.Value > _currentGeneration.Value)
        {
            return Task.FromResult(Result.Fail<bool>("Cannot check future generations"));
        }
        
        var state = _stateHistory.ElementAtOrDefault(generation.Value);
        return Task.FromResult(Result.Ok(state == CellState.Alive));
    }

    public Task<Result> AddNeighbours(ICellGrain[] neighbours)
    {
        _neighbours = neighbours;
        return Task.FromResult(Result.Ok());
    }

    public async Task<Result<Generation>> NextGeneration()
    {
        if (_neighbours is {Length: 0})
        {
            return Result.Fail("No neighbours added");
        }
        
        var neighboursLifeStatusTasks = _neighbours.Select(async n => await n.IsAlive(_currentGeneration));
        var neighboursLifeStatus = await Task.WhenAll(neighboursLifeStatusTasks);
        
        var aliveNeighbours = neighboursLifeStatus.Where(x => x.IsSuccess).Count(s => s.Value);

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
        return Result.Ok(_currentGeneration);
    }
}
