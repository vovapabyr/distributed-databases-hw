use e-commerce

db.items.insertMany([
    {"category": "Phone", "model": "iPhone 6", "producer": "Apple", "price": 400, "camera": "10mp"},
    {"category": "Phone", "model": "iPhone XS", "producer": "Apple", "price": 600, "camera": "12mp"},
    {"category": "Phone", "model": "iPhone 14", "producer": "Apple", "price": 1000, "camera": "12mp"},
    //
    {"category": "TV", "model": "OLED", "producer": "LG", "price": 1500, "size": 65, "resolution": "4k"},
    {"category": "TV", "model": "OLED", "producer": "LG", "price": 1200, "size": 55, "resolution": "4k"},
    {"category": "TV", "model": "LCD", "producer": "LG", "price": 800, "size": 45, "resolution": "FullHD"},
    {"category": "TV", "model": "OLED", "producer": "SONY", "price": 2000, "size": 65, "resolution": "8k"},
    {"category": "TV", "model": "LCD", "producer": "SONY", "price": 1000, "size": 45, "resolution": "FullHD"},
    //
    {"category": "Watch", "model": "Series 8", "producer": "Apple", "price": 500, "size": 41, "gps": "Yes"},
    {"category": "Watch", "model": "Galaxy 5", "producer": "Samsung", "price": 350, "size": 40, "gps": "Yes"},
    {"category": "Watch", "model": "Pixel", "producer": "Google", "price": 300, "size": 41, "gps": "Yes"},
    {"category": "Watch", "model": "Sense 2", "producer": "Fitbit", "price": 250, "size": 40, "gps": "No"},
]);