namespace gameoflife

module Life = 
    type Cell = Cell of (int * int)
    type Board = Board of Set<Cell>

    let emptyBoard = Board Set.empty<Cell>

    let private boardSize = 5

    let neighbours c =
        let (Cell (x, y)) = c
        Set.ofList [ 
            for i in [ x - 1; x; x + 1] do
                for j in [ y - 1; y; y + 1] do
                    yield Cell ((i + boardSize) % boardSize, (j + boardSize) % boardSize) ]
            |> Set.remove c
    
    let cellShouldSurvive numNeighbours =
        numNeighbours = 2 || numNeighbours = 3

    let cellShouldComeToLife numNeighbours =
        numNeighbours = 3

    let private survivingCells (Board board) =
        board
            |> Set.filter (fun c -> c
                                    |> neighbours
                                    |> Set.intersect board
                                    |> Set.count
                                    |> cellShouldSurvive)

    let private newbornCells (Board board) =
        board
            |> Set.toSeq |> Seq.collect neighbours |> Set.ofSeq
            |> Set.filter (fun c -> not (Set.contains c board))
            |> Set.filter (fun c -> c 
                                    |> neighbours
                                    |> Set.intersect board
                                    |> Set.count
                                    |> cellShouldComeToLife)

    let evolve board =
        let survivors = survivingCells board
        let babyCells = newbornCells board
        
        Board (survivors + babyCells)

    let print (Board cells) =
        let boardWidth = boardSize * 2 + 1
        printfn "%s" <| String.replicate boardWidth "-"

        for i in 0..boardSize - 1 do
            let activeColumns = cells 
                                |> Set.filter (fun (Cell (_, y)) -> y = i)
                                |> Set.map (fun (Cell (x, _)) -> x)
            
            for j in 0..boardSize - 1 do
                if (Set.contains j activeColumns) then
                    printf "|*"
                else
                    printf "| "
            done

            printfn "|"
            printfn "%s" <| String.replicate boardWidth "-"
        done

