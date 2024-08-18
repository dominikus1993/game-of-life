namespace GameOfLife.Core.Cells;

public interface ICellFactory
{
    Cell CreateCell(Location location, CellState state);
    IReadOnlyList<Cell> CreateCells(int width, int height);
}

public interface INeighborFinder
{
    IReadOnlyList<Cell> FindNeighbors(Cell cell, IReadOnlyList<Cell> cells);
}

public interface ICellStateDeterminer
{
    CellState DetermineState();
}

public sealed class CellFactory : ICellFactory
{
    private readonly INeighborFinder _neighborFinder;
    private readonly ICellStateDeterminer _cellStateDeterminer;

    public CellFactory(INeighborFinder neighborFinder, ICellStateDeterminer cellStateDeterminer)
    {
        _neighborFinder = neighborFinder;
        _cellStateDeterminer = cellStateDeterminer;
    }

    public Cell CreateCell(Location location, CellState state)
    {
        return Cell.Create(location, state);
    }

    public IReadOnlyList<Cell> CreateCells(int width, int height)
    {
        var cells = new List<Cell>(width * height);
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var location = new Location(x, y);
                var state = _cellStateDeterminer.DetermineState();
                var cell = CreateCell(location, state);
                cells.Add(cell);
            }
        }

        foreach (var cell in cells)
        {
            var neighbors = _neighborFinder.FindNeighbors(cell, cells);
            cell.AddNeighbors(neighbors);
        }

        return cells;
    }
}

public sealed class NeighborFinder : INeighborFinder
{
    public IReadOnlyList<Cell> FindNeighbors(Cell cell, IReadOnlyList<Cell> cells)
    {

        if (cells is {Count: 0})
        {
            return [];
        }
        
        var (maxX, maxY) = cells.Max(c => c.Location);
        var dict = cells.ToDictionary(c => c.Location);
        var (x, y) = cell.Location;
        Location[] potentialNeighbours = [new Location(x + 1, y), new Location(x - 1, y), new Location(x, y + 1), new Location(x, y - 1), new Location(x + 1, y + 1), new Location(x - 1, y - 1), new Location(x + 1, y - 1), new Location(x - 1, y + 1)];
        Location[] validNeighbours = potentialNeighbours.Where(location => location.X >= 0 && location.X <= maxX && location.Y >= 0 && location.Y <= maxY).ToArray();

        if (validNeighbours is {Length: 0})
        {
            return [];
        }
        
        var neighbors = new List<Cell>(validNeighbours.Length);
        foreach (var location in validNeighbours.AsSpan())
        {
            if (dict.TryGetValue(location, out var neighbor))
            {
                neighbors.Add(neighbor);
            }
        }
        
        return neighbors;
    }
}