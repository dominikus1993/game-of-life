import { Board } from "./Board";


test('test board', () => {
    const rows = 10;
    const cols = 5;
    const board = Board.create(rows, cols);
    const subject =  board.getCells();
    expect(subject).toHaveLength(rows);
    expect(subject[0]).toHaveLength(cols);
});


test('test board and next function', () => {
    const rows = 10;
    const cols = 5;
    const board = Board.create(rows, cols);
    board.next();
    const subject = board.getCells();
    expect(subject).toHaveLength(rows);
    expect(subject[0]).toHaveLength(cols);
});