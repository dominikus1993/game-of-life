namespace GameOfLife

type Cell = 
    | Alive
    | Dead 


module Cell =

    let name cell = 
        match cell with 
        | Alive -> "alive"
        | Dead -> "dead"