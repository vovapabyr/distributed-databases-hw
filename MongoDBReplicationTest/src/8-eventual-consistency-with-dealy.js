db.items.insertOne({"category": "Delay", "model": "Delay", "producer": "Delay"}, { writeConcern: { w: 1 }})

db.items.find({}).readConcern("local")
db.items.find({}).readConcern("majority")