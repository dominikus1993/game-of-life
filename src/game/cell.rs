
pub struct Coordinate {
    pub x: i32,
    pub y: i32
}

impl Coordinate {
    pub fn new(x: i32, y: i32) -> Coordinate {
        Coordinate {
            x,
            y
        }
    }
}

pub enum Cell {
    Alive(Coordinate),
    Dead(Coordinate)
}
