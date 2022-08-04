module BoardTests

open Fable.Mocha
open GameOfLife


let coordinates =
    testList "Generate Coordinates tests" [
        test "0 index should be (x: 0, y: 0)" {
            let board = Board.newBoard 10 10
            let subject =  board |> Board.generateCoordinate 0
            Expect.equal subject (TwoDimensionCoordinate(0, 0)) "dead cell"
        }
        test "1 index should be (x: 1, y: 0)" {
            let board = Board.newBoard 10 10
            let subject =  board |> Board.generateCoordinate 2
            Expect.equal subject (TwoDimensionCoordinate(2, 0)) "dead cell"
        }
        test "10 index should be (x: 0, y: 1)" {
            let board = Board.newBoard 10 10
            let subject =  board |> Board.generateCoordinate 10
            Expect.equal subject (TwoDimensionCoordinate(0, 1)) "dead cell"
        }
        test "21 index should be (x: 1, y: 2)" {
            let board = Board.newBoard 10 10
            let subject =  board |> Board.generateCoordinate 21
            Expect.equal subject (TwoDimensionCoordinate(1, 2)) "dead cell"
        }
    ]

let board =
    testList "Board tests" [
        test "Dead name works" {
            let cellName = Dead |> Cell.name
            Expect.equal cellName "dead" "dead cell"
        }
        test "Alive name works" {
            let cellName = Alive |> Cell.name
            Expect.equal cellName "alive" "dead cell"
        }
        test "Check state dead cell when new state should be a Alive" {
            let cell = Dead
            let neighbours = [Alive; Alive; Alive;]
            let subject = cell |> Cell.updateState(neighbours)
            Expect.equal subject (Alive) "dead cell"
        }
        test "Check state dead cell when new state should be a Dead"  {
            let cell = Dead
            let neighbours = [Alive; Alive; Alive;Alive]
            let subject = cell |> Cell.updateState(neighbours)
            Expect.equal subject (Dead) "dead cell"
        }
        test "Check state Alive cell when new state should be a Alive" {
            let cell = Alive
            let neighbours = [Alive; Alive; Alive;]
            let subject = cell |> Cell.updateState(neighbours)
            Expect.equal subject (Alive) "dead cell"
        }
        test "Check state dead cell when new state should be a Dead"  {
            let cell = Dead
            let neighbours = [Alive; Alive; Alive;Alive]
            let subject = cell |> Cell.updateState(neighbours)
            Expect.equal subject (Dead) "dead cell"
        }
    ]