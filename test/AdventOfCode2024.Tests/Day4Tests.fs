module AdventOfCode2024.Tests.Day4Tests

open AdventOfCode2024.Solver
open Xunit

type Day4Test() =
    let demoData =
        [|
            "MMMSXXMASM"
            "MSAMXMSMSA"
            "AMXSXMAAMM"
            "MSAMASMSMX"
            "XMASAMXAMM"
            "XXAMMXXAMA"
            "SMSMSASXSS"
            "SAXAMASAAA"
            "MAMMMXMMMM"
            "MXMXAXMASX"
        |]


    [<Fact>]
    let ``Day 4 part 1`` () =
        let solution = Day4.solver1 demoData
        Assert.Equal(18, solution)


    [<Fact>]
    let ``Day 4 part 2`` () =
        let solution = Day4.solver2 demoData
        Assert.Equal(9, solution)
