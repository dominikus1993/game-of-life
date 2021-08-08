import { isCorrect, getNeighbours } from './Coordinate';

test('test correct 2d point', () => {
    const coor = { x: 0, y: 0 };
    const subject = isCorrect(coor);
    expect(subject).toBe(true);
});

test('test incorrect 2d point', () => {
    const coor = { x: -1, y: 0 };
    const subject = isCorrect(coor);
    expect(subject).toBe(false);
});


test('test get neighbours for 2d point', () => {
    const coor = { x: 0, y: 0 };
    const subject = [...getNeighbours(coor)];
    expect(subject.length).toBe(3);
});

