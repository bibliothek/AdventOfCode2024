module AdventOfCode2024.Solver.Day6

open AdventOfCodeHelpers
open AdventOfCodeHelpers.Array2DHelper

type State =
    { direction: Direction
      position: Pos
      visited: Pos Set }

let turnRight (direction: Direction) =
    match direction with
    | Up -> Right
    | Right -> Down
    | Down -> Left
    | Left -> Up
    | _ -> failwith "illegal"

let rec getNextStep (state: State) map =
    let nextElement = Array2DHelper.tryGetWithDirection map state.position state.direction

    match nextElement with
    | None -> state
    | Some(value, pos) ->
        let nextDirection =
            if value = '#' then
                (turnRight state.direction)
            else
                state.direction
        let nextPos =
                if state.direction = nextDirection then
                    pos
                else
                    state.position

        getNextStep
            { direction = nextDirection
              position = nextPos
              visited = state.visited.Add nextPos}
            map

let solver1 (lines: string array) =
    let map = Array2DHelper.buildFromLines lines

    let startingPos =
        map
        |> Array2DHelper.toSeqWithPoint
        |> Seq.filter (fun el -> snd el = '^')
        |> Seq.head
        |> fst

    let state =
        getNextStep
            { direction = Up
              visited = Set.empty.Add startingPos
              position = startingPos }
            map

    state.visited.Count

let solver2 (lines: string array) = failwith "error"
