namespace GameOfLife

[<Struct>]
type BoardCell = { Cell: Cell; Coordinates: Coordinates; }

type Board = { Cells: BoardCell[][]; Width: int; Height: int }

module BoardCell = 
    let create coord cell =
        { Cell = cell; Coordinates = coord;}

module Board = 
    let private generateRow column rows =
        seq {
            for row in rows do
                let cell = Cell.random()
                { Cell = cell; Coordinates = TwoDimensionCoordinate(column, row)}            
        }

    let generateCells height width =
        let rows = [0..height-1]
        seq {
            for colummn in [0..width-1] do
                yield generateRow colummn rows |> Seq.toArray
        } |> Seq.toArray

    let empty witdh height = 
        let cells = Array.empty<BoardCell[]>
        { Cells = cells; Width = witdh; Height = height}

    let newBoard witdh height = 
        let board = empty witdh height
        let cells = generateCells height witdh
        { board with Cells = cells }

    

    let isCorrectCoordinate coordinate board =
        let (TwoDimensionCoordinate(x, y)) = coordinate
        if x < 0 || y < 0 then
            false
        elif x > board.Width || y > board.Height then 
            false
        else 
            true
        
    let getNeighbours (cell: BoardCell) (board: Board) =
        let ({ Coordinates = TwoDimensionCoordinate(x, y) }) =  cell
        seq {
            for i in [x-1..x+1] do 
                for j in [y-1..y+1] do
                    if not(x = i && y = j) then
                        let coor = TwoDimensionCoordinate(i, j)
                        if board |> isCorrectCoordinate coor then
                            yield TwoDimensionCoordinate(i, j)
        }
