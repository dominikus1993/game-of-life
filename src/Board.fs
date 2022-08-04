namespace GameOfLife

[<Struct>]
type BoardCell = { Cell: Cell; Coordinates: Coordinates }

type Board = { Cells: BoardCell[]; Width: int; Height: int }


module Board = 
    let newBoard witdh height = 
        { Cells = Array.empty<BoardCell>; Width = witdh; Height = height}

    let generateCoordinate index board = 
        let y = index / board.Width
        let x = index % board.Width
        TwoDimensionCoordinate(x, y) 
        
    let getNeighbours (cell: Cell) (cells: seq<Cell>) =
        2
