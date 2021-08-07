module Cell.Tests

open System
open Xunit
open GameOfLife.Core

let crateCells (aliveCount: int) (deadCount: int) =
    let alive = [|0..aliveCount - 1|] |> Array.map (fun i -> Alive(TwoDimensionalCoordinate(i, i)))
    let dead = [|0..deadCount - 1|] |> Array.map (fun i -> Dead(TwoDimensionalCoordinate(i, i)))
    Array.concat [dead; alive]

[<Theory>]
[<InlineData(1, true)>]
[<InlineData(2, false)>]
[<InlineData(3, false)>]
[<InlineData(4, true)>]
let ``Test alive when has x neighbour`` (alive: int, shouldBeDead: bool) =
    let neighours = crateCells (alive) (6) 
    let cell = Alive(TwoDimensionalCoordinate(0, 0))
    let subject = Cell.checkLiveness(cell)(neighours)
    match subject with
    | Alive(_) -> Assert.True(shouldBeDead |> not)
    | Dead(_) -> Assert.True(shouldBeDead)
