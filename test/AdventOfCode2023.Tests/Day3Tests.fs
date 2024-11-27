module AdventOfCode2023.Tests.Day3Tests

open AdventOfCode2023.Solver
open Xunit

type Day3Test() =
    let demoData =
        [|
            "467..114.."
            "...*......"
            "..35..633."
            "......#..."
            "617*......"
            ".....+.58."
            "..592....."
            "......755."
            "...$.*...."
            ".664.598.."
        |]

    [<Fact>]
    let ``Day 3 part 1`` () =
        let solution = Day3.solver1 demoData
        Assert.Equal(4361, solution)


    [<Fact>]
    let ``Day 3 part 2`` () =
        let solution = Day3.solver2 demoData
        Assert.Equal(467835, solution)
