// Learn more about F# at http://fsharp.org

open System
open gameoflife

[<EntryPoint>]
let main _ =
    printfn "Here's a flipper!"

    let flipper = Life.Board(Set.ofList [Life.Cell (0, 1); Life.Cell (1, 1); Life.Cell (2, 1)])

    Life.print flipper

    Life.print (Life.evolve flipper)
    0 // return an integer exit code
