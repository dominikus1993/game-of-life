module Tests

open Fable.Mocha

let cell =
    testList "Cell tests" [
        test "name works" {
            Expect.equal (1 + 1) 2 "plus"
        }
    ]

Mocha.runTests arithmeticTests