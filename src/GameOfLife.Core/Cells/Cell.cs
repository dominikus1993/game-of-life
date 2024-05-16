namespace GameOfLife.Core.Cells;

public abstract class Cell
{
    public abstract bool IsAlive { get; }
}

public sealed class AliveCell : Cell
{
    public override bool IsAlive => true;
}

public sealed class DeadCell : Cell
{
    public override bool IsAlive => false;
}