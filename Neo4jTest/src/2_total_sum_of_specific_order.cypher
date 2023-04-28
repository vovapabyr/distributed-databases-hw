//2 total sum of specific order
MATCH (o:Order {id: 21}) -[:CONTAINS]-> (i:Item) RETURN sum(i.price)