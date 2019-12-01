namespace One

open Expecto

module Rocket =
    let calculateFuel mass =
        let result =
            mass / 3.
            |> floor
        result - 2.

    let rec calculateFuel' mass =
        let result = calculateFuel mass

        if result <= 0. then
            0.
        else 
            result + (calculateFuel' result)
        

module Tests =
    let tests =
        testList "Calculate Fuel" [
            test "Mass of 12" {
                let mass = 12.
                let result = Rocket.calculateFuel mass

                Expect.equal result 2. "Mass of 12 should use 2 fuel"
            }

            test "Mass of 14" {
                let mass = 14.
                let result = Rocket.calculateFuel mass

                Expect.equal result 2. "Mass of 14 should use 2 fuel"
            }

            test "Mass of 1969" {
                let mass = 1969.
                let result = Rocket.calculateFuel mass

                Expect.equal result 654. "Mass of 1969 should use 654 fuel"
            }

            test "Mass of 100756" {
                let mass = 100_756.
                let result = Rocket.calculateFuel mass

                Expect.equal result 33_583. "Mass of 100756 should use 33583 fuel"
            }

        ]