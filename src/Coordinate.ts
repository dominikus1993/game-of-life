export interface Coordinate {
}

export interface TwoDimensionalCoordinate extends Coordinate {
    readonly x: number;
    readonly y: number;
}

export function isTwoDimensionalCoordinate(coor: Coordinate): coor is TwoDimensionalCoordinate {
    return "x" in coor && "y" in coor;
}

export function isCorrect(coor: Coordinate): boolean {
    if(isTwoDimensionalCoordinate(coor)) {
        return coor.x >= 0 && coor.y >= 0;
    }
    throw new Error("Not a 2d coordinate");
}

export function* getNeighbours(coor: Coordinate) {
    if(isTwoDimensionalCoordinate(coor)) {
        const { x, y } = coor;
        for(let i = x - 1; i <= x + 1; i++) {
            for(let j = y - 1; j <= y + 1; j++) {
                if((i !== x || j !== y) && isCorrect({ x: i, y: j })) {
                    yield { x: i, y: j } as Coordinate;
                }
            }
        }
    }
}