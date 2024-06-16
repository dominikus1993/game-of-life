using Akka.TestKit.Xunit2;
using GameOfLife.Core.Cells;
using GameOfLife.Core.Types;

namespace GameOfLife.Core.Tests;


public class BoardTests : TestKit
{
    [Fact]
    public void Board_WithNoCells_ShouldNotThrowExceptipns()
    {
        // Arrange
        var record = Record.Exception(() => base.Sys.ActorOf(Board.Props(10)));
        Assert.Null(record);
    }
}