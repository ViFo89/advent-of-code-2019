// Learn more about F# at http://fsharp.org

open System

open Expecto
open Two

let readInputAsString path = System.IO.File.ReadAllText(path)
let readInput path = System.IO.File.ReadLines(path)



[<EntryPoint>]
let main argv =
    // let masses = readInput "./src/console/one_input"
    let numbers = readInputAsString "./src/console/two_input"
    let list = 
        numbers.Split [|','|]
        |> Array.map int


    
    
    for i in 1 .. 100 do
        for j in 1 .. 100 do
            let newList = list |> Array.copy
            newList.[1] <- i
            newList.[2] <- j
            let res = Intcode.run newList
            let firstVal = res.[0]
            
            if firstVal = 19690720 then
                printfn "SUCCESS: Noun: %i | Verb: %i | Answer: %i" i j firstVal
            // else 
            //     printfn "FAIL: Noun: %i | Verb: %i | Answer: %i" i j firstVal
            |> ignore                                    
        
    
    // printf "%A" res
    // runTests defaultConfig Tests.run |> ignore

    // runTests defaultConfig One.Tests.tests |> ignore
    // Console.ReadLine() |> ignore
    0 // return an integer exit code

