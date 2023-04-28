// 8 items viewed by specific customer
MATCH (:Customer {id: 16}) -[:VIEW]-> (i:Item) RETURN i