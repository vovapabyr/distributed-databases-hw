use e-commerce

db.orders.updateMany({
    $and: [
        {date: {$gte: ISODate("2023-04-24")}},
        {date: {$lte: ISODate("2023-04-25")}}
    ]
},{
    $pull:{
        items_id: "6447e92b84f0d2d330a9483c"
    }   
})