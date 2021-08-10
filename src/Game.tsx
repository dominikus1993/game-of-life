import React, { useState } from "react";
import './App.css';
import { Board } from "./Board";
import { Cell, isAlive } from "./Cell";

function CellRow(props: { rowId: number; row: Cell[] }) {
    const cells = props.row.map((cell, index) => {
        const key = `${props.rowId}-${index}`;
        return (<td color="red" key={key}>{isAlive(cell) ? "A" : "D"}</td>)
    })
    return (
        <tr key={props.rowId}>
            {cells}
        </tr>
    )
}

function CellsComponent(props: { cells: Cell[][] }) {
    const rows = props.cells.map((row, index) => <CellRow rowId={index} row={row} />)
    return (<table width="100%">
        <tbody>
            {rows}
        </tbody>
    </table>)
}

function Game() {
    const [count, setCount] = useState(0);
    const [board, _] = useState(Board.create(100, 100));
    return (
        <div className="App">
            <CellsComponent cells={board.getCells()} />
        </div>
    );
}

export default Game;
