module AdventOfCode2023.Tests.Day7Tests

open AdventOfCode2023.Solver
open Xunit

type Day7Test() =
    let demoData =
        [|
            "32T3K 765"
            "T55J5 684"
            "KK677 28"
            "KTJJT 220"
            "QQQJA 483"
        |]

    [<Fact>]
    let ``Day 7 part 1`` () =
        let solution = Day7.solver1 demoData
        Assert.Equal(6440, solution)


    [<Fact>]
    let ``Day 7 part 2`` () =
        let solution = Day7.solver2 demoData
        Assert.Equal(5905, solution)
