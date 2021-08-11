import { Cell, createRandom } from "./Cell";
import { Coordinate, getNeighbours, isTwoDimensionalCoordinate } from "./Coordinate";

type Size = { rows: number; columns: number };

function isInSize(coordinate: Coordinate, size: Size): boolean {
    if (isTwoDimensionalCoordinate(coordinate)) {
        return coordinate.x < size.columns && coordinate.y < size.rows;
    }
    return false;
}

function memonize<TParam, TRes>() {
    const cache = new Map<string, TRes>();
    return (p: TParam, func: (p: TParam) => TRes) => {
        const key = JSON.stringify(p);
        if (!cache.has(key)) {
            cache.set(key, func(p));
        }
        return cache.get(key);
    };
}

function getCell(board: Board, coor: Coordinate): Cell {
    if (isTwoDimensionalCoordinate(coor)) {
        const { x, y } = coor;
        return board.Cells[y][x];
    }
    throw new Error(`Coordinate ${coor} is not two dimensional`);
}

const memo = memonize<Coordinate, Coordinate[]>();

export interface Board { 
    readonly Cells: Cell[][];
    readonly Size: Size;
}

export function createBoard(size: Size): Board {
    function* generate(size: Size) {
        for (let i = 0; i < size.rows; i++) {
            const row = [];
            for (let j = 0; j < size.columns; j++) {
                row.push(createRandom({ x: j, y: i }));
            }
            yield row;
        }
    }
    return { Size: size, Cells: [...generate(size)]} ;
}

export function next(board: Board): Board {
    const newCells = [];
    for (const row of board.Cells) {
        const newRow = [];
        for (const column of row) {
            const neighbours = memo(column.Coordinate, (coor) => [...getNeighbours(coor)].filter(x => isInSize(x, board.Size)));
            if (Array.isArray(neighbours)) {
                const n = neighbours.map(coor => getCell(board, coor));
                newRow.push(column.checkState(n));
            }
        }
        newCells.push(newRow);
    }
    return { ...board, Cells: newCells };
}