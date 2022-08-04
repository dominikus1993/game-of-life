module Tests
open Fable.Mocha

let allTests = testList "All" [
    CellTests.cell
    BoardTests.board
]


Mocha.runTests allTests |> ignore