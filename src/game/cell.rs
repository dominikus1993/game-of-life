
#[derive(Clone, Copy, Debug, PartialEq, Eq)]
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

    fn is_valid_coordinate(&self, width: u32, height: u32) -> bool {
        self.x >= 0 && self.x < width as i32 && self.y >= 0 && self.y < height as i32
    }

    pub fn get_neighbours(&self, width: u32, height: u32) -> Vec<Coordinate> {
        let mut neighbours = [
            Coordinate::new(self.x - 1, self.y - 1),
            Coordinate::new(self.x, self.y - 1),
            Coordinate::new(self.x + 1, self.y - 1),
            Coordinate::new(self.x - 1, self.y),
            Coordinate::new(self.x + 1, self.y),
            Coordinate::new(self.x - 1, self.y + 1),
            Coordinate::new(self.x, self.y + 1),
            Coordinate::new(self.x + 1, self.y + 1)
        ];
        let mut result = vec![];
        for neighbour in neighbours.iter() {
            if neighbour.is_valid_coordinate(width, height) {
                result.push(*neighbour);
            }
        }
        result
    }
}

#[repr(u8)]
#[derive(Clone, Copy, Debug, PartialEq, Eq)]
pub enum Cell {
    Alive,
    Dead
}

impl Cell {
    pub fn is_live(&self) -> bool {
        *self == Cell::Alive
    }
}
