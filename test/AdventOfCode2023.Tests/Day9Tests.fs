module AdventOfCode2023.Tests.Day9Tests

open AdventOfCode2023.Solver
open Xunit

type Day9Test() =
    let demoData =
        [|
            "0 3 6 9 12 15"
            "1 3 6 10 15 21"
            "10 13 16 21 30 45"
        |]

    [<Fact>]
    let ``Day 9 part 1`` () =
        let solution = Day9.solver1 demoData
        Assert.Equal(114, solution)


    [<Fact>]
    let ``Day 9 part 2`` () =
        let solution = Day9.solver2 demoData
        Assert.Equal(2, solution)
