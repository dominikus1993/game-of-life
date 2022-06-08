use super::cell::{Cell, Coordinate};

pub struct Universe {
    width: u32,
    height: u32,
    cells: Vec<Vec<Cell>>
}

impl Universe {
    pub fn get_live_neighbour_count(&self, coor: Coordinate) -> u8 {
        let mut count = 0;
        let mut neighbours = vec![
            Coordinate::new(coor.x - 1, coor.y - 1),
            Coordinate::new(coor.x, coor.y - 1),
            Coordinate::new(coor.x + 1, coor.y - 1),
            Coordinate::new(coor.x - 1, coor.y),
            Coordinate::new(coor.x + 1, coor.y),
            Coordinate::new(coor.x - 1, coor.y + 1),
            Coordinate::new(coor.x, coor.y + 1),
            Coordinate::new(coor.x + 1, coor.y + 1)
        ];
        for neighbour in neighbours {
            if self.is_valid_coordinate(neighbour) {
                self.cells.ind
                if self.cells[neighbour.x as usize][neighbour.y as usize].is_live() {
                    count += 1;
                }
            }
        }
        count
    }   
}