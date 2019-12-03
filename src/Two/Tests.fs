namespace Two

open Expecto
open Intcode

module Tests =
    let run =
        testList "Intcode" [
            test "Add for single run" {
                let result = 
                    [| 1; 0; 0; 0; 99 |]
                    |> Intcode.run

                Expect.equal result [|2;0;0;0;99|] "should add"
            }
            test "Multiply for single run" {
                let result =
                    [| 2; 3; 0; 3; 99 |]
                    |> Intcode.run

                Expect.equal result [|2;3;0;6;99|] "should multiply"
            }
            test "Add for two runs" {
                let result = 
                    [| 1; 0; 0; 0; 
                       1; 4; 4; 4; 
                       99 |]
                    |> Intcode.run 

                Expect.equal result [|
                    2;0;0;0;
                    2;4;4;4;
                    99
                |] "should add"
            }
            test "Saves result after exit code" {
                let list = [| 
                    2; 4; 4; 5; 
                    99; 0 
                |]
                
                let result = Intcode.run list

                Expect.equal result [|
                    2;4;4;5;
                    99; 9801
                |] "should add"
            }
        ]