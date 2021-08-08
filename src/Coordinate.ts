export interface Coordinate {
}

export interface TwoDimensionalCoordinate extends Coordinate {
    readonly x: number;
    readonly y: number;
}

function isTwoDimensionalCoordinate(coor: Coordinate): coor is TwoDimensionalCoordinate {
    return "x" in coor && "y" in coor;
}

export function isCorrect(coor: Coordinate): boolean {
    if(isTwoDimensionalCoordinate(coor)) {
        return coor.x >= 0 && coor.y >= 0;
    }
    throw new Error("Not a 2d coordinate");
}