module AdventOfCode2023.Solver.Day10

open System
open AdventOfCodeHelpers
open AdventOfCodeHelpers.Array2DHelper

let getNextDirection (enter: Direction) (pipe: char) =
    match enter, pipe with
    | Up, '|' -> Some Up
    | Up, '7' -> Some Left
    | Up, 'F' -> Some Right
    | Left, '-' -> Some Left
    | Left, 'F' -> Some Down
    | Left, 'L' -> Some Up
    | Right, '-' -> Some Right
    | Right, 'J' -> Some Up
    | Right, '7' -> Some Down
    | Down, '|' -> Some Down
    | Down, 'J' -> Some Left
    | Down, 'L' -> Some Right
    | _ -> None

let getValidStartingDirections (map: char array2d) pos =
    let startingDirections =
        [ ((tryGetLeft map pos), Left)
          ((tryGetRight map pos), Right)
          ((tryGetUp map pos), Up)
          ((tryGetDown map pos), Down) ]

    startingDirections
    |> List.filter (fst >> Option.isSome)
    |> List.map (fun (el, direction) -> ((direction, el.Value), getNextDirection direction (el.Value |> fst)))
    |> List.filter (snd >> Option.isSome)
    |> List.map fst

let getStartingPos map =
    map |> toSeqi |> Seq.find (fun (_, el) -> el = 'S') |> fst

let rec traverseCollect (map: char array2d) startPos pos direction (points: (int * int) list) =
    if startPos = pos && (points.IsEmpty |> not) then
        points
    else
        let currentVal = map[pos |> fst, pos |> snd]
        let nextDirection = getNextDirection direction currentVal |> Option.get
        let nextPos = tryGetWithDirection map pos nextDirection |> Option.get |> snd
        traverseCollect map startPos nextPos nextDirection (nextPos :: points)

let getPoints lines =
    let map = buildFromLines lines
    let startingPos = getStartingPos map
    let validStartingDirections = getValidStartingDirections map startingPos
    let startingDirection = validStartingDirections.Head
    let nextPos = (startingDirection |> snd |> snd)
    traverseCollect map startingPos nextPos (startingDirection |> fst) [ nextPos ]

let solver1 (lines: string array) =
    let points = getPoints lines
    (points |> List.length) / 2

let getSurface (points: (int * int) list) =
    let doubledArea =
        points
        |> List.pairwise
        |> List.append [ (points |> List.last, points.Head) ]
        |> List.map (fun ((x1, y1), (x2, y2)) -> x1 * y2 - y1 * x2)
        |> List.sum

    Math.Abs(doubledArea / 2)

let solver2 (lines: string array) =
    let points = getPoints lines |> List.rev
    let surface = getSurface points
    surface - (points.Length / 2) + 1
