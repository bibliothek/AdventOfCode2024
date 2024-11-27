module AdventOfCode2023.Solver.Day4

open System

type Card =
    { matchingNumbers: int
      index: int }

let lineToCard (idx: int) (line: string) =
    let noPrefix = line.Split(":").[1].Split("|")

    let winningNumbers =
        noPrefix.[0]
            .Trim()
            .Split(Array.zeroCreate<char> 0, StringSplitOptions.RemoveEmptyEntries)
        |> Array.map int
        |> Set.ofArray

    let numbers =
        noPrefix.[1]
            .Trim()
            .Split(Array.zeroCreate<char> 0, StringSplitOptions.RemoveEmptyEntries)
        |> Array.map int
        |> Set.ofArray

    { matchingNumbers = (Set.intersect numbers winningNumbers) |> Set.count
      index = idx }

let getCardValue (card: Card) =
    if card.matchingNumbers = 0 then
        0
    else
        pown 2 (card.matchingNumbers - 1)

let solver1 (lines: string array) =
    lines |> Array.mapi lineToCard |> Array.map getCardValue |> Array.sum

let updateState (state: int list) (el: Card) : int list =
    if el.matchingNumbers = 0 then
        state
    else
        let countCardsInState = state |> List.filter (fun x -> x = el.index) |> List.length
        let mutable newState = state

        for j in 1..countCardsInState do
            for i in (el.index + 1) .. (el.index + el.matchingNumbers) do
                newState <- i :: newState

        newState


let solver2 (lines: string array) =
    let cards = lines |> Array.mapi lineToCard

    cards
    |> Array.fold updateState (cards |> Array.map (fun c -> c.index) |> Array.toList)
    |> List.length
