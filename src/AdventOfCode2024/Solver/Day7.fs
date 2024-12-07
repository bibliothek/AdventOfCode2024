module AdventOfCode2024.Solver.Day7

let calc1 ((acc: int64 Set), (expectedResult: int64)) (number: int64) =
    if acc.IsEmpty then
        acc, expectedResult
    else
        let newResults =
            acc
            |> Set.map (fun el -> el + number)
            |> Set.union (acc |> Set.map (fun el -> el * number))

        (newResults |> Set.filter (fun el -> el <= expectedResult)), expectedResult

let calc2 ((acc: int64 Set), (expectedResult: int64)) (number: int64) =
    if acc.IsEmpty then
        acc, expectedResult
    else
        let newResults =
            acc
            |> Set.map (fun el -> el + number)
            |> Set.union (acc |> Set.map (fun el -> el * number))
            |> Set.union (acc |> Set.map (fun el -> el.ToString() + number.ToString() |> int64))

        (newResults |> Set.filter (fun el -> el <= expectedResult)), expectedResult

let evalLine calc (line: string) =
    let split = line.Split ':'
    let expectedResult = split[0] |> int64
    let numbers = split[1].Substring(1).Split() |> Array.map int64 

    let results =
        numbers
        |> Array.skip 1
        |> Array.fold calc (Set.empty.Add(numbers[0]), expectedResult)
        |> fst

    if results |> Set.contains expectedResult then
        expectedResult
    else
        0

let solver1 (lines: string array) =
    lines |> Array.map (evalLine calc1) |> Array.sum

let solver2 (lines: string array) =
    lines |> Array.map (evalLine calc2) |> Array.sum
