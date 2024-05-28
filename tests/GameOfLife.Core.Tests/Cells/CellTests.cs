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
    
    
    [Fact]
    public async Task TestNextGenerationOfAliveCellWithNoNeighbours()
    {
        // Arrange
        var cell = new Cell(CellState.Alive);
        
        // Act
        
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await cell.NextGeneration());
    }
    
    [Theory]
    [InlineData(1, false)]
    [InlineData(2, true)]
    [InlineData(3, true)]
    [InlineData(4, false)]
    [InlineData(5, false)]
    public async Task TestNextGenerationOfAliveCellWithNAliveNeighbours(int nAliveNeighbours, bool shouldSurvive)
    {
        // Arrange
        var cell = new Cell(CellState.Alive);
        await cell.AddNeighbours(Enumerable.Range(0, nAliveNeighbours).Select(x => new Cell(CellState.Alive)).ToArray());
        // Act

        var gen = await cell.NextGeneration();
        
        // Assert

        var expectedState = await cell.IsAlive(gen);
        
        Assert.Equal(shouldSurvive, expectedState);
    }
    
    [Fact]
    public async Task TestNextGenerationOfDeadWithNoNeighbours()
    {
        // Arrange
        var cell = new Cell(CellState.Dead);
        
        // Act
        
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await cell.NextGeneration());
    }
    
    [Theory]
    [InlineData(1, false)]
    [InlineData(2, false)]
    [InlineData(3, true)]
    [InlineData(4, false)]
    [InlineData(5, false)]
    public async Task TestNextGenerationOfDeadCellWithNAliveNeighbours(int nAliveNeighbours, bool shouldBeAlive)
    {
        // Arrange
        var cell = new Cell(CellState.Dead);
        await cell.AddNeighbours(Enumerable.Range(0, nAliveNeighbours).Select(x => new Cell(CellState.Alive)).ToArray());
        // Act

        var gen = await cell.NextGeneration();
        
        // Assert

        var expectedState = await cell.IsAlive(gen);
        
        Assert.Equal(shouldBeAlive, expectedState);
    }
}