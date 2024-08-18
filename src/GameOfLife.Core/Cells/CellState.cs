using System.Diagnostics.CodeAnalysis;

namespace GameOfLife.Core.Cells;

public abstract class CellState
{
    public static readonly CellState Alive = new AliveState();
    
    public static readonly CellState Dead = new DeadState();
    public int Value { get; }
    public string Name { get; }
    
    private CellState(int value, string name)
    {
        Value = value;
        Name = name;
    }
    
    public sealed class AliveState : CellState
    {
        public AliveState() : base(1, nameof(Alive))
        {
        }
    }

    public sealed class DeadState : CellState
    {
        public DeadState() : base(0, nameof(Dead))
        {
        }
    }
}