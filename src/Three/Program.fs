// Learn more about F# at http://fsharp.org

open System

type Direction =
| R of int
| D of int
| U of int 
| L of int

let rec walk pos dir (collisions: Set<int*int>) (currentRoute: Set<int*int>)=
    let move dir (x,y) =
        match dir with
        | U _ -> (x, y + 1)
        | D _ -> (x, y - 1)
        | R _ -> (x + 1, y)
        | L _ -> (x - 1, y)

    let newPosition = move dir pos
    let walk' = walk newPosition

    let m dir col route = 
        match dir with
        | U 0 | D 0 | R 0 | L 0 -> (route, pos, col)
        | U i -> walk' (U (i - 1)) col route
        | D i -> walk' (D (i - 1)) col route
        | R i -> walk' (R (i - 1)) col route
        | L i -> walk' (L (i - 1)) col route

    if Set.contains newPosition currentRoute then
        let col = Set.add newPosition collisions
        m dir col currentRoute
    else 
        let newRoute = Set.add newPosition currentRoute
        m dir collisions newRoute
    
let getShortestCollision (currentShortest: int option) (collisions: Set<int * int>) =
    let shortest =
        if Set.isEmpty collisions then
            None
        else
            collisions
            |> Set.map (fun (x,y) -> abs x + abs y) 
            |> Set.minElement 
            |> Some

    match shortest, currentShortest with
    | Some i, Some j when i <= j -> Some i
    | Some _, Some j -> Some j
    | Some i, None -> Some i
    | _ -> currentShortest
    

let rec mapRoute' (directions: Direction list) currentRoute pos shortestCollision =
    
    match directions with
    | [] -> currentRoute, shortestCollision
    | (head::tail) -> (
        let (newRoute, currentPos, collisions) = walk pos head Set.empty<int*int> currentRoute
        let newShortest = getShortestCollision shortestCollision collisions
        mapRoute' tail newRoute currentPos newShortest)

let mapRoute (input: Direction list) currentRoute = 
    mapRoute' input currentRoute (0, 0) None

let mapDir (x:string) =
    let toInt = 
        x.[1..] |> int

    match x.[0] with 
    | 'R' -> toInt |> R
    | 'D' -> toInt |> D
    | 'U' -> toInt |> U
    | 'L' -> toInt |> L
    | _ -> raise (Exception("Not valid"))


[<EntryPoint>]
let main argv =
    let route = [|"R75";"D30";"R83";"U83";"L12";"D49";"R71";"U7";"L72"|]
    let input = route |> Array.toList |> List.map mapDir

    let newRoute, collision = mapRoute input Set.empty<int*int>
    
    let route2 = [|"U62";"R66";"U55";"R34";"D71";"R55";"D58";"R83"|]
    let input2 = route |> Array.toList |> List.map mapDir
    
    let newRoute2, collision2 = mapRoute input2 newRoute

    match collision2 with
    | Some i -> printfn "Shortest collision is: %i" i
    | None -> printfn "No collisions"
    0 // return an integer exit code
