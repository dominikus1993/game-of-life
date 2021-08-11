import React, { useEffect, useRef, useState } from "react";
import './App.css';
import { Board, createBoard, next } from "./Board";
import { Cell, isAlive } from "./Cell";

function AliveCell(props: { cell: Cell, key: string }) {
    return (<td style={{ backgroundColor: "white" }} key={props.key}>A</td>)
}

function DeadCell(props: { cell: Cell, key: string }) {
    return (<td style={{ backgroundColor: "black" }} key={props.key}>D</td>)
}

function CellRow(props: { rowId: number; row: Cell[] }) {
    const cells = props.row.map((cell, index) => {
        const key = `${props.rowId}-${index}`;
        return (isAlive(cell) ? <AliveCell cell={cell} key={key} /> : <DeadCell cell={cell} key={key} />);
    });
    return (
        <tr key={props.rowId}>
            {cells}
        </tr>
    )
}

function CellsComponent(props: { cells: Cell[][] }) {
    const rows = props.cells.map((row, index) => <CellRow rowId={index} row={row} />)
    return (<table width="100%">
        <tbody key={"cells"}>
            {rows}
        </tbody>
    </table>)
}

function Game() {
    const [board, setBoard] = useState(createBoard({ rows: 10, columns: 10 }));
    const [count, setCount] = useState(0);
    const refCount = useRef(count);
    setInterval(() => {
        setCount(refCount.current + 1);
        setBoard(next(board))
    }, 1000)

    return (
        <div className="App">
            <h1>{count}</h1>
            <CellsComponent cells={board.Cells} />
        </div>
    );
}

export default Game;
