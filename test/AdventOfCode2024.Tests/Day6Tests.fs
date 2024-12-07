module AdventOfCode2024.Tests.Day6Tests

open AdventOfCode2024.Solver
open Xunit

type Day6Test() =
    let demoData =
        [|
            "....#....."
            ".........#"
            ".........."
            "..#......."
            ".......#.."
            ".........."
            ".#..^....."
            "........#."
            "#........."
            "......#..."
        |]

    [<Fact>]
    let ``Day 6 part 1`` () =
        let solution = Day6.solver1 demoData
        Assert.Equal(41, solution)


    [<Fact>]
    let ``Day 6 part 2`` () =
        let solution = Day6.solver2 demoData
        Assert.Equal("", solution)
