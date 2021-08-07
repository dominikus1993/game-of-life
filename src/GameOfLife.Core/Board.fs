namespace GameOfLife.Core

type Board = private { Cells: Cell[,]}

module Board =
    let create (rows, colums) =
        let array = [0..rows-1] |> Seq.map(fun row -> [0..colums-1] |> Seq.map(fun col -> Cell.createRandom(TwoDimensionalCoordinate(row, col))))
        { Cells = array2D(array) }

    let nextGen(board: Board) =
        board
        
    let print (board: Board) (printF : Board -> Async<unit>) =
        printF board 

