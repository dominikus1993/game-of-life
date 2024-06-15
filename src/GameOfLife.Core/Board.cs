// using System.Security.Cryptography;
// using Akka.Actor;
// using FluentResults;
// using GameOfLife.Core.Cells;
// using GameOfLife.Core.Services;
// using GameOfLife.Core.Types;
// using Orleans;
//
// namespace GameOfLife.Core;
//
// public interface IBoardGrain : IGrain
// {
//     Task<Result> Initialize(int size);
// }
//
// public sealed class Board : Grain, IBoardGrain
// {
//     private readonly IActorRef[,] _cells;
//
//     public Board(ICellInitialStateDecider initialStateDecider, int size = 10)
//     {
//         // _cells = new Cell[size, size];
//         // for (uint x = 0; x < size; x++)
//         // {
//         //     for (uint y = 0; y < size; y++)
//         //     {
//         //         _cells[x, y] = initialStateDecider.GetInitialCellState(new Coordinate(x, y));
//         //     }
//         // }
//     }
//
//
//     public Task<Result> Initialize(int size)
//     {
//         throw new NotImplementedException();
//     }
// }