db.items.insertOne({"category": "Phone", "model": "iPhone 14", "producer": "Apple", "price": 1000, "camera": "12mp"})
db.items.insertOne({"category": "Phone", "model": "iPhone XS", "producer": "Apple", "price": 600, "camera": "12mp"})

db.items.find({}).readPref("secondary")
