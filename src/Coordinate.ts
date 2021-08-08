export interface Coordinate {
    kind: string
}

export interface TwoDimensionalCoordinate extends Coordinate {
    kind: "2d"
    readonly x: number;
    readonly y: number;
}

function isTwoDimensionalCoordinate(coor: Coordinate): coor is TwoDimensionalCoordinate {
    return coor.kind === "2d";
}

export function isCorrect(coor: Coordinate): boolean {
    if(isTwoDimensionalCoordinate(coor)) {
        return coor.x >= 0 && coor.y >= 0;
    }
    throw new Error("Not a 2d coordinate");
}