module AdventOfCode2023.Tests.Day8Tests

open AdventOfCode2023.Solver
open Xunit

type Day8Test() =
    let demoData =
        [|
            "LLR"
            ""
            "AAA = (BBB, BBB)"
            "BBB = (AAA, ZZZ)"
            "ZZZ = (ZZZ, ZZZ)"
        |]

    let demoData2 =
        [|
            "LR"
            ""
            "11A = (11B, XXX)"
            "11B = (XXX, 11Z)"
            "11Z = (11B, XXX)"
            "22A = (22B, XXX)"
            "22B = (22C, 22C)"
            "22C = (22Z, 22Z)"
            "22Z = (22B, 22B)"
            "XXX = (XXX, XXX)"
        |]

    [<Fact>]
    let ``Day 8 part 1`` () =
        let solution = Day8.solver1 demoData
        Assert.Equal(6, solution)


    [<Fact>]
    let ``Day 8 part 2`` () =
        let solution = Day8.solver2 demoData2
        Assert.Equal(6L, solution)
