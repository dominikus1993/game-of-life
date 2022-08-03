namespace GameOfLife

type Board = { Cells: Cell[]; Width: int; Height: int }


module Board = 
    let generateCoordinate board index = 
        let y = index / board.Width
        let x = index % board.Width
        TwoDimensionCoordinate(x, y) 
        
    let getNeighbours (cell: Cell) (cells: seq<Cell>) =
        let coors = match cell with Alive(coor) -> coor | Dead(coor) -> coor


        2
