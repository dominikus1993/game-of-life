// See https://aka.ms/new-console-template for more information

using GameOfLife.Core;
using GameOfLife.Core.Cells;

using var cancellationTokenSource = new CancellationTokenSource();

Console.CancelKeyPress += (_, _) =>
{
    cancellationTokenSource.Cancel();
};

var neighbourFinder = new NeighborFinder();
var updater = DefaultCellStateUpdater.Instance;
var determimenr = new RandomCellStateDeterminer(Random.Shared);
var factory = new CellFactory(neighbourFinder, determimenr);

var game = new Game(10, 10, factory, updater);

await game.Run(cancellationTokenSource.Token);

