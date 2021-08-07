namespace GameOfLife.Core

type Coordinate = 
    | TwoDimensionalCoordinate of x: int * y: int

type Cell = 
    | Alive of Coordinate
    | Dead of Coordinate

 module Cell =
    open System

    let checkLiveness (cell: Cell) (neighbors: Cell[]) =
        let aliveNeighours = neighbors |> Array.filter(fun x -> match x with | Alive(_) -> true | Dead(_) -> false) |> Array.length
        match cell with
        | Alive (c) ->
            if aliveNeighours = 2 || aliveNeighours = 3 then
                cell
            else
                Dead(c)
        | Dead (c) ->
            if aliveNeighours = 3 then
                Alive(c)
            else
                cell

    let createRandom (coor: Coordinate) =
        let r = Random()
        let num = r.Next(1, 10000)
        if num % 2 = 0 then 
            Alive(coor)
        else
            Dead(coor)