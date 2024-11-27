module AdventOfCode2023.Solver.Day6

open System

type Race =
    {
        time: int64
        recordDistance: int64
    }

let getRaces (lines: string array) : Race array =
    let times = lines.[0].Split(":").[1].Trim().Split(Array.zeroCreate<char> 0, StringSplitOptions.RemoveEmptyEntries)
    let distances = lines.[1].Split(":").[1].Trim().Split(Array.zeroCreate<char> 0, StringSplitOptions.RemoveEmptyEntries)
    Array.init times.Length (fun i -> {time = times.[i] |> int64; recordDistance = distances.[i] |> int64})

let beatsRecordDistance (time:int64) (speed:int64) (record: int64) : bool =
    (time - speed) * speed > record

let getLowerBound (race:Race) : int64 =
    Seq.initInfinite (fun i -> (i |> int64) + 1L)
        |> Seq.skipWhile (fun el -> (beatsRecordDistance race.time el race.recordDistance) |> not) |> Seq.head

let getUpperBound (race:Race) : int64 =
    Seq.initInfinite (fun i -> (race.time - 2L) - (i |> int64))
        |> Seq.skipWhile (fun el -> (beatsRecordDistance race.time el race.recordDistance) |> not) |> Seq.head

let getWaysToBeatRecord (race:Race) : int64 =
    let lowerBound = getLowerBound race
    let upperBound = getUpperBound race
    upperBound - lowerBound + 1L

let solver1 (lines: string array) =
    lines
    |> getRaces
    |> Array.map getWaysToBeatRecord
    |> Array.fold (fun state el -> state * el) 1L

let getRace (lines: string array) : Race =
    let time = lines.[0].Split(":").[1].Trim().Split(Array.zeroCreate<char> 0, StringSplitOptions.RemoveEmptyEntries) |> Array.fold (fun state el -> state + el ) ""
    let distance = lines.[1].Split(":").[1].Trim().Split(Array.zeroCreate<char> 0, StringSplitOptions.RemoveEmptyEntries) |> Array.fold (fun state el -> state + el ) ""
    {time = time |> int64; recordDistance = distance |> int64}

let solver2 (lines: string array) =
    lines
    |> getRace
    |> getWaysToBeatRecord
