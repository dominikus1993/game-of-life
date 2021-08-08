import { Coordinate } from "./Coordinate";

export interface Cell {
    readonly Coordinate: Coordinate,
    checkState: (neighbours: Cell[]) => Cell
    
}

function isDead(cell: Cell): cell is DeadCell {
    return cell instanceof DeadCell;
}

export function isAlive(cell: Cell): cell is AliveCell {
    return cell instanceof AliveCell;
}

export class DeadCell implements Cell {
    readonly Coordinate: Coordinate;

    constructor(coordinate: Coordinate) {
        this.Coordinate = coordinate;
    }

    checkState(neighbours: Cell[]): Cell {
        const aliveCount = neighbours.filter((cell) => isAlive(cell)).length;
        if(aliveCount === 3) {
            return new AliveCell(this.Coordinate);
        }
        return this;
    }
    
}

export class AliveCell implements Cell {
    Coordinate: Coordinate;

    constructor(coordinate: Coordinate) {
        this.Coordinate = coordinate;
    }

    checkState(neighbours: Cell[]): Cell {
        const aliveCount = neighbours.filter((cell) => isAlive(cell)).length;
        if(aliveCount === 2 || aliveCount === 3) {
            return this;
        }
        return new DeadCell(this.Coordinate);
    }

}