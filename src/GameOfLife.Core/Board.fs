namespace GameOfLife.Core
open System.Collections.Generic

type Board = private { Cells: Cell[,]; }

module Board =
    let private getNeighbourIndexes (coor: Coordinate) = 
        match coor with
        | TwoDimensionalCoordinate(x, y) ->
            seq {
                for i in x-1..x+1 do
                    for j in y-1..y+1 do
                        let coor = TwoDimensionalCoordinate(i, j);
                        if (i<>x || j<>y) && Coordinate.isCorrect(coor) then                           
                            yield coor
            }
            
    let create (rows, colums) =
        let array = [0..rows-1] |> Seq.map(fun row -> [0..colums-1] |> Seq.map(fun col -> Cell.createRandom(TwoDimensionalCoordinate(row, col))))
        { Cells = array2D(array) }

    let next(board: Board) : Board =
        let cells = board.Cells

        board
        
    let print (board: Board) =
        board.Cells

