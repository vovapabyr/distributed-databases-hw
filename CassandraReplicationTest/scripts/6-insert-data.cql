-- insert items
INSERT INTO itemskeyspace.items_by_category JSON '{"id":1, "category": "Phone", "name": "iPhone 6", "producer": "Apple", "price": 400, "data": {"camera": "10mp", "weight": "120g"}}';
INSERT INTO itemskeyspace.items_by_category JSON '{"id":2, "category": "Phone", "name": "iPhone XS", "producer": "Apple", "price": 600, "data": {"camera": "12mp", "weight": "150g"}}';
INSERT INTO itemskeyspace.items_by_category JSON '{"id":3, "category": "Phone", "name": "iPhone 14", "producer": "Apple", "price": 1000, "data": {"camera": "12mp", "weight": "145g"}}';

INSERT INTO itemskeyspace.items_by_category JSON '{"id":1, "category": "TV", "name": "OLED", "producer": "LG", "price": 1500, "data": {"size": "65", "resolution": "4k"}}';
INSERT INTO itemskeyspace.items_by_category JSON '{"id":2, "category": "TV", "name": "OLED", "producer": "LG", "price": 1200, "data": {"size": "55", "resolution": "4k"}}';
INSERT INTO itemskeyspace.items_by_category JSON '{"id":3, "category": "TV", "name": "LCD", "producer": "LG", "price": 800, "data": {"size": "45", "resolution": "FullHD"}}';
INSERT INTO itemskeyspace.items_by_category JSON '{"id":4, "category": "TV", "name": "OLED", "producer": "SONY", "price": 2000, "data": {"size": "65", "resolution": "8k"}}';
INSERT INTO itemskeyspace.items_by_category JSON '{"id":5, "category": "TV", "name": "LCD", "producer": "SONY", "price": 1000, "data": {"size": "45", "resolution": "FullHD"}}';

INSERT INTO itemskeyspace.items_by_category JSON '{"id":1, "category": "Watch", "name": "Series 8", "producer": "Apple", "price": 500, "data": {"size": "41", "gps": "Yes"}}';
INSERT INTO itemskeyspace.items_by_category JSON '{"id":2, "category": "Watch", "name": "Galaxy 5", "producer": "Samsung", "price": 350, "data": {"size": "40", "gps": "Yes"}}';
INSERT INTO itemskeyspace.items_by_category JSON '{"id":3, "category": "Watch", "name": "Pixel", "producer": "Google", "price": 300, "data": {"size": "41", "gps": "Yes"}}';
INSERT INTO itemskeyspace.items_by_category JSON '{"id":4, "category": "Watch", "name": "Sense 2", "producer": "Fitbit", "price": 250, "data": {"size": "40", "gps": "No"}}';

-- insert orders
INSERT INTO orderskeyspace.orders_by_customer JSON '{    
	"id" : 1,
	"order_date" : "2023-04-24",
	"total_sum" : 1900,
	"customer" : "Andrii Rodionov",
	"items" : [["Phone", 1], ["TV", 1]]
}';

INSERT INTO orderskeyspace.orders_by_customer JSON '{    
	"id" : 1,
	"order_date" : "2023-04-25",
	"total_sum" : 2500,
	"customer" : "Volodymyr Pabyrivskyi",
	"items" : [["TV", 1], ["Phone", 3]]
}';

INSERT INTO orderskeyspace.orders_by_customer JSON '{    
	"id" : 1,
	"order_date" : "2023-04-25",
	"total_sum" : 1100,
	"customer" : "Yaroslava Selina",
	"items" : [["Phone", 2], ["Watch", 1]]
}';

INSERT INTO orderskeyspace.orders_by_customer JSON '{    
	"id" : 1,
	"order_date" : "2023-04-26",
	"total_sum" : 1500,
	"customer" : "Myroslava Kozachuk",
	"items" : [["TV", 1]]
}';

INSERT INTO orderskeyspace.orders_by_customer JSON '{    
	"id" : 2,
	"order_date" : "2023-04-26",
	"total_sum" : 600,
	"customer" : "Volodymyr Pabyrivskyi",
	"items" : [["Watch", 2], ["Watch", 4]]
}';

-- insert reviews
INSERT INTO reviewskeyspace.reviews_by_item JSON '{"id": 1, "customer": "Andrii Rodionov", "rating": 4, "item_id": ["TV", 1]}';
INSERT INTO reviewskeyspace.reviews_by_item JSON '{"id": 2, "customer": "Volodymyr Pabyrivskyi", "rating": 5, "item_id": ["TV", 1]}';
INSERT INTO reviewskeyspace.reviews_by_item JSON '{"id": 3, "customer": "Myroslava Kozachuk", "rating": 2, "item_id": ["TV", 1]}';
INSERT INTO reviewskeyspace.reviews_by_item JSON '{"id": 4, "customer": "Volodymyr Pabyrivskyi", "rating": 5, "item_id": ["Phone", 3]}';
INSERT INTO reviewskeyspace.reviews_by_item JSON '{"id": 5, "customer": "Yaroslava Selina", "rating": 2, "item_id": ["Watch", 1]}';
INSERT INTO reviewskeyspace.reviews_by_item JSON '{"id": 6, "customer": "Yaroslava Selina", "rating": 4, "item_id": ["Phone", 2]}';