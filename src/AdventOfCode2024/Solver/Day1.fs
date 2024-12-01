module AdventOfCode2024.Solver.Day1

open System
open Microsoft.FSharp.Core

let toTwoLists (lines: string array) =
    let list0 = lines |> Array.map (fun x -> (x.Split("   ").[0]) |> int)
    let list1 = lines |> Array.map (fun x -> (x.Split("   ").[1]) |> int)
    (list0, list1)

let solver1 (lines: string array) =
    let (list0, list1) = toTwoLists lines

    (0, list0 |> Array.sort, list1 |> Array.sort)
    |||> Array.fold2 (fun acc el0 el1 -> acc + Math.Abs(el1 - el0))

let solver2 (lines: string array) =
    let (list0, list1) = toTwoLists lines
    let counted = list1 |> Array.countBy id |> Map.ofArray

    (0, list0)
    ||> Array.fold (fun acc el ->
        acc
        + el * (match counted |> Map.tryFind el with
                 | Some x -> x
                 | _ -> 0))
