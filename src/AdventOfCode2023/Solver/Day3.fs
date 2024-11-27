module AdventOfCode2023.Solver.Day3

open System.Text.RegularExpressions

type Part = { number: int; adjacentSymbol: bool }

type Gear =
    { number: int
      adjacentGearPos: Option<int * int> }

let containsSymbol (lines: string array) (x: int) (y: int) (regex: string) =
    if x < 0 || y < 0 || x >= lines.[0].Length || y >= lines.Length then
        false
    else
        let char = lines.[y].[x]
        Regex.IsMatch(char.ToString(), regex)

let getSearchArea (x1: int) (x2: int) (y: int) =
    seq {
        for i in y - 1 .. y + 1 do
            for j in x1 - 1 .. x2 + 1 do
                (j, i)
    }

let hasAdjacentSymbol (lines: string array) (x1: int) (x2: int) (y: int) =
    getSearchArea x1 x2 y
    |> Seq.exists (fun (j, i) -> containsSymbol lines j i "[^\.\d]")

let getGearPos (lines: string array) (x1: int) (x2: int) (y: int) =
    getSearchArea x1 x2 y
    |> Seq.tryFind (fun (j, i) -> containsSymbol lines j i "\*")

let partRegex = "\d+"

let getParts (lines: string array) =
    seq {
        let mutable y = -1
        for line in lines do
            y <- y + 1
            for m in Regex.Matches(line, partRegex) do
                { number = (m.Value |> int)
                  adjacentSymbol = hasAdjacentSymbol lines m.Index (m.Index + m.Value.Length - 1) y }
    }

let getGears (lines: string array) =
    seq {
        let mutable y = -1
        for line in lines do
            y <- y + 1
            for m in Regex.Matches(line, partRegex) do
                { number = (m.Value |> int)
                  adjacentGearPos = getGearPos lines m.Index (m.Index + m.Value.Length - 1) y }
    }

let solver1 (lines: string array) =
    lines
    |> getParts
    |> Seq.filter (fun x -> x.adjacentSymbol)
    |> Seq.map (fun x -> x.number)
    |> Seq.sum

let solver2 (lines: string array) =
    lines
    |> getGears
    |> Seq.filter (fun x -> x.adjacentGearPos.IsSome)
    |> Seq.toList
    |> List.groupBy (fun x -> x.adjacentGearPos.Value)
    |> List.filter (fun ((_, _), g) -> g.Length = 2)
    |> List.map (fun ((_, _), g) -> g.Head.number * g.Tail.Head.number)
    |> List.sum
