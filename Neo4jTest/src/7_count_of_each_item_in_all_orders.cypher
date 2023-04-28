// 7 count of each item in all orders
MATCH (o:Order) -[:CONTAINS]-> (i:Item)
RETURN i, count(i) as countInAllOrders
ORDER BY countInAllOrders DESC