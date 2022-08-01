module GameOfLife
open GameOfLife;
open Fable.Jester

Jest.describe("Cell Tests", fun () ->
    Jest.test("test cell name", fun () ->
        let cellName = Dead |> Cell.name

        Jest.expect(cellName).toEqual("dead")
    )
)