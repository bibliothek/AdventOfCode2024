module AdventOfCode2023.Tests.Day1Tests

open AdventOfCode2023.Solver
open Xunit

type Day1Tests() =
    let demoData =
        [|
            "1abc2"
            "pqr3stu8vwx"
            "a1b2c3d4e5f"
            "treb7uchet"
           |]

    let demoData2 =
        [|
            "two1nine"
            "eightwothree"
            "abcone2threexyz"
            "xtwone3four"
            "4nineeightseven2"
            "zoneight234"
            "7pqrstsixteen"
        |]
    [<Fact>]
    let ``Day 1 part 1`` () =
        let solution = Day1.solver1 demoData
        Assert.Equal(142, solution)

    [<Fact>]
    let ``Day 1 part 2`` () =
        let solution = Day1.solver2 demoData2
        Assert.Equal(281, solution)
