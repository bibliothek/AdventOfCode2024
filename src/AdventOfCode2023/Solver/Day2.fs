module AdventOfCode2023.Solver.Day2

open System.Text.RegularExpressions

type CubeSet = { blues: int; reds: int; greens: int }

type Game = { id: int; cubeSets: CubeSet[] }

let maxRed = 12
let maxGreen = 13
let maxBlue = 14

let isSetPossible (set: CubeSet) =
    set.blues <= maxBlue && set.reds <= maxRed && set.greens <= maxGreen

let isGamePossible (game: Game) =
    game.cubeSets |> Array.forall isSetPossible

let setRegex = "\s(\d+)\s(blue|red|green)"

let lineToSet (s: string) : CubeSet =
    let mutable greens = 0
    let mutable blues = 0
    let mutable reds = 0

    for m in Regex.Matches(s, setRegex) do
        match m.Groups[2].Value with
        | "red" -> reds <- (m.Groups[1].Value |> int)
        | "green" -> greens <- (m.Groups[1].Value |> int)
        | "blue" -> blues <- (m.Groups[1].Value |> int)
        | _ -> ()

    { blues = blues
      reds = reds
      greens = greens }

let lineToGame (line: string) : Game =
    let splitLine = line.Substring(5).Split(":")
    let sets = splitLine[1].Split(";")

    { id = splitLine[0] |> int
      cubeSets = sets |> Array.map lineToSet }

let solver1 (lines: string array) =
    lines
    |> Array.map lineToGame
    |> Array.filter isGamePossible
    |> Array.sumBy (fun g -> g.id)

let getGamePower (game: Game) : int =
    let maxReds = game.cubeSets |> Array.map (fun c -> c.reds) |> Array.max
    let maxGreens = game.cubeSets |> Array.map (fun c -> c.greens) |> Array.max
    let maxBlues = game.cubeSets |> Array.map (fun c -> c.blues) |> Array.max

    maxReds * maxBlues * maxGreens

let solver2 (lines: string array) =
    lines |> Array.map (lineToGame >> getGamePower) |> Array.sum
