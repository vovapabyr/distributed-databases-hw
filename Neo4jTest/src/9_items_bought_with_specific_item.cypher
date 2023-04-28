// 9 items bought with specific item
MATCH (:Item {id: 6}) <-[:CONTAINS]- (:Order) -[:CONTAINS]-> (i:Item) RETURN distinct(i)