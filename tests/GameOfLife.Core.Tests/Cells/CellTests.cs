using GameOfLife.Core.Cells;

namespace GameOfLife.Core.Tests.Cells;

public sealed class CellTests
{


    [Fact] 
    public async Task Cell__WithDeadState_IsActive_ShouldBeFalse()
    {
        // Arrange
        var cell = new Cell(CellState.Dead);
        
        // Act
        var state = await cell.IsAlive(Generation.First);
        
        // Assert
        Assert.False(state);
    }
    
    [Fact] 
    public async Task Cell__WithAliveState_IsActive_ShouldBeFalse()
    {
        // Arrange
        var cell = new Cell(CellState.Alive);
        
        // Act
        var state = await cell.IsAlive(Generation.First);
        
        // Assert
        Assert.True(state);
    }
    
    [Fact] 
    public async Task Cell__WithAliveState_IsActive_InFutureGen_ShouldThrowException()
    {
        // Arrange
        var cell = new Cell(CellState.Alive);
        
        // Act

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await cell.IsAlive(new Generation(10)));
    }
}