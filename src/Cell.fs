namespace GameOfLife


[<Struct>]
type Coordinates = | TwoDimensionCoordinate of  x: int * y: int 

[<Struct>]
type Cell = 
    | Alive of coordinates1: Coordinates
    | Dead of coordinates2: Coordinates


module Cell =

    let name cell = 
        match cell with 
        | Alive(_) -> "alive"
        | Dead(_) -> "dead"

    let checkState neighbours cell =
        let aliveCount = neighbours |> Seq.filter(fun n -> match n with | Alive(_) -> true | _ -> false) |> Seq.length
        match cell with
        | Dead(coor) when aliveCount = 3 ->
            Alive(coor)
        | Alive(coor) when not (aliveCount = 2 || aliveCount = 3) -> 
            Dead(coor)
        | cell -> cell 

module Coordinates = 
    let isValid (coor: Coordinates) = 
        let (TwoDimensionCoordinate(x, y)) = coor
        x >= 0 && y >= 0

    let isNeighbour coor1 coor2 =
        true
