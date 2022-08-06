module BoardTests

open Fable.Mocha
open GameOfLife



let coordinatesInBoard =
    testList "Generate Coordinates tests" [
        test "(x: 0, y: 0) is in board" {
            let board = Board.empty 10 10
            let subject =  board |> Board.isCorrectCoordinate(TwoDimensionCoordinate(0, 0))
            Expect.isTrue subject "dead cell"
        }
        test "(x: -1, y: 0) is not in board" {
            let board = Board.empty 10 10
            let subject =  board |> Board.isCorrectCoordinate(TwoDimensionCoordinate(-1, 0))
            Expect.isFalse subject "dead cell"
        }
        test "(x: 0, y: -1) is  not in board" {
            let board = Board.empty 10 10
            let subject =  board |> Board.isCorrectCoordinate(TwoDimensionCoordinate(0, -1))
            Expect.isFalse subject "dead cell"
        }
        test "(x: 11, y: 0) is not in board" {
            let board = Board.empty 10 10
            let subject =  board |> Board.isCorrectCoordinate(TwoDimensionCoordinate(11, 0))
            Expect.isFalse subject "dead cell"
        }
        test "(x: 0, y: 11) is not in board" {
            let board = Board.empty 10 10
            let subject =  board |> Board.isCorrectCoordinate(TwoDimensionCoordinate(0, 11))
            Expect.isFalse subject "dead cell"
        }
        test "(x: 10, y: 0) is in board" {
            let board = Board.empty 10 10
            let subject =  board |> Board.isCorrectCoordinate(TwoDimensionCoordinate(10, 0))
            Expect.isTrue subject "dead cell"
        }
        test "(x: 0, y: 10) is in board" {
            let board = Board.empty 10 10
            let subject =  board |> Board.isCorrectCoordinate(TwoDimensionCoordinate(0, 10))
            Expect.isTrue subject "dead cell"
        }
    ]

let isInBoard =
    testList "IsInBoard" [
        test "(x: 0, y: 0) is in board" {
            let subject = Board.empty 10 10 |> Board.isCorrectCoordinate(TwoDimensionCoordinate(0, 0))
            Expect.isTrue subject "should have 3 neighbours"
        }
        test "(x: -1, y: 0) is not in board" {
            let subject = Board.empty 10 10 |> Board.isCorrectCoordinate(TwoDimensionCoordinate(-1, 0))
            Expect.isFalse subject "should have 3 neighbours"
        }
        test "(x: 0, y: -1) is not in board" {
            let subject = Board.empty 10 10 |> Board.isCorrectCoordinate(TwoDimensionCoordinate(0, -1))
            Expect.isFalse subject "should have 3 neighbours"
        }
        test "(x: 11, y: 6) is not in board" {
            let subject = Board.empty 10 10 |> Board.isCorrectCoordinate(TwoDimensionCoordinate(11, 6))
            Expect.isFalse subject "should have 3 neighbours"
        }
    ]


let neighbours =
    testList "Get Neighbours" [
        test "(x: 0, y: 0)" {
            let subject = Board.empty 10 10 |> Board.getNeighbours({ Coordinates = TwoDimensionCoordinate(0, 0); Cell = Dead }) |> Seq.toList
            Expect.hasLength subject 3 "should have 3 neighbours"
        }
    ]

let newBoard =
    testList "New Board" [
        test "(height: 10, width: 10)" {
            let subject = Board.newBoard 10 10

            Expect.equal subject.Width 10 "should have 3 neighbours"
            Expect.equal subject.Height 10 "should have 3 neighbours"
            Expect.equal subject.Cells.Length 10 "should have 3 neighbours"
            Expect.equal (subject.Cells.[0].Length) 10 "should have 3 neighbours"

            for (column, cells) in subject.Cells |> Array.indexed do
                for (row, cell) in cells |> Array.indexed do
                    let ({Coordinates = TwoDimensionCoordinate(x, y)}) = cell
                    Expect.equal y column "column should equal y"
                    Expect.equal x row "row should equal x"
        }
    ]

let board =
    testList "Board tests" [
        isInBoard
        coordinatesInBoard
        neighbours
        newBoard
    ]