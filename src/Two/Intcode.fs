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