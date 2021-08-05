namespace GameOfLife.Core

type Cell = 
    | Alive
    | Dead


type Board = { Cells: Cell[,] }