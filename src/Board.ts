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

export class Board {
    #cells: Cell[][];
    readonly #size: Size;
    readonly #memonize;

    private constructor(size: Size, cell: Cell[][]) {
        this.#cells = cell;
        this.#size = Object.freeze(size);
        this.#memonize = memonize<Coordinate, Coordinate[]>();
    }

    private getCell(coor: Coordinate): Cell {
        if (isTwoDimensionalCoordinate(coor)) {
            const { x, y } = coor;
            return this.#cells[y][x];
        }
        throw new Error(`Coordinate ${coor} is not two dimensional`);
    }

    getCells(): Cell[][] {
        return this.#cells;
    }

    getSize(): Size {
        return this.#size;
    }

    next() {
        const newCells = [];
        for (const row of this.#cells) {
            const newRow = [];
            for (const column of row) {
                const neighbours = this.#memonize(column.Coordinate, (coor) => [...getNeighbours(coor)].filter(x => isInSize(x, this.#size)));
                if (Array.isArray(neighbours)) {
                    const n = neighbours.map(coor => this.getCell(coor));
                    newRow.push(column.checkState(n));
                }
            }
            newCells.push(newRow);
        }
        this.#cells = newCells;
    }

    static create(rows: number, columns: number): Board {
        function* generate(size: Size) {
            for (let i = 0; i < size.rows; i++) {
                const row = [];
                for (let j = 0; j < size.columns; j++) {
                    row.push(createRandom({ x: j, y: i }));
                }
                yield row;
            }
        }
        const size: Size = { rows, columns };
        return new Board(size, [...generate(size)]);
    }
}