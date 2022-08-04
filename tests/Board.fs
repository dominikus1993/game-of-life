module BoardTests

open Fable.Mocha
open GameOfLife


let coordinates =
    testList "Generate Coordinates tests" [
        test "0 index should be (x: 0, y: 0)" {
            let board = Board.newBoard 10 10
            let subject =  board |> Board.generateCoordinate 0
            Expect.equal subject (TwoDimensionCoordinate(0, 0)) "dead cell"
        }
        test "1 index should be (x: 1, y: 0)" {
            let board = Board.newBoard 10 10
            let subject =  board |> Board.generateCoordinate 2
            Expect.equal subject (TwoDimensionCoordinate(2, 0)) "dead cell"
        }
        test "10 index should be (x: 0, y: 1)" {
            let board = Board.newBoard 10 10
            let subject =  board |> Board.generateCoordinate 10
            Expect.equal subject (TwoDimensionCoordinate(0, 1)) "dead cell"
        }
        test "21 index should be (x: 1, y: 2)" {
            let board = Board.newBoard 10 10
            let subject =  board |> Board.generateCoordinate 21
            Expect.equal subject (TwoDimensionCoordinate(1, 2)) "dead cell"
        }
    ]

let coordinatesInBoard =
    testList "Generate Coordinates tests" [
        test "(x: 0, y: 0) is in board" {
            let board = Board.newBoard 10 10
            let subject =  board |> Board.isInBoard(TwoDimensionCoordinate(0, 0))
            Expect.isTrue subject "dead cell"
        }
        test "(x: -1, y: 0) is not in board" {
            let board = Board.newBoard 10 10
            let subject =  board |> Board.isInBoard(TwoDimensionCoordinate(-1, 0))
            Expect.isFalse subject "dead cell"
        }
        test "(x: 0, y: -1) is  not in board" {
            let board = Board.newBoard 10 10
            let subject =  board |> Board.isInBoard(TwoDimensionCoordinate(0, -1))
            Expect.isFalse subject "dead cell"
        }
        test "(x: 11, y: 0) is not in board" {
            let board = Board.newBoard 10 10
            let subject =  board |> Board.isInBoard(TwoDimensionCoordinate(11, 0))
            Expect.isFalse subject "dead cell"
        }
        test "(x: 0, y: 11) is not in board" {
            let board = Board.newBoard 10 10
            let subject =  board |> Board.isInBoard(TwoDimensionCoordinate(0, 11))
            Expect.isFalse subject "dead cell"
        }
    ]

let board =
    testList "Board tests" [
        coordinates
        coordinatesInBoard
    ]