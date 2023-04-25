use e-commerce

db.orders.updateMany({items_id: {$in: ["6447db9569d8dabda1ea2075"]}},
{
    $push:{
        items_id: "6447db9569d8dabda1ea207d"
    },
    $inc:{
       total_sum: 250 
    }
})