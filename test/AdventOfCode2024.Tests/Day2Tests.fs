module AdventOfCode2024.Tests.Day2Tests

open AdventOfCode2024.Solver
open Xunit

type Day2Test() =
    let demoData =
        [|
            "7 6 4 2 1"
            "1 2 7 8 9"
            "9 7 6 2 1"
            "1 3 2 4 5"
            "8 6 4 4 1"
            "1 3 6 7 9"
        |]

    [<Fact>]
    let ``Day 2 part 1`` () =
        let solution = Day2.solver1 demoData
        Assert.Equal(2, solution)


    [<Fact>]
    let ``Day 2 part 2`` () =
        let solution = Day2.solver2 demoData
        Assert.Equal(4, solution)
