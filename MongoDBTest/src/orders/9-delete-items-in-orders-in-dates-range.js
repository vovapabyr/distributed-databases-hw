use e-commerce

db.orders.updateMany({
    $and: [
        {date: {$gte: ISODate("2023-04-24")}},
        {date: {$lte: ISODate("2023-04-25")}}
    ]
},{
    $pull:{
        items_id: ObjectId("6448ea3e240816959ab93441")
    }   
})