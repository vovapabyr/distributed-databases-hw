// 3 orders of specific customer
MATCH (:Customer {id: 16}) -[:BOUGHT]-> (o:Order) RETURN o