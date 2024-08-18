using GameOfLife.Core.Cells;

namespace GameOfLife.Core.Tests.Cells;

public class DefaultCellStateUpdaterTests
{
    private ICellStateUpdater _cellStateUpdater;

    public DefaultCellStateUpdaterTests()
    {
        _cellStateUpdater = DefaultCellStateUpdater.Instance;
    }
    
    [Fact]
    public void TestWhenAliveCellHasNoNeighborsThenItDies()
    {
        // Arrange
        var state = CellState.Alive;
        var neighbors = new List<Cell>();
        
        // Act
        var result = _cellStateUpdater.UpdateState(state, neighbors);
        
        // Assert
        Assert.Equal(CellState.Dead, result);
    }
    
    [Fact]
    public void TestWhenAliveCellHasOneNeighborThenItDies()
    {
        // Arrange
        var state = CellState.Alive;
        var neighbors = new List<Cell>
        {
            Cell.Create(new Location(0, 0), CellState.Alive)
        };
        
        // Act
        var result = _cellStateUpdater.UpdateState(state, neighbors);
        
        // Assert
        Assert.Equal(CellState.Dead, result);
    }
    
    [Fact]
    public void TestWhenAliveCellHasTwoNeighborsThenItLives()
    {
        // Arrange
        var state = CellState.Alive;
        var neighbors = new List<Cell>
        {
            Cell.Create(new Location(0, 0), CellState.Alive),
            Cell.Create(new Location(0, 1), CellState.Alive)
        };
        
        // Act
        var result = _cellStateUpdater.UpdateState(state, neighbors);
        
        // Assert
        Assert.Equal(CellState.Alive, result);
    }
    
    [Fact]
    public void TestWhenAliveCellHasThreeNeighborsThenItLives()
    {
        // Arrange
        var state = CellState.Alive;
        var neighbors = new List<Cell>
        {
            Cell.Create(new Location(0, 0), CellState.Alive),
            Cell.Create(new Location(0, 1), CellState.Alive),
            Cell.Create(new Location(0, 2), CellState.Alive)
        };
        
        // Act
        var result = _cellStateUpdater.UpdateState(state, neighbors);
        
        // Assert
        Assert.Equal(CellState.Alive, result);
    }
}