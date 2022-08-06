module CellTests

open Fable.Mocha
open GameOfLife

let cell =
    testList "Cell tests" [
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
            let subject = cell |> Cell.mapState(neighbours)
            Expect.equal subject (Alive) "dead cell"
        }
        test "Check state dead cell when new state should be a Dead"  {
            let cell = Dead
            let neighbours = [Alive; Alive; Alive;Alive]
            let subject = cell |> Cell.mapState(neighbours)
            Expect.equal subject (Dead) "dead cell"
        }
        test "Check state Alive cell when new state should be a Alive" {
            let cell = Alive
            let neighbours = [Alive; Alive; Alive;]
            let subject = cell |> Cell.mapState(neighbours)
            Expect.equal subject (Alive) "dead cell"
        }
        test "Check state dead cell when new state should be a Dead"  {
            let cell = Dead
            let neighbours = [Alive; Alive; Alive;Alive]
            let subject = cell |> Cell.mapState(neighbours)
            Expect.equal subject (Dead) "dead cell"
        }
    ]
