import { isCorrect } from './Coordinate';

test('test correct 2d point', () => {
  const coor = { kind: "2d", x: 0, y: 0 };
  const subject = isCorrect(coor);
  expect(subject).toBe(true);
});
