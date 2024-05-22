using GameOfLife.Core.Cells;
using GameOfLife.Core.Services;
using GameOfLife.Core.Types;

namespace GameOfLife.Core.Tests;

public sealed class FakeDeadCellsStateDecider : ICellInitialStateDecider
{
    public Cell GetInitialCellState(Coordinate coordinate) => new DeadCell();
}

public class BoardTests
{
    [Fact]
    public void Board_WithNoCells_ShouldNotThrowExceptipns()
    {
        // Arrange
        var record = Record.Exception(() => new Board(new FakeDeadCellsStateDecider(), 3));
        Assert.Null(record);
    }
}