namespace GameOfLife.Core.Cells;

public readonly record struct Location(int X, int Y) : IComparable<Location>
{
    public int CompareTo(Location other)
    {
        var xComparison = X.CompareTo(other.X);
        if (xComparison != 0) return xComparison;
        return Y.CompareTo(other.Y);
    }
}