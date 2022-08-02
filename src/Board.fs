namespace GameOfLife

type Board = { Cells: Cell[][] }


module Board = 

    let getNeighbours (cell: Cell) (cells: seq<Cell>) =
        let coors = match cell with Alive(coor) -> coor | Dead(coor) -> coor


        2
