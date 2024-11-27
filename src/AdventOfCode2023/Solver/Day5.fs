module AdventOfCode2023.Solver.Day5

type Mapping =
    { sourceStart: int64
      sourceEnd: int64
      destinationStart: int64
      destinationEnd: int64 }

type Mappings =
    { seedToSoilMapping: Mapping array
      soilToFertilizerMapping: Mapping array
      fertilizerToWaterMapping: Mapping array
      waterToLightMapping: Mapping array
      lightToTemperatureMapping: Mapping array
      temperatureToHumidityMapping: Mapping array
      humidityToLocationMapping: Mapping array }

let getSeeds1 (line: string) =
    (line.Split(":")[1]).Trim().Split() |> Array.map int64

let getMapping (lines: string array) : Mapping array =
    lines
    |> Array.map (fun x ->
        let splits = x.Split()
        let range = splits.[2] |> int64

        let sourceStart = splits[1] |> int64
        let destinationStart = splits[0] |> int64

        { sourceStart = sourceStart
          sourceEnd = sourceStart + range
          destinationStart = destinationStart
          destinationEnd = destinationStart + range })

let getMappings (lines: string array) : Mappings =
    let seedToSoilIndex = lines |> Array.findIndex (fun x -> x = "seed-to-soil map:")

    let soilToFertilizerIndex =
        lines |> Array.findIndex (fun x -> x = "soil-to-fertilizer map:")

    let fertilizerToWaterIndex =
        lines |> Array.findIndex (fun x -> x = "fertilizer-to-water map:")

    let waterToLightIndex =
        lines |> Array.findIndex (fun x -> x = "water-to-light map:")

    let lightToTemperatureIndex =
        lines |> Array.findIndex (fun x -> x = "light-to-temperature map:")

    let temperatureToHumidityIndex =
        lines |> Array.findIndex (fun x -> x = "temperature-to-humidity map:")

    let humidityToLocationIndex =
        lines |> Array.findIndex (fun x -> x = "humidity-to-location map:")

    { seedToSoilMapping = getMapping lines[seedToSoilIndex + 1 .. soilToFertilizerIndex - 2]
      soilToFertilizerMapping = getMapping lines[soilToFertilizerIndex + 1 .. fertilizerToWaterIndex - 2]
      fertilizerToWaterMapping = getMapping lines[fertilizerToWaterIndex + 1 .. waterToLightIndex - 2]
      waterToLightMapping = getMapping lines[waterToLightIndex + 1 .. lightToTemperatureIndex - 2]
      lightToTemperatureMapping = getMapping lines[lightToTemperatureIndex + 1 .. temperatureToHumidityIndex - 2]
      temperatureToHumidityMapping = getMapping lines[temperatureToHumidityIndex + 1 .. humidityToLocationIndex - 2]
      humidityToLocationMapping = getMapping lines[humidityToLocationIndex + 1 ..] }


let getMappedValue (maps: Mapping array) (el: int64) : int64 =
    let mapping =
        maps |> Array.tryFind (fun x -> el >= x.sourceStart && el < x.sourceEnd)

    match mapping with
    | Some v -> v.destinationStart + (el - v.sourceStart)
    | None -> el

let getLocation (mappings: Mappings) (seed: int64) : int64 =
    seed
    |> getMappedValue mappings.seedToSoilMapping
    |> getMappedValue mappings.soilToFertilizerMapping
    |> getMappedValue mappings.fertilizerToWaterMapping
    |> getMappedValue mappings.waterToLightMapping
    |> getMappedValue mappings.lightToTemperatureMapping
    |> getMappedValue mappings.temperatureToHumidityMapping
    |> getMappedValue mappings.humidityToLocationMapping

let solver1 (lines: string array) =
    let seeds = getSeeds1 lines.[0]
    let mappings = getMappings lines

    seeds |> Array.map (getLocation mappings) |> Array.min

let getSeeds2 (line: string) =
    let seedDefinitions = getSeeds1 line
    seq {
        for i in 0..2 .. (seedDefinitions.Length - 1) do
            for j in seedDefinitions[i] .. seedDefinitions[i] + seedDefinitions[i + 1] - 1L do
                yield j
    }

let solver2 (lines: string array) =
    let seeds = getSeeds2 lines.[0]
    let mappings = getMappings lines
    seeds |> Seq.map (getLocation mappings) |> Seq.min
