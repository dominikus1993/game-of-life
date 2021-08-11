import { Coordinate } from "./Coordinate";

export type AliveCell = {
    kind: "Alive",
    Coordinate: Coordinate,
}

export type DeadCell = {
    kind: "Dead",
    Coordinate: Coordinate,
}

export type Cell = AliveCell | DeadCell;

function isDead(cell: Cell): cell is DeadCell {
    return cell.kind === "Dead";
}

export function isAlive(cell: Cell): cell is AliveCell {
    return cell.kind === "Alive";
}

export function checkState(cell: Cell, neighbours: Cell[]): Cell {
    const aliveCount = neighbours.filter((cell) => isAlive(cell)).length;
    if (isDead(cell)) {
        const aliveCount = neighbours.filter((cell) => isAlive(cell)).length;
        if (aliveCount === 3) {
            return { ...cell, kind: "Alive" };
        }
        return cell;
    } else {
        if (aliveCount === 2 || aliveCount === 3) {
            return cell;
        }
        return { ...cell, kind: "Dead" };
    }
}


function getRandomInt(min: number, max: number): number {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min)) + min;
}

export function createRandom(coordinate: Coordinate): Cell {
    const num = getRandomInt(1, 100000);
    if (num % 2 === 0) {
        return { kind: "Alive", Coordinate: coordinate };
    }
    return { kind: "Dead", Coordinate: coordinate };
}