// 5 count of items of specific customer
MATCH (:Customer {id: 16}) -[:BOUGHT]-> (o:Order) -[:CONTAINS]-> (i:Item) RETURN size(collect(i))