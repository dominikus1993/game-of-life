// See https://aka.ms/new-console-template for more information

using GameOfLife.Core;
using GameOfLife.Core.Abstraction;
using GameOfLife.Core.Cells;

using var cancellationTokenSource = new CancellationTokenSource();

Console.CancelKeyPress += (_, _) =>
{
    cancellationTokenSource.Cancel();
};

var neighbourFinder = new NeighborFinder();
var updater = DefaultCellStateUpdater.Instance;
var determimenr = new RandomCellStateDeterminer(Random.Shared);
var renderer = new ConsoleBoardRenderer();
var factory = new CellFactory(neighbourFinder, determimenr);

using var game = new Game(100, 100, factory, updater, renderer);

await game.Run(cancellationTokenSource.Token);

