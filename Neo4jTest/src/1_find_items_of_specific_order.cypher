//1 find items of specific order
MATCH (o:Order {id: 21}) -[:CONTAINS]-> (i:Item) RETURN i