module AdventOfCode2024.Solver.Day5

open System
open System.Collections.Generic

let getRulesMap (lines: (int * int) array) =
    let map = new Dictionary<int, int Set>()

    lines
    |> Array.iter (fun el ->
        let k = snd el
        let v = fst el

        if map.ContainsKey k then
            map[k] <- map[k].Add(v)
        else
            map[k] <- Set.empty.Add(v))

    map

let isUpdateValid (rules: Dictionary<int, int Set>) (pages: int array) =
    let search =
        seq {
            for i in 0 .. pages.Length - 1 do
                yield
                    if rules.ContainsKey pages[i] then
                        let rule = rules[pages[i]]
                        let following = pages |> Array.skip (i + 1) |> Set.ofArray
                        (Set.intersect rule following |> Set.isEmpty)
                    else
                        true
        }

    search |> Seq.filter (id >> not) |> Seq.isEmpty

let getRulesAndUpdates lines =
    let splitIndex = lines |> Array.findIndex String.IsNullOrEmpty

    let rules =
        lines
        |> Array.take splitIndex
        |> Array.map (fun el ->
            let split = el.Split('|')
            (split[0] |> int, split[1] |> int))

    let updates =
        lines
        |> Array.skip (splitIndex + 1)
        |> Array.map (fun el -> el.Split(',') |> Array.map int)

    (rules, updates)


let solver1 (lines: string array) =
    let (rules, updates) = getRulesAndUpdates lines
    let rulesMap = getRulesMap rules

    let validUpdates = updates |> Array.filter (isUpdateValid rulesMap)

    validUpdates |> Array.map (fun el -> el[el.Length / 2]) |> Array.sum


let fixUpdate (rules: Dictionary<int, int Set>) (pages: int array) =
    pages
    |> Array.sortWith (fun x y ->
        if rules.ContainsKey(x) && rules[x].Contains(y) then
            -1
        else
            1)

let solver2 (lines: string array) =
    let (rules, updates) = getRulesAndUpdates lines
    let rulesMap = getRulesMap rules

    let invalidUpdates = updates |> Array.filter ((isUpdateValid rulesMap) >> not)

    invalidUpdates
    |> Array.map ((fixUpdate rulesMap) >> (fun el -> el[el.Length / 2]))
    |> Array.sum
