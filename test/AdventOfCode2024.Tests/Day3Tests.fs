module AdventOfCode2024.Tests.Day3Tests

open AdventOfCode2024.Solver
open Xunit

type Day3Test() =
    let demoData =
        [| "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))" |]

    let demoDataPart2 =
        [| "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))" |]

    [<Fact>]
    let ``Day 3 part 1`` () =
        let solution = Day3.solver1 demoData
        Assert.Equal(161, solution)


    [<Fact>]
    let ``Day 3 part 2`` () =
        let solution = Day3.solver2 demoDataPart2
        Assert.Equal(48,solution)
