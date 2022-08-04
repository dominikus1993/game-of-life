namespace GameOfLife

[<Struct>]
type BoardCell = { Cell: Cell; Coordinates: Coordinates }

type Board = { Cells: BoardCell[]; Width: int; Height: int }


module Board = 
    let newBoard witdh height = 
        { Cells = Array.empty<BoardCell>; Width = witdh; Height = height}

    let isInBoard coordinate board =
        let (TwoDimensionCoordinate(x, y)) = coordinate
        if x < 0 || y < 0 then
            false
        elif x > board.Width || y > board.Height then 
            false
        else 
            true

    let generateCoordinate index board = 
        let y = index / board.Width
        let x = index % board.Width
        TwoDimensionCoordinate(x, y)

    let getIndexFromCoordinates coor board = 
        let (TwoDimensionCoordinate(x, _)) = coor
        let index = x * board.Width
        index
        
        
    let getNeighbours (cell: BoardCell) (board: Board) =
        let ({ Coordinates = TwoDimensionCoordinate(x, y) }) =  cell
        for i in [x-1..x+1] do 
            for j in [y-1..y+1] do
                //let isCorrect = board |> isInBoard(TwoDimensionCoordinate(i, j))

        2
