use e-commerce

db.orders.updateMany({items_id: {$in: [ObjectId("6448ea3e240816959ab93441")]}},
{
    $push:{
        items_id: "6448ea3e240816959ab93449"
    },
    $inc:{
       total_sum: 250 
    }
})