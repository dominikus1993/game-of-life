import React, { useEffect, useState } from "react";
import logo from './logo.svg';
import './App.css';
import { Board } from "./Board";
import { Cell, isAlive } from "./Cell";


function Game() {
    const [count, setCount] = useState(0);
    const [board, _] = useState(Board.create(10, 10));
    return (
        <div className="App">
            <h1>xD</h1>
            <h2>{JSON.stringify(board.getCells())}</h2>
        </div>
    );
}

export default Game;
