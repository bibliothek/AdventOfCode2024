module AdventOfCode2023.Solver.Day9

let rec getDiff (numbers: int array) (nextValueSelector) : int =
    if numbers |> Array.forall (fun x -> x = 0) then
        0
    else
        let nextRow = numbers |> Array.pairwise |> Array.map (fun (x, y) -> y - x)
        let diff = getDiff nextRow nextValueSelector
        nextValueSelector (numbers, diff)

let getLineValue nextValueSelector (line: string) : int =
    let numbers = line.Split() |> Array.map int
    getDiff numbers nextValueSelector

let solver1 (lines: string array) =
    lines
    |> Array.map (getLineValue (fun (x, diff) -> (x |> Array.last) + diff))
    |> Array.sum

let solver2 (lines: string array) =
    lines
    |> Array.map (getLineValue (fun (x, diff) -> (x |> Array.head) - diff))
    |> Array.sum
