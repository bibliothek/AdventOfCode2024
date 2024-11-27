module AdventOfCode2023.Solver.Day8

open System.Collections.Generic
open AdventOfCodeHelpers.MathHelper

type Node =
    { id: string
      left: string
      right: string }

type MyMap = IDictionary<string, Node>

let getMap (lines: string array) =
    lines
    |> Array.map (fun x ->
        (x.Substring(0, 3),
         { id = x.Substring(0, 3)
           left = x.Substring(7, 3)
           right = x.Substring(12, 3) }))
    |> dict

let getNextNodeId instruction node =
    match instruction with
    | 'L' -> node.left
    | 'R' -> node.right
    | _ -> failwith "impossible"

let rec findDistance (map: MyMap) (instructions: string) (node: Node) (count: int) endCondition : int =
    if endCondition node.id then
        count
    else
        let instruction = instructions[count % instructions.Length]
        let nextNodeId = getNextNodeId instruction node
        findDistance map instructions map[nextNodeId] (count + 1) endCondition

let solver1 (lines: string array) =
    let map = getMap (lines |> Array.skip 2)
    findDistance map lines[0] map["AAA"] 0 (fun x -> x = "ZZZ")

let solver2 (lines: string array) =
    let map = getMap (lines |> Array.skip 2)
    let startingNodes = map.Keys |> Seq.filter (fun x -> x[2] = 'A') |> Array.ofSeq

    let distances =
        startingNodes
        |> Array.map (fun nodeId -> findDistance map lines[0] map[nodeId] 0 (fun x -> x[2] = 'Z'))
        |> Array.map int64

    distances |> Array.fold lcm 1