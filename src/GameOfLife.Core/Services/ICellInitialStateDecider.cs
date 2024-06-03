using System.Security.Cryptography;
using GameOfLife.Core.Cells;
using GameOfLife.Core.Types;
using Orleans;
using Orleans.Runtime;

namespace GameOfLife.Core.Services;

public interface ICellInitialStateDecider
{
    Task<ICellGrain> GetInitialCellState(Coordinate coordinate);
}

public sealed class OrleansCellInitialStateDecider : ICellInitialStateDecider
{
    private IGrainFactory _grainFactory;

    public OrleansCellInitialStateDecider(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }

    public async Task<ICellGrain> GetInitialCellState(Coordinate coordinate)
    {
        var n = RandomNumberGenerator.GetInt32(0, 200000);
        var cell = _grainFactory.GetGrain<ICellGrain>(Guid.NewGuid());
        if (n % 4 == 0)
        {
            await cell.SetCellState(CellState.Alive);
        }
        else
        {
            await cell.SetCellState(CellState.Dead);
        }

        return cell;
    }
}