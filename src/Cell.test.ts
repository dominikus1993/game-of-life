import { AliveCell, Cell, checkState, createAlive, createDead, DeadCell, isAlive } from './Cell';
import { isCorrect } from './Coordinate';
import each from 'jest-each';

function createCells(alive: number, dead: number): Cell[] {
    const cells: Cell[] = [];
    for (let i = 0; i < alive; i++) {
        cells.push(createAlive({ x: i, y: i }));
    }
    for (let i = 0; i < dead; i++) {
        cells.push(createDead({ x: i, y: i }));
    }
    return cells;
}

describe("test check should be alive", () => {
    each([
        [1, false],
        [2, true],
        [3, true],
        [4, false],
    ]).it("when cell has %d alive nieghbours then should be alive: %s", (alive, shouldBeAlive) => {
        const neigh = createCells(alive, 8 - alive);
        const cell = alive({ x: 0, y: 0 });
        const subject = cell.checkState(neigh);
        expect(isAlive(subject)).toBe(shouldBeAlive);
    })
});

describe("test check should be dead", () => {
    each([
        [1, false],
        [2, false],
        [3, true],
        [4, false],
    ]).it("when cell has %d alive nieghbours then should be dead: %s", (alive, shouldBeAlive) => {
        const neigh = createCells(alive, 8 - alive);
        const cell = createDead({ x: 0, y: 0 });
        const subject = checkState(cell, neigh);
        expect(isAlive(subject)).toBe(shouldBeAlive);
    })
});