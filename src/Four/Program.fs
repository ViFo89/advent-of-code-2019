// Learn more about F# at http://fsharp.org

open System
open Password

let rec loop start stop acc= 
    printfn "loop | start: %i | acc: %i" start acc
    let result = findPasswords start stop

    match result with
    | Result i -> loop (i + 1) stop (acc + 1) 
    | NoResult -> acc



[<EntryPoint>]
let main argv =
    let start = 108457
    let stop = 562041
    let result = loop start stop 0

    printfn "Result is %i" result
    
    
    0 // return an integer exit code
