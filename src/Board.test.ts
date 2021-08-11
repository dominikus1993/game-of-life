import { Board, createBoard, next } from "./Board";


test('test board', () => {
    const rows = 10;
    const columns = 5;
    const board = createBoard({ rows, columns });
    const subject =  board.Cells;
    expect(subject).toHaveLength(rows);
    expect(subject[0]).toHaveLength(columns);
});


test('test board and next function', () => {
    const rows = 10;
    const columns = 5;
    const board = createBoard({ rows, columns });
    const newBoard = next(board);
    const subject = newBoard.Cells;
    expect(subject).toHaveLength(rows);
    expect(subject[0]).toHaveLength(columns);
});