using Akka.Actor;
using Akka.TestKit.Xunit2;
using GameOfLife.Core.Cells;

namespace GameOfLife.Core.Tests.Cells;

public sealed class CellTests : TestKit
{

    [Fact]
    public async Task Cell__WithDeadState_IsActive_ShouldBeFalse()
    {
        // Arrange
        var cell = this.Sys.ActorOf(CellActor.Props(new Coordinate(0, 0), CellState.Dead));
        var probe = this.CreateTestProbe();

        var currentGen = Generation.Zero;

        // Act
        cell.Tell(new CheckCellState(currentGen), this.TestActor);

        var msg = await ExpectMsgAsync<CheckCellStateResponse>(TimeSpan.FromSeconds(30));

        // Assert

        Assert.Equal(CellState.Dead, msg.State);
    }

    [Fact]
    public async Task Cell__WithAliveState_IsActive_ShouldBeFalse()
    {
        // Arrange
        var cell = this.Sys.ActorOf(CellActor.Props(new Coordinate(0, 0), CellState.Alive));

        var currentGen = Generation.Zero;

        // Act
        cell.Tell(new CheckCellState(currentGen), this.TestActor);

        var msg = await ExpectMsgAsync<CheckCellStateResponse>(TimeSpan.FromSeconds(30));

        // Assert

        Assert.Equal(CellState.Alive, msg.State);
    }


    [Fact]
    public async Task Cell__WithAliveState_IsActive_InFutureGen_ShouldThrowException()
    {
        // Arrange
        var cell = this.Sys.ActorOf(CellActor.Props(new Coordinate(0, 0), CellState.Alive));

        var currentGen = new Generation(10);

        // Act
        cell.Tell(new CheckCellState(currentGen), this.TestActor);

        var msg = await ExpectMsgAsync<CheckCellStateFailedResponse>(TimeSpan.FromSeconds(30));

        // Assert

        Assert.NotNull(msg);

    }



    [Fact]
    public async Task TestNextGenerationOfAliveCellWithNoNeighbours()
    {
        // Arrange
        var cell = this.Sys.ActorOf(CellActor.Props(new Coordinate(0, 0), CellState.Alive));
        
        // Act

        cell.Tell(new CheckNextGenerations(Generation.First), base.TestActor);

        var msg = await ExpectMsgAsync<CheckCellStateFailedResponse>(TimeSpan.FromSeconds(30));
        // Assert

        Assert.NotNull(msg);
        Assert.Equal(CheckCellStateFailedResponse.NoNeighbours, msg);
    }


    [Theory]
    [InlineData(1, false)]
    [InlineData(2, true)]
    [InlineData(3, true)]
    [InlineData(4, false)]
    [InlineData(5, false)]
    public async Task TestNextGenerationOfAliveCellWithNAliveNeighbours(int nAliveNeighbours, bool shouldSurvive)
    {
        // Arrange
        var cell = this.Sys.ActorOf(CellActor.Props(new Coordinate(0, 0), CellState.Alive));
        var neighbours = new List<IActorRef>();
        for (int i = 0; i < nAliveNeighbours; i++)
        {
            neighbours.Add(this.Sys.ActorOf(CellActor.Props(new Coordinate(0, 0), CellState.Alive)));
        }
        
        cell.Tell(new AddNeighbours([..neighbours]), base.TestActor);

        _ = await ExpectMsgAsync<NeighboursAdded>(TimeSpan.FromSeconds(30));
        
        
        // Act

        cell.Tell(new CheckNextGenerations(Generation.Zero), base.TestActor);

        var resp = await ExpectMsgAsync<CheckNextGenerationsResponse>(TimeSpan.FromSeconds(30));

        // Assert
        Assert.Equal(shouldSurvive, resp.State == CellState.Alive);
    }
}
//     
//     [Fact]
//     public async Task TestNextGenerationOfDeadWithNoNeighbours()
//     {
//         // Arrange
//         var cell = new Cell();
//         await cell.SetCellState(CellState.Dead);
//         
//         // Act
//         var result = await cell.NextGeneration();
//         
//         // Assert
//         Assert.True(result.IsFailed);
//     }
//     
//     [Theory]
//     [InlineData(1, false)]
//     [InlineData(2, false)]
//     [InlineData(3, true)]
//     [InlineData(4, false)]
//     [InlineData(5, false)]
//     public async Task TestNextGenerationOfDeadCellWithNAliveNeighbours(int nAliveNeighbours, bool shouldBeAlive)
//     {
//         // Arrange
//         var firstState = CellState.Dead;
//         var cell = new Cell();
//         await cell.SetCellState(firstState);
//         var ts = Enumerable.Range(0, nAliveNeighbours).Select(async x =>
//         {
//             var cell = new Cell();
//             await cell.SetCellState(CellState.Alive);
//             return cell;
//         });
//         var cells = await Task.WhenAll(ts);
//         await cell.AddNeighbours(cells);
//         // Act
//
//         var gen = await cell.NextGeneration();
//         
//         // Assert
//
//         var expectedState = await cell.IsAlive(gen.Value);
//         var lastState = await cell.IsAlive(Generation.First);
//         
//         Assert.True(lastState.IsSuccess);
//         Assert.False(lastState.Value);
//         Assert.Equal(shouldBeAlive, expectedState.Value);
//     }
// }