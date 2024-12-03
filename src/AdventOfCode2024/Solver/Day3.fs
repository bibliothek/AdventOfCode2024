module AdventOfCode2024.Solver.Day3

open System
open System.Text
open System.Text.RegularExpressions

let regexPattern = "mul\(\d+,\d+\)"

let processItem (el: Match) =
    let indexOfComma = el.Value.IndexOf(',')
    let first = el.Value.Substring(4, indexOfComma - 4) |> int

    let second =
        el.Value.Substring(indexOfComma + 1, el.Value.Length - 2 - indexOfComma) |> int

    first * second

let processLine (line: string) =
    let matches = Regex.Matches(line, regexPattern)
    matches |> Seq.map processItem |> Seq.sum

let prepareLineWithInstructions (line: string) =
    let sb = StringBuilder()
    let splits = line.Split "don't()"
    sb.Append(splits[0]) |> ignore

    splits
    |> Array.skip 1
    |> Array.iter (fun el ->
        if el.Contains("do()") then
            sb.Append(el.Substring(el.IndexOf("do()"))) |> ignore)

    sb.ToString()

let solver1 (lines: string array) =
    String.Join("", lines) |> processLine

let solver2 (lines: string array) =
    String.Join("", lines) |> prepareLineWithInstructions |> processLine
