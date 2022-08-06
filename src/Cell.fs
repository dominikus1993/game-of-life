namespace GameOfLife


[<Struct>]
type Coordinates = | TwoDimensionCoordinate of  x: int * y: int 

type Cell =  
    | Alive
    | Dead

module Cell =
    open System

    let private r = Random()
    
    let random () =
        if r.Next() % 2 = 0 then
            Alive
        else
            Dead
        
    let name cell = 
        match cell with 
        | Alive -> "alive"
        | Dead -> "dead"

    let updateState neighbours cell =
        let aliveCount = neighbours |> Seq.filter(fun n -> match n with | Alive(_) -> true | _ -> false) |> Seq.length
        match cell with
        | Dead when aliveCount = 3 ->
            Alive
        | Alive when not (aliveCount = 2 || aliveCount = 3) -> 
            Dead
        | cell -> cell 

module Coordinates = 
    let isValid (coor: Coordinates) = 
        let (TwoDimensionCoordinate(x, y)) = coor
        x >= 0 && y >= 0

    let isNeighbour coor1 coor2 =
        true
    