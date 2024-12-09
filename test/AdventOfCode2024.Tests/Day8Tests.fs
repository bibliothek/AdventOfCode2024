module AdventOfCode2024.Tests.Day8Tests

open AdventOfCode2024.Solver
open Xunit

type Day8Test() =
    let demoData =
        [|
            "............"
            "........0..."
            ".....0......"
            ".......0...."
            "....0......."
            "......A....."
            "............"
            "............"
            "........A..."
            ".........A.."
            "............"
            "............"
        |]

    [<Fact>]
    let ``Day 8 part 1`` () =
        let solution = Day8.solver1 demoData
        Assert.Equal(14, solution)


    [<Fact>]
    let ``Day 8 part 2`` () =
        let solution = Day8.solver2 demoData
        Assert.Equal("", solution)
