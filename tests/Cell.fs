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
    ]

Mocha.runTests cell |> ignore