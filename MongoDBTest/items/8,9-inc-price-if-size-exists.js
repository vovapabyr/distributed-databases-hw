use e-commerce

db.items.updateMany({ size: {$exists: true}}, { $inc: {price: 2} })