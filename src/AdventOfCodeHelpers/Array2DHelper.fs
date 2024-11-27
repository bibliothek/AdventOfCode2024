module AdventOfCodeHelpers.Array2DHelper

open System
open System.Text

type Direction =
    | Up
    | Down
    | Left
    | Right

type Pos =
    {
        x: int
        y: int
    }

type Pos64 =
    {
        x: int64
        y: int64
    }

let buildFromLinesAsInt (lines: string array) =
    Array2D.init lines.[0].Length lines.Length (fun x y -> Char.GetNumericValue lines.[y].[x] |> int)

let buildFromLines (lines: string array) =
    Array2D.init lines.[0].Length lines.Length (fun x y -> lines.[y].[x])

let toSeq<'A> (array: 'A[,]) =
    seq {
        for x in 0 .. Array2D.length1 array - 1 do
            for y in 0 .. Array2D.length2 array - 1 do
                yield array.[x, y]
    }
let toSeqi<'A> (array: 'A[,]) =
    seq {
        for x in 0 .. Array2D.length1 array - 1 do
            for y in 0 .. Array2D.length2 array - 1 do
                yield ((x,y),array.[x, y])
    }

let toSeqWithPoint<'A> (array: 'A[,]) =
    seq {
        for x in 0 .. Array2D.length1 array - 1 do
            for y in 0 .. Array2D.length2 array - 1 do
                yield ({x = x; y = y},array.[x, y])
    }

let getPrintableOverview map =
    let sb = StringBuilder()

    for y in 0 .. (map |> Array2D.length2) - 1 do
        for x in 0 .. (map |> Array2D.length1) - 1 do
            sb.Append(map.[x, y] |> string) |> ignore

        sb.Append Environment.NewLine |> ignore

    sb.ToString()

let getHorizontalAndVerticalNeighbours map pos =
    let x, y = pos

    [| (x + 1, y); (x - 1, y); (x, y + 1); (x, y - 1) |]
    |> Array.filter (fun (x, y) -> x >= 0 && x < Array2D.length1 map && y >= 0 && y < Array2D.length2 map)

let tryGetWithDirection map pos direction =
    let x, y = pos
    match direction with
    | Up -> if y - 1 < 0 then None else Some (Array2D.get map (x) (y-1), (x,y-1))
    | Down -> if y + 1 >= Array2D.length2 map then None else Some (Array2D.get map (x) (y+1), (x,y+1))
    | Left -> if x - 1 < 0 then None else Some (Array2D.get map (x-1) y, (x - 1, y))
    | Right -> if x + 1 >= Array2D.length1 map then None else Some (Array2D.get map (x+1) y, (x + 1, y))

let tryGetLeft map pos =  tryGetWithDirection map pos Left

let tryGetRight map pos =  tryGetWithDirection map pos Right

let tryGetUp map pos = tryGetWithDirection map pos Up

let tryGetDown map pos = tryGetWithDirection map pos Down