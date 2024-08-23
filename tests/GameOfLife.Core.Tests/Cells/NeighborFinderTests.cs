using GameOfLife.Core.Cells;

namespace GameOfLife.Core.Tests.Cells;

public class NeighborFinderTests
{
    [Fact]
    public void TestWhenHasNeighbours()
    {
        // Arrange
        var cell = Cell.Create(new Location(0, 0), CellState.Dead);
        var neighbors = new[]
        {
            Cell.Create(new Location(0, 1), CellState.Dead),
            Cell.Create(new Location(1, 0), CellState.Dead),
            Cell.Create(new Location(1, 1), CellState.Dead),
        };
        
        var finder = new NeighborFinder();
        
        
        // Act
        var result = finder.FindNeighbors(cell, neighbors.ToDictionary(x => x.Location));
        
        // Assert
        
        Assert.NotEmpty(result);
        Assert.Equal(neighbors.Length, result.Count);
    }
    
    [Fact]
    public void TestWhenHasNeighboursAndMoreData()
    {
        // Arrange
        var cell = Cell.Create(new Location(0, 0), CellState.Dead);
        var neighbors = new List<Cell>();
        
        for (var i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                neighbors.Add(Cell.Create(new Location(i, j), CellState.Dead));
            }
        }
        
        var finder = new NeighborFinder();
        
        
        // Act
        var result = finder.FindNeighbors(cell, neighbors.ToDictionary(x => x.Location));
        
        // Assert
        
        Assert.NotEmpty(result);
        Assert.Equal(3, result.Count);
    }
    
    [Fact]
    public void TestWhenHasNoNeighbours()
    {
        // Arrange
        var cell = Cell.Create(new Location(0, 0), CellState.Dead);
        
        
        var finder = new NeighborFinder();
        
        
        // Act
        var result = finder.FindNeighbors(cell, new Dictionary<Location, Cell>());
        
        // Assert
        
        Assert.Empty(result);
    }
}