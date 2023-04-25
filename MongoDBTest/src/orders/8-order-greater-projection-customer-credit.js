use e-commerce

db.orders.find({total_sum: {$gt: 1500}}, {"customer.name": 1, "payment.cardId": 1})