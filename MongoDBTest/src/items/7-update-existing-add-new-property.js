use e-commerce

db.items.updateMany({category: "Watch"},
[{
    $set: {
        segment: {
            $cond: {
                if: {$lte: ["$price", 300]}, then: "cheap", else: "expensive"
            }
        }
    }
},
{
    $set:{
        model: {
            $toUpper: "$model"
        }
    }
}
]
)