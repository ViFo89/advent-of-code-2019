namespace Two

module Intcode =

    type Opcode =
    | Add of (int * int * int)
    | Multiply of (int * int * int)
    | End
    | Unknown of int

    let calc op (list: int array) (addr1, addr2, saveTo) =
        let firstValue = list.[addr1]
        let secondValue = list.[addr2]

        list.[saveTo] <-  op firstValue secondValue
        list

    let multiply = calc (*)
    
    let add = calc (+)

    let resolveOpcode (list: int array) pointer =
        match list.[pointer] with
            | 1 -> (Add (list.[pointer + 1], list.[pointer + 2], list.[pointer + 3]), pointer + 4)
            | 2 ->  (Multiply (list.[pointer + 1], list.[pointer + 2], list.[pointer + 3]), pointer + 4)
            | 99 -> (End, pointer)
            | num -> (Unknown num, pointer)

    let rec handleOpcode (list: int array) pointer = 

        let (opcode, nextPointer) = resolveOpcode list pointer

        let list' =
            match opcode with
            | Add tpl -> handleOpcode (add list tpl) nextPointer
            | Multiply tpl -> handleOpcode (multiply list tpl) nextPointer
            | End -> list
            | Unknown _ -> list

        list'

    let run (list: int array) = 
        handleOpcode list 0








(*

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
*)