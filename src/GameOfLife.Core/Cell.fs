namespace GameOfLife.Core

type Coordinate = 
    | TwoDimensionalCoordinate of x: int * y: int

type Cell = 
    | Alive of Coordinate
    | Dead of Coordinate


module Coordinate = 
    let isCorrect coor = 
        match coor with 
        | TwoDimensionalCoordinate (x, y) ->
            x >= 0 && y >= 0

    let getNeighbours(coor: Coordinate) = 
        match coor with
        | TwoDimensionalCoordinate(x, y) ->
            seq {
                for i in x-1..x+1 do
                    for j in y-1..y+1 do
                        let coor = TwoDimensionalCoordinate(i, j);
                        if (i<>x || j<>y) && isCorrect(coor) then                           
                            yield coor
            }

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