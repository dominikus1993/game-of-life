using System.Security.Cryptography;
using GameOfLife.Core.Cells;
using GameOfLife.Core.Services;
using GameOfLife.Core.Types;

namespace GameOfLife.Core;

public sealed class Board
{
    private readonly Cell[,] _cells;

    public Board(ICellInitialStateDecider initialStateDecider, int size = 10)
    {
        _cells = new Cell[size, size];
        for (uint x = 0; x < size; x++)
        {
            for (uint y = 0; y < size; y++)
            {
                _cells[x, y] = initialStateDecider.GetInitialCellState(new Coordinate(x, y));
            }
        }
    }
}