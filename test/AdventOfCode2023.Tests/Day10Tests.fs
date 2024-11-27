module AdventOfCode2023.Tests.Day10Tests

open AdventOfCode2023.Solver
open Xunit

type Day10Test() =
    let demoData =
        [|
            "..F7."
            ".FJ|."
            "SJ.L7"
            "|F--J"
            "LJ..."
        |]

    let demoData2 =
        [|
            "..........."
            ".S-------7."
            ".|F-----7|."
            ".||.....||."
            ".||.....||."
            ".|L-7.F-J|."
            ".|..|.|..|."
            ".L--J.L--J."
            "..........."
        |]
    let demoData3 =
        [|
            ".F----7F7F7F7F-7...."
            ".|F--7||||||||FJ...."
            ".||.FJ||||||||L7...."
            "FJL7L7LJLJ||LJ.L-7.."
            "L--J.L7...LJS7F-7L7."
            "....F-J..F7FJ|L7L7L7"
            "....L7.F7||L7|.L7L7|"
            ".....|FJLJ|FJ|F7|.LJ"
            "....FJL-7.||.||||..."
            "....L---J.LJ.LJLJ..."
        |]

    [<Fact>]
    let ``Day 10 part 1`` () =
        let solution = Day10.solver1 demoData
        Assert.Equal(8, solution)


    [<Fact>]
    let ``Day 10 part 2 small`` () =
        let solution = Day10.solver2 demoData2
        Assert.Equal(4, solution)

    [<Fact>]
    let ``Day 10 part 2 larger`` () =
        let solution = Day10.solver2 demoData3
        Assert.Equal(8, solution)
