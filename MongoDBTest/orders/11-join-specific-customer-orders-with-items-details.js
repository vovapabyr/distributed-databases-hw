db.orders.aggregate([
{
    $match:{
        $and:[
            {
                "customer.name": "Volodymyr"            
            },
            {
                "customer.surname": "Pabyrivskyi"
            }        
        ]
    }
},
{
    $lookup:{
        from: "items",
        localField: "items_id",
        foreignField: "_id",
        as: "items"
    }
},
{
    $project:{
        customer_name: "$customer.name",
        customer_surname: "$customer.surname",
        items: {
            $map: {
                input: "$items",
                as: "item",
                in: {
                    model: "$$item.model",
                    price: "$$item.price"
                }
            }
        }
    }
}
])