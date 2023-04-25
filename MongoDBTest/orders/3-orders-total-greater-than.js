use e-commerce

db.orders.find({total_sum: {$gt: 1500}})