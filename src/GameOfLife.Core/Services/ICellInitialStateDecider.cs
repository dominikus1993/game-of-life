using GameOfLife.Core.Cells;
using GameOfLife.Core.Types;

namespace GameOfLife.Core.Services;

public interface ICellInitialStateDecider
{
    Cell GetInitialCellState(Coordinate coordinate);
}