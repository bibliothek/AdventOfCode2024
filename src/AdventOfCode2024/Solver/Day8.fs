module AdventOfCode2024.Solver.Day8

open System.Collections.Generic
open AdventOfCodeHelpers
open AdventOfCodeHelpers.Array2DHelper

let getOccurrenceDict (map: char array2d) =
    let occurenceDict = Dictionary<char, Pos Set>()

    map
    |> Array2DHelper.toSeqWithPoint
    |> Seq.filter (fun (_, el) -> el <> '.')
    |> Seq.iter (fun (pos, el) ->
        match occurenceDict.ContainsKey el with
        | false -> occurenceDict[el] <- Set.empty.Add(pos)
        | true -> occurenceDict[el] <- (occurenceDict[el]).Add(pos))

    occurenceDict

let getNewPoints (map: char array2d) (points: Pos*Pos)  =
    let a = (fst points)
    let b = (snd points)
    let xDiff = a.x - b.x
    let yDiff = a.y - b.y
    let xFactor = if xDiff < 0 then 1 else -1
    let yFactor = if yDiff < 0 then -1 else 1
    let newPoint1 = {x = a.x + xFactor * xDiff; y = a.y + yFactor * yDiff}
    let newPoint2 = {x = b.x - xFactor * xDiff; y = b.y - yFactor * yDiff}
    Set.empty.Add(newPoint1).Add(newPoint2) |> Set.filter (Array2DHelper.isInBounds map)

let solver1 (lines: string array) =
    let map = Array2DHelper.buildFromLines lines
    let occurenceDict = Array2DHelper.buildFromLines lines |> getOccurrenceDict

    let result =
        occurenceDict.Keys
        |> Seq.fold
            (fun acc el ->
                let points = occurenceDict[el] |> Set.toArray
                let combinations = [
                    for i in 0 .. points.Length - 1  do
                        for j in 1 .. points.Length - 1 do
                            if j > i then
                                yield (points[i], points[j])
                ]
                let validCombinations = combinations |> Seq.map (getNewPoints map) |> Seq.fold Set.union Set.empty
                Set.union acc validCombinations
                )
            Set.empty

    let inputPositions = occurenceDict.Values |> Seq.fold Set.union Set.empty


    Set.difference result inputPositions |> Set.count

let solver2 (lines: string array) = failwith "error"
