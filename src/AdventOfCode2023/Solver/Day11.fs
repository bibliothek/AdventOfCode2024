module AdventOfCode2023.Solver.Day11

open System
open AdventOfCodeHelpers.Array2DHelper

let adjustPoint emptyRowIndices emptyColumnIndices (factor:int64) (point:Pos64) : Pos64 =
    let newX = (point.x :: emptyColumnIndices |> List.sort |> ((List.findIndex ((=) point.x))) |> int64) * (factor - 1L) + (point.x |> int64)
    let newY = (point.y :: emptyRowIndices |> List.sort |> ((List.findIndex ((=) point.y))) |> int64) * (factor - 1L) + (point.y |> int64)
    {x = newX; y = newY}

let calculatePath ((a:Pos64), (b:Pos64)) =
    let distance = Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y)
    (distance, (a,b))

let rec pairs l = seq {
    match l with
    | h::t -> for e in t do yield h, e
              yield! pairs t
    | _ -> () }

let getPoints lines =
    let map = buildFromLines lines

    let emptyRowIndices =
        lines
        |> Array.indexed
        |> Array.filter (fun (_, row) -> row.ToCharArray() |> Array.forall ((=) '.'))
        |> Array.map fst
        |> Array.map int64
        |> List.ofArray

    let emptyColumnIndices =
        Seq.init (lines[0].Length - 1) (id)
        |> Seq.filter (fun i -> map[i, *] |> Array.forall ((=) '.'))
        |> Seq.map int64
        |> List.ofSeq

    let points = map |> toSeqWithPoint |> Seq.filter (snd >> (=) '#') |> Seq.map fst |> Array.ofSeq
    (points, emptyRowIndices, emptyColumnIndices)

let solve lines factor =
    let (points, emptyRowIndices, emptyColumnIndices) = getPoints lines

    let adjustedPoints = points |> Array.map (adjustPoint emptyRowIndices emptyColumnIndices factor) |> List.ofArray

    let allPairs = adjustedPoints |> pairs |> List.ofSeq

    let distances = allPairs |> List.map calculatePath
    distances |> List.map fst |> List.sum

let solver1 (lines: string array) =
    solve lines 2L

let solver2 (lines: string array) =
    solve lines 1000000L
