// 11 specific customer viewed bot not bought items
MATCH (c:Customer {id: 16}) -[:VIEW]-> (i:Item)
WHERE NOT (c) -[:BOUGHT]-> (:Order) -[:CONTAINS]-> (i) 
RETURN i