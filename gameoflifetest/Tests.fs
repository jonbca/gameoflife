module Tests

open Xunit
open gameoflife

[<Fact>]
let ``Produces set of neighbours`` () =
    let expected = Set.ofList [ Life.Cell (0, 0); Life.Cell (1, 0); Life.Cell (2, 0);
                                Life.Cell (0, 1); Life.Cell (2, 1);
                                Life.Cell (0, 2); Life.Cell (1, 2); Life.Cell (2, 2) ]
    let actual = Life.neighbours (Life.Cell (1, 1))
    Assert.Equal<Set<Life.Cell>>(expected, actual)

[<Fact>]
let ``Wraps neighbours to a board of 5 from top left`` () =
    let expected = Set.ofList [ Life.Cell (4, 4); Life.Cell (0, 4); Life.Cell (1, 4);
                                Life.Cell (4, 0); Life.Cell (1, 0);
                                Life.Cell (4, 1); Life.Cell (0, 1); Life.Cell (1, 1) ]
    let actual = Life.neighbours (Life.Cell (0, 0))
    Assert.Equal<Set<Life.Cell>>(expected, actual)

[<Fact>]
let ``Wraps neighbours to a board of 5 from bottom right`` () =
    let expected = Set.ofList [ Life.Cell (3, 3); Life.Cell (4, 3); Life.Cell (0, 3);
                                Life.Cell (3, 4); Life.Cell (0, 4);
                                Life.Cell (3, 0); Life.Cell (4, 0); Life.Cell (0, 0) ]
    let actual = Life.neighbours (Life.Cell (4, 4))
    Assert.Equal<Life.Cell>(expected, actual)

[<Theory>]
[<InlineData(2)>]
[<InlineData(3)>]
let ``cell survives with a number of neighbours`` (numNeighbours) =
    Assert.True(Life.cellShouldSurvive(numNeighbours))

[<Theory>]
[<InlineData(4)>]
[<InlineData(5)>]
[<InlineData(6)>]
[<InlineData(7)>]
[<InlineData(8)>]
let ``cell dies from overcrowding`` (numNeighbours) =
    Assert.False(Life.cellShouldSurvive(numNeighbours))

[<Theory>]
[<InlineData(0)>]
[<InlineData(1)>]
let ``cell dies from loneliness`` (numNeighbours) =
    Assert.False(Life.cellShouldSurvive(numNeighbours))

[<Fact>]
let ``cell should come to life with three neighbours`` () =
    Assert.True(Life.cellShouldComeToLife 3)

[<Theory>]
[<InlineData(0)>]
[<InlineData(1)>]
[<InlineData(2)>]
[<InlineData(4)>]
[<InlineData(5)>]
[<InlineData(6)>]
[<InlineData(7)>]
[<InlineData(8)>]
let ``cell should not come to life otherwise`` (numNeighbours) =
    Assert.False(Life.cellShouldComeToLife numNeighbours)

[<Fact>]
let ``Evolving an empty board gives an empty board`` () =
    Assert.Equal<Life.Board>(Life.evolve(Life.emptyBoard), Life.emptyBoard)

[<Fact>]
let ``Evolving a board with a flipper flips the flipper`` () =
    let flipper = Set.ofList [Life.Cell (0, 1); Life.Cell (1, 1); Life.Cell (2, 1)]
    let expected = Life.Board (Set.ofList [Life.Cell (1, 0); Life.Cell (1, 1); Life.Cell (1, 2)])
    let actual = Life.evolve (Life.Board flipper)

    Assert.Equal<Life.Board>(expected, actual)