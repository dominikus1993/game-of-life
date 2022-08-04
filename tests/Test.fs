module Tests
open Fable.Mocha

let allTests = testList "All" [
    CellTests.cell
    BoardTests.board
    BoardTests.coordinates
]


Mocha.runTests allTests |> ignore