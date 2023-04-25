use e-commerce

db.items.find({$and:[{category: "Phone"}, {price: {$in: [600, 1000]}}]})

//db.items.find({$or:[{model: "Series 8"}, {model: "Galaxy 5"}]})

//db.items.find({producer: {$in: ["Apple", "Samsung"]}})