module AdventOfCode2023.Tests.Day11Tests

open AdventOfCode2023.Solver
open Xunit

type Day11Test() =
    let demoData =
        [|
            "...#......"
            ".......#.."
            "#........."
            ".........."
            "......#..."
            ".#........"
            ".........#"
            ".........."
            ".......#.."
            "#...#....."
        |]

    [<Fact>]
    let ``Day 11 part 1`` () =
        let solution = Day11.solver1 demoData
        Assert.Equal(374L, solution)


    [<Fact>]
    let ``Day 11 part 2`` () =
        let solution = Day11.solver2 demoData
        Assert.Equal(82000210L, solution)
