// 10 customers who bought specific item
MATCH (c:Customer) -[:BOUGHT]-> (:Order) -[:CONTAINS]-> (:Item {id: 6}) RETURN distinct(c)