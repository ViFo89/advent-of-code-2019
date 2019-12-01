// Learn more about F# at http://fsharp.org

open System

open Expecto
open One.Tests

let readInput path = System.IO.File.ReadLines(path)



[<EntryPoint>]
let main argv =
    let masses = readInput "./src/console/one_input"

    let result =
        masses 
        |> Seq.map float
        |> Seq.sumBy One.Rocket.calculateFuel'

    printf "Sum is: %f" result

    // runTests defaultConfig One.Tests.tests |> ignore
    Console.ReadLine() |> ignore
    0 // return an integer exit code
