module AdventOfCode2023.Solver.Day7

type Hand =
    { cards: char array
      bid: int
      handRanking: int }

let cardOrder1 =
    [| 'A'; 'K'; 'Q'; 'J'; 'T'; '9'; '8'; '7'; '6'; '5'; '4'; '3'; '2' |]

let cardOrder2 =
    [| 'A'; 'K'; 'Q'; 'T'; '9'; '8'; '7'; '6'; '5'; '4'; '3'; '2'; 'J' |]

let getCardOrder cardOrder (c: char) =
    cardOrder |> Array.findIndex (fun x -> x = c)

let getHand (withJokers: bool) (line: string) : Hand =
    let split = line.Split()
    let cards = split[0].ToCharArray()

    let jokers =
        if withJokers then
            cards |> Array.filter (fun x -> x = 'J') |> Array.length
        else
            0

    let groups =
        cards
        |> Array.filter (fun x -> not withJokers || x <> 'J')
        |> Array.groupBy id
        |> Array.map snd

    let largestGroupLength =
        if groups.Length = 0 then
            0
        else
            (groups |> Array.maxBy (fun x -> x.Length)).Length

    let handRanking =
        match groups.Length, (largestGroupLength + jokers) with
        | 0, _ -> 7
        | 1, _ -> 7
        | 2, 4 -> 6
        | 2, 3 -> 5
        | 3, 3 -> 4
        | 3, 2 -> 3
        | 4, _ -> 2
        | 5, _ -> 1
        | _ -> failwith "impossible"

    { cards = cards
      bid = split[1] |> int
      handRanking = handRanking }

let compareHands cardOrder (a: Hand) (b: Hand) : int =
    if a.handRanking <> b.handRanking then
        compare a.handRanking b.handRanking
    else
        let differingIndex =
            a.cards |> Array.indexed |> Array.tryFindIndex (fun (i, el) -> el <> b.cards[i])

        if differingIndex.IsNone then
            0
        else
            let aCardOrder = a.cards[differingIndex.Value] |> (getCardOrder cardOrder)
            let bCardOrder = b.cards[differingIndex.Value] |> (getCardOrder cardOrder)
            bCardOrder - aCardOrder

let solver1 (lines: string array) =
    lines
    |> Array.map (getHand false)
    |> Array.sortWith (compareHands cardOrder1)
    |> Array.mapi (fun i el -> el.bid * (i + 1))
    |> Array.sum

let solver2 (lines: string array) =
    lines
    |> Array.map (getHand true)
    |> Array.sortWith (compareHands cardOrder2)
    |> Array.mapi (fun i el -> el.bid * (i + 1))
    |> Array.sum
