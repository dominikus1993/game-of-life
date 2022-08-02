module CellTests

open Fable.Mocha
open GameOfLife

let cell =
    testList "Cell tests" [
        test "Dead name works" {
            let cellName = Dead({X = 2; Y = 3}) |> Cell.name
            Expect.equal cellName "dead" "dead cell"
        }
        test "Alive name works" {
            let cellName = Alive({X = 2; Y = 3}) |> Cell.name
            Expect.equal cellName "alive" "dead cell"
        }
        test "Check state dead cell when new state should be a Alive" {
            let sampleCoord = {X = 2; Y = 3};
            let cell = Dead(sampleCoord)
            let neighbours = [Alive(sampleCoord); Alive(sampleCoord); Alive(sampleCoord);]
            let subject = cell |> Cell.checkState(neighbours)
            Expect.equal subject (Alive(sampleCoord)) "dead cell"
        }
        test "Check state dead cell when new state should be a Dead"  {
            let sampleCoord = {X = 2; Y = 3};
            let cell = Dead(sampleCoord)
            let neighbours = [Alive(sampleCoord); Alive(sampleCoord); Alive(sampleCoord);Alive(sampleCoord)]
            let subject = cell |> Cell.checkState(neighbours)
            Expect.equal subject (Dead(sampleCoord)) "dead cell"
        }
        test "Check state Alive cell when new state should be a Alive" {
            let sampleCoord = {X = 2; Y = 3};
            let cell = Alive(sampleCoord)
            let neighbours = [Alive(sampleCoord); Alive(sampleCoord); Alive(sampleCoord);]
            let subject = cell |> Cell.checkState(neighbours)
            Expect.equal subject (Alive(sampleCoord)) "dead cell"
        }
        test "Check state dead cell when new state should be a Dead"  {
            let sampleCoord = {X = 2; Y = 3};
            let cell = Dead(sampleCoord)
            let neighbours = [Alive(sampleCoord); Alive(sampleCoord); Alive(sampleCoord);Alive(sampleCoord)]
            let subject = cell |> Cell.checkState(neighbours)
            Expect.equal subject (Dead(sampleCoord)) "dead cell"
        }
    ]

Mocha.runTests cell |> ignore