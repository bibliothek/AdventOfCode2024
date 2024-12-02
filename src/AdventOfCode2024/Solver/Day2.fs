module AdventOfCode2024.Solver.Day2

open System
open Microsoft.FSharp.Core

let parseLine (line: string) = line.Split() |> Array.map int

let isPairSafe (pair: int * int) (ascending: bool) =
    let diff =
        if ascending then
            (snd pair) - (fst pair)
        else
            (fst pair) - (snd pair)

    match diff with
    | diff when diff >= 1 && diff <= 3 -> true
    | _ -> false

let isLineSafe (line: int array) =
    let isAscending = (line[1] - line[0]) > 0

    let find =
        line
        |> Array.pairwise
        |> Array.tryFind (fun pair -> isPairSafe pair isAscending |> not)

    match find with
    | Some _ -> false
    | _ -> true

let arrayExceptIndex index array =
    (Array.append (array |> Array.take (index)) (array |> Array.skip (index + 1)))

let isLineSafeWithTolerance (line: int array) =
    let isAscending = (line[1] - line[0]) > 0

    let resultPairs =
        line
        |> Array.mapi (fun i el ->
            if i + 1 = line.Length then
                true
            else
                isPairSafe (el, line[i + 1]) isAscending |> not)

    let find = resultPairs |> Array.tryFindIndex id

    match find with
    | None -> true
    | Some x ->
        let zero = isLineSafe (arrayExceptIndex x line)

        let minus1 =
            if x - 1 < 0 then
                false
            else
                isLineSafe (arrayExceptIndex (x - 1) line)

        let plus1 =
            if x + 1 = line.Length then
                false
            else
                isLineSafe (arrayExceptIndex (x + 1) line)

        zero || minus1 || plus1

let solver1 (lines: string array) =
    lines |> Array.map (parseLine >> isLineSafe) |> Array.filter id |> Array.length


let solver2 (lines: string array) =
    lines
    |> Array.map (parseLine >> isLineSafeWithTolerance)
    |> Array.filter id
    |> Array.length
