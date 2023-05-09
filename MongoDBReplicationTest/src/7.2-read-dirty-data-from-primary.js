db.items.find({_id: ObjectId("6458fc843d4c02ceffbe10c0")}).readConcern("local")
db.items.find({_id: ObjectId("6458fc843d4c02ceffbe10c0")}).readConcern("majority")
db.items.find({_id: ObjectId("6458fc843d4c02ceffbe10c0")}).readConcern("linearizable")