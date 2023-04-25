use e-commerce

db.orders.updateMany({}, [{
    $set:{
        "customer.name": {
            $concat: ["$customer.name", " new"]
        }
    }
},{
    $set:{
        "customer.surname": {
            $concat: ["$customer.surname", " new"]
        }
    }
}])