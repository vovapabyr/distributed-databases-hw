db.items.insertOne({"category": "Delay", "model": "Delay", "producer": "Delay"}, { writeConcern: { w: 1 }})
db.items.insertOne({"category": "Delay1", "model": "Delay1", "producer": "Delay1"}, { writeConcern: { w: 1 }})

db.items.find({}).readConcern("linearizable")