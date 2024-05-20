// See https://aka.ms/new-console-template for more information

using GameOfLife.Core;
using var cancellationTokenSource = new CancellationTokenSource();

Console.CancelKeyPress += (_, _) =>
{
    cancellationTokenSource.Cancel();
};

var game = new Game();

await game.Run(cancellationTokenSource.Token);

