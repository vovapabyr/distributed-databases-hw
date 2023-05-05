INSERT INTO ecommerce.orders_by_customer JSON '{    
	"id" : 3,
	"order_date" : "2023-04-27",
	"total_sum" : 300,
	"customer" : "Volodymyr Pabyrivskyi",
	"items" : [["Watch", 3]]
}' USING TTL 10;