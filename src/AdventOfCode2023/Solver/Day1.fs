module AdventOfCode2023.Solver.Day1

open System
open System.Text.RegularExpressions

let regex1 = "\d"
let getFirstInt (line: string) =
    Regex.Match(line, regex1).Value

let getLastInt (line: string) =
    Regex.Match(line, regex1, RegexOptions.RightToLeft).Value

let regex2 = "one|two|three|four|five|six|seven|eight|nine|\d"

let numberStringToIntString (number: string) =
    match number with
    | "one" -> "1"
    | "two" -> "2"
    | "three" -> "3"
    | "four" -> "4"
    | "five" -> "5"
    | "six" -> "6"
    | "seven" -> "7"
    | "eight" -> "8"
    | "nine" -> "9"
    | n -> n

let getFirstNumberWithString (line: string) =
    Regex.Match(line, regex2).Value |> numberStringToIntString

let getLastNumberWithString (line: string) =
    Regex.Match(line, regex2, RegexOptions.RightToLeft).Value |> numberStringToIntString

let solver1 (lines: string array) =
    lines
    |> Array.map (fun l ->
        (l |> getFirstInt) + (l |> getLastInt) |> Int32.Parse)
    |> Array.sum

let solver2 (lines: string array) =
    lines
    |> Array.map (fun l ->
        (l |> getFirstNumberWithString) + (l |> getLastNumberWithString) |> Int32.Parse)
    |> Array.sum
