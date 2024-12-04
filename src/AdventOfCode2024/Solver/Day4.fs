module AdventOfCode2024.Solver.Day4

open AdventOfCodeHelpers
open AdventOfCodeHelpers.Array2DHelper

let rec evaluateFromX (map: char array2d) (pos) =
    let neighbours =
        getNeighboursIncludingDiagonal map pos
        |> List.filter (fun (el, _) -> el = 'M')

    if neighbours.Length = 0 then
        0
    else
        neighbours
        |> List.map (fun (el, newPos) ->
            let direction = getDirection pos newPos
            let thirdLetter = tryGetWithDirection map newPos direction

            if thirdLetter.IsNone || thirdLetter.Value |> fst <> 'A' then
                false
            else
                let fourthLetter = tryGetWithDirection map (thirdLetter.Value |> snd) direction

                match fourthLetter with
                | Some x when (fst x) = 'S' -> true
                | _ -> false)
        |> List.filter id |> List.length

let rec evaluateFromA (map: char array2d) (pos) =
    let upRight = tryGetWithDirection map pos UpRight |> Option.map fst
    let downRight = tryGetWithDirection map pos DownRight |> Option.map fst
    let upLeft = tryGetWithDirection map pos UpLeft |> Option.map fst
    let downLeft = tryGetWithDirection map pos DownLeft |> Option.map fst
    let someCount = [upLeft ; upRight ; downLeft ; downRight] |> List.filter Option.isSome |> List.length
    if someCount < 4 then false
    else
        ((upRight.Value = 'M' && downLeft.Value = 'S') ||
        (upRight.Value = 'S' && downLeft.Value = 'M')) &&

        ((upLeft.Value = 'M' && downRight.Value = 'S') ||
        (upLeft.Value = 'S' && downRight.Value = 'M'))

let solver1 (lines: string array) =
    let puzzle = buildFromLines lines

    let Xes =
        puzzle |> toSeqWithPoint |> Seq.filter (fun (_, el) -> el = 'X')

    Xes |> Seq.map fst |> Seq.map (evaluateFromX puzzle) |> Seq.sum

let solver2 (lines: string array) =
    let puzzle = buildFromLines lines

    let As =
        puzzle |> toSeqWithPoint |> Seq.filter (fun (_, el) -> el = 'A')

    As
    |> Seq.map fst
    |> Seq.map (evaluateFromA puzzle)
    |> Seq.filter id
    |> Seq.length
