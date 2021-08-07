module Board.Tests

open System
open Xunit
open GameOfLife.Core

[<Fact>]
let ``Test create`` () =
    let board = Board.create (10, 10)
    Assert.True(true)
