module AdventOfCode2024.Tests.Day7Tests

open AdventOfCode2024.Solver
open Xunit

type Day7Test() =
    let demoData =
        [|
            "190: 10 19"
            "3267: 81 40 27"
            "83: 17 5"
            "156: 15 6"
            "7290: 6 8 6 15"
            "161011: 16 10 13"
            "192: 17 8 14"
            "21037: 9 7 18 13"
            "292: 11 6 16 20" |]
        
    [<Fact>]
    let ``Day 7 part 1`` () =
        let solution = Day7.solver1 demoData
        Assert.Equal(3749L, solution)


    [<Fact>]
    let ``Day 7 part 2`` () =
        let solution = Day7.solver2 demoData
        Assert.Equal(11387L, solution)
