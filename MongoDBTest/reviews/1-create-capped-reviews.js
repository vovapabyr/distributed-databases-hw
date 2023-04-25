use e-commerce

db.createCollection("reviews", { capped : true, size : 4096, max : 5 } )