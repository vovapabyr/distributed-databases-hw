db.items.insertOne({"category": "Watch", "model": "Pixel", "producer": "Google", "price": 300, "size": 41, "gps": "Yes"}, {writeConcern: {w: 3, wtimeout: 10000}})

db.items.find({}).readConcern("majority") 