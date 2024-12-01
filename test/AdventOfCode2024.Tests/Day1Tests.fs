module AdventOfCode2024.Tests.Day1Tests

open AdventOfCode2024.Solver
open Xunit

type Day1Test() =
    let demoData = [| "3   4"; "4   3"; "2   5"; "1   3"; "3   9"; "3   3" |]

    [<Fact>]
    let ``Day 1 part 1`` () =
        let solution = Day1.solver1 demoData
        Assert.Equal(11, solution)


    [<Fact>]
    let ``Day 1 part 2`` () =
        let solution = Day1.solver2 demoData
        Assert.Equal(31, solution)
