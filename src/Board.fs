namespace GameOfLife

[<Struct>]
type BoardCell = { Cell: Cell; Coordinates: Coordinates; }

type Board = { Cells: BoardCell[][]; Width: int; Height: int }

module BoardCell = 
    let create coord cell =
        { Cell = cell; Coordinates = coord;}

module Board = 
    
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
        } |> Seq.toArray
    
    let private memonizedGetNeighbours board = Utils.memonize (getNeighbours(board))

    let private generateRow column rows =
        seq {
            for row in rows do
                let cell = Cell.random()
                { Cell = cell; Coordinates = TwoDimensionCoordinate(column, row)}            
        }

    let getCell coord board =
        let (TwoDimensionCoordinate(x, y)) = coord
        board.Cells.[x].[y]

    let private generateCells height width =
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
    
    let private nextCell cell board =
        let neighbours = memonizedGetNeighbours cell board |> Array.map(fun coord -> getCell coord board) |> Array.map(fun x -> x.Cell)
        { cell with Cell = cell.Cell |> Cell.mapState neighbours }

    let next (borad: Board) = 
        let cells = borad.Cells |> Array.map(fun row -> row |> Array.map(fun colummn -> nextCell colummn borad))
        { borad with Cells = cells}