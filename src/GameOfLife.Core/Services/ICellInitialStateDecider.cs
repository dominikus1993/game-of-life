using GameOfLife.Core.Cells;
using GameOfLife.Core.Types;
using Orleans;

namespace GameOfLife.Core.Services;

public interface ICellInitialStateDecider
{
    Cell GetInitialCellState(Coordinate coordinate);
}

// public sealed class OrleansCellInitialStateDecider : ICellInitialStateDecider
// {
//     private IGrainFactory _grainFactory;
//
//     public OrleansCellInitialStateDecider(IGrainFactory grainFactory)
//     {
//         _grainFactory = grainFactory;
//     }
//
//     public Cell GetInitialCellState(Coordinate coordinate)
//     {
//         var n = Random.Shared.Next(0, 213769);
//         if (n % 3 == 0)
//         {
//             return 
//         }
//     }
// }