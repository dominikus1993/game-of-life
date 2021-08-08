import { Coordinate } from "./Coordinate";

export interface Cell {
    Coordinate: Coordinate,
}

export interface DeadCell extends Cell {
    
}

export interface AliveCell extends Cell {

}