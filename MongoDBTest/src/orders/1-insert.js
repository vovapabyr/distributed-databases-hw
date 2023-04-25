use e-commerce

db.orders.insertMany([
{    
	"order_number" : 1,
	"date" : ISODate("2023-04-24"),
	"total_sum" : 1900,
	"customer" : {
    	"name" : "Andrii",
    	"surname" : "Rodinov",
    	"phones" : [ 9876543, 1234567],
    	"address" : "PTI, Peremohy 37, Kyiv, UA"
	},
	"payment" : {
    	"card_owner" : "Andrii Rodionov",
    	"cardId" : 12345678
	},
	"items_id" : [ObjectId("6447e92b84f0d2d330a94839"), ObjectId("6447e92b84f0d2d330a9483c")]
},
{    
	"order_number" : 2,
	"date" : ISODate("2023-04-25"),
	"total_sum" : 2500,
	"customer" : {
    	"name" : "Volodymyr",
    	"surname" : "Pabyrivskyi",
    	"phones" : [ 936140299],
    	"address" : "Shevchenka 68, Lviv, UA"
	},
	"payment" : {
    	"card_owner" : "Volodymyr Pabyrivskyi",
    	"cardId" : 87654321
	},
	"items_id" : [ObjectId("6447e92b84f0d2d330a9483c"), ObjectId("6447e92b84f0d2d330a9483b")]
},
{    
	"order_number" : 3,
	"date" : ISODate("2023-04-25"),
	"total_sum" : 1100,
	"customer" : {
    	"name" : "Yaroslava",
    	"surname" : "Selina",
    	"phones" : [ 976140299 ],
    	"address" : "Shevchenka 68, Lviv, UA"
	},
	"payment" : {
    	"card_owner" : "Yaroslava Selina",
    	"cardId" : 87654321
	},
	"items_id" : [ObjectId("6447e92b84f0d2d330a9483a"), ObjectId("6447e92b84f0d2d330a94841")]
},
{    
	"order_number" : 4,
	"date" : ISODate("2023-04-26"),
	"total_sum" : 1500,
	"customer" : {
    	"name" : "Myroslava",
    	"surname" : "Kozachuk",
    	"phones" : [ 986140299 ],
    	"address" : "Valencia, ESP"
	},
	"payment" : {
    	"card_owner" : "Myroslava Kozachuk",
    	"cardId" : 87654321
	},
	"items_id" : [ObjectId("6447e92b84f0d2d330a9483c")]
},
{    
	"order_number" : 5,
	"date" : ISODate("2023-04-26"),
	"total_sum" : 600,
	"customer" : {
    	"name" : "Volodymyr",
    	"surname" : "Pabyrivskyi",
    	"phones" : [ 936140299],
    	"address" : "Shevchenka 68, Lviv, UA"
	},
	"payment" : {
    	"card_owner" : "Volodymyr Pabyrivskyi",
    	"cardId" : 87654321
	},
	"items_id" : [ObjectId("6447e92b84f0d2d330a94842"), ObjectId("6447e92b84f0d2d330a94844")]
}
])