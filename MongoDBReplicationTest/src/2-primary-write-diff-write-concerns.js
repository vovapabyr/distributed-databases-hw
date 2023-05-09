db.items.insertOne({"category": "Phone", "model": "iPhone 6", "producer": "Apple", "price": 400, "camera": "10mp"}, { writeConcern: { w: 0 }}) // unacknowledged
db.items.insertOne({"category": "TV", "model": "OLED", "producer": "LG", "price": 1500, "size": 65, "resolution": "4k"}, { writeConcern: { w: 1 }}) // acknowledged
db.items.insertOne({"category": "TV", "model": "OLED", "producer": "SONY", "price": 2000, "size": 65, "resolution": "8k"}, { writeConcern: { w: 1, j: true }}) // journaled
db.items.insertOne({"category": "Watch", "model": "Series 8", "producer": "Apple", "price": 500, "size": 41, "gps": "yes"}, { writeConcern: { w: "majority" }}) // replica acknowledged
