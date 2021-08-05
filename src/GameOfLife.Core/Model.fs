namespace GameOfLife.Core

type Cell = 
    | Alive
    | Dead


type Board(rows: int, columns: int) =
    member this.cells = []