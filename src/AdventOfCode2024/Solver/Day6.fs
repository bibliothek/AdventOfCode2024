module AdventOfCode2024.Solver.Day6

open AdventOfCodeHelpers
open AdventOfCodeHelpers.Array2DHelper

type State =
    { direction: Direction
      position: Pos
      visited: Pos Set }

type State2 =
    { direction: Direction
      position: Pos
      visited: (Pos * Direction) Set }

let turnRight (direction: Direction) =
    match direction with
    | Up -> Right
    | Right -> Down
    | Down -> Left
    | Left -> Up
    | _ -> failwith "illegal"

let rec getNextStep (state: State) map =
    let nextElement = tryGetWithDirection map state.position state.direction

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
              visited = state.visited.Add nextPos }
            map

let solver1 (lines: string array) =
    let map = buildFromLines lines

    let startingPos =
        map |> toSeqWithPoint |> Seq.filter (fun el -> snd el = '^') |> Seq.head |> fst

    let state =
        getNextStep
            { direction = Up
              visited = Set.empty.Add startingPos
              position = startingPos }
            map

    state.visited.Count

let getMapVariants (map: char array2d) =
    map
    |> Array2DHelper.toSeqWithPoint
    |> Seq.filter (fun (_, value) -> value = '.')
    |> Seq.map fst
    |> Seq.map (fun pos ->
        let copy = Array2D.copy map
        Array2D.set copy pos.x pos.y '#'
        copy)

let rec isALoop (state: State2) map =
    let nextElement = tryGetWithDirection map state.position state.direction

    match nextElement with
    | None -> false
    | Some(value, pos) ->
        let nextDirection =
            if value = '#' then
                (turnRight state.direction)
            else
                state.direction

        let tup = (pos, nextDirection)

        if state.visited.Contains(tup) then
            true
        else
            let nextPos =
                if state.direction = nextDirection then
                    pos
                else
                    state.position

            isALoop
                { direction = nextDirection
                  position = nextPos
                  visited = state.visited.Add tup }
                map

let solver2 (lines: string array) =
    let map = buildFromLines lines

    let startingPos =
        map |> toSeqWithPoint |> Seq.filter (fun el -> snd el = '^') |> Seq.head |> fst

    let variants = getMapVariants map

    variants
    |> Seq.map (
        isALoop
            { position = startingPos
              visited = Set.empty.Add((startingPos, Up))
              direction = Up }
    )
    |> Seq.filter id
    |> Seq.length
