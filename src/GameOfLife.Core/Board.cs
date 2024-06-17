using System.Security.Cryptography;
using Akka.Actor;
using GameOfLife.Core.Cells;

namespace GameOfLife.Core;

public interface IDisplay
{
    void Render();
}

public sealed record Next();

public sealed class Board : UntypedActor
{
    private readonly Dictionary<Coordinate, IActorRef> _cells = new Dictionary<Coordinate, IActorRef>();
    private IActorRef _displayActor;
    private IActorRef _gameActor;
    private Generation _currentGeneration = Generation.Zero;
    private bool _isChecking = false;
    
    private readonly int _size;
    public Board(int size = 10)
    {
        _size = size;
    }

    protected override void PreStart()
    {

        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                var coord = new Coordinate(i, j);
                var state = RandomNumberGenerator.GetInt32(0, 3) == 1 ? CellState.Alive : CellState.Dead;
                var cell = Context.ActorOf(CellActor.Props(coord, state), CellName(coord));
                _cells.Add(coord, cell);
            }
        }

        foreach (var cell in _cells)
        {
            var neighbourCoordinates = GetNeighbours(cell.Key);
            var neighbours = neighbourCoordinates.Select(c => _cells.TryGetValue(c, out var cellActor) ? cellActor : null).OfType<IActorRef>().ToArray();
            cell.Value.Tell(new AddNeighbours(neighbours));
        }
        
        base.PreStart();
    }

    private Coordinate[] GetNeighbours(Coordinate coord)
    {
        Coordinate[] potentialNeighbours = [
            new Coordinate(coord.X - 1, coord.Y - 1),
            new Coordinate(coord.X - 1, coord.Y),
            new Coordinate(coord.X - 1, coord.Y + 1),
            new Coordinate(coord.X, coord.Y - 1),
            new Coordinate(coord.X, coord.Y + 1),
            new Coordinate(coord.X + 1, coord.Y - 1),
            new Coordinate(coord.X + 1, coord.Y),
            new Coordinate(coord.X + 1, coord.Y + 1)
        ];
        
        return potentialNeighbours.Where(c => c.IsValidCoordinate(_size)).ToArray();
    }

    private static string CellName(Coordinate coord) => $"cell_{coord.X}_{coord.Y}";
    
    
    public static Props Props(int size = 10) => Akka.Actor.Props.Create(() => new Board(size));
    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case Next msg:
                CheckNextGenerationsHandler(msg);
                break;
            case CheckNextGenerationsResponse msg:
                _displayActor.Tell(msg);
                break;
        }
    }

    private void CheckNextGenerationsHandler(Next msg)
    {
        if (_isChecking)
        {
            return;
        }

        _isChecking = true;
        foreach (var cell in _cells)
        {
            cell.Value.Tell(new CheckNextGenerations(_currentGeneration));
        }
    }
}