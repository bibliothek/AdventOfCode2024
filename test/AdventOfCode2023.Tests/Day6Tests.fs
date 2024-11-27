module AdventOfCode2023.Tests.Day6Tests

open AdventOfCode2023.Solver
open Xunit

type Day6Test() =
    let demoData =
        [|
            "Time:      7  15   30"
            "Distance:  9  40  200"
        |]

    [<Fact>]
    let ``Day 6 part 1`` () =
        let solution = Day6.solver1 demoData
        Assert.Equal(288L, solution)


    [<Fact>]
    let ``Day 6 part 2`` () =
        let solution = Day6.solver2 demoData
        Assert.Equal(71503L, solution)
