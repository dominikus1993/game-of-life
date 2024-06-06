using System.Security.Cryptography;
using Akka.Actor;
using GameOfLife.Core.Cells;
using GameOfLife.Core.Types;
using Orleans;
using Orleans.Runtime;

namespace GameOfLife.Core.Services;

public interface ICellInitialStateDecider
{
    IActorRef GetInitialCellState(Coordinate coordinate);
}

public sealed class OrleansCellInitialStateDecider : ICellInitialStateDecider
{
    private IGrainFactory _grainFactory;

    public OrleansCellInitialStateDecider(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }

    public IActorRef GetInitialCellState(Coordinate coordinate)
    {
        
    }
}