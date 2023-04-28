//0 create e-commerce graph
CREATE (phoneIpone6:Item:Phone {id : 1, name : "iPhone 6", price : 400})
CREATE (phoneIponeXS:Item:Phone {id : 2, name : "iPhone XS", price : 600})
CREATE (phoneIpone14:Item:Phone {id : 3, name : "iPhone 14", price : 1000})
CREATE (phoneSmasungA54:Item:Phone {id : 4, name : "Samsung A54", price : 500})
CREATE (phoneSamsungA23:Item:Phone {id : 5, name : "Samsung Galaxy A23", price : 1500})
// TVs
CREATE (tvLgOled65:Item:TV {id : 6, name : "LG OLED A65", price : 1500})
CREATE (tvLgOled55:Item:TV {id : 7, name : "LG OLED A55", price : 1200})
CREATE (tvLgLc45:Item:TV {id : 8, name : "LG LCD F45", price : 600})
CREATE (tvSonyOled65:Item:TV {id : 9, name : "Sony OLED 65", price : 2000})
CREATE (tvSonyLcd45:Item:TV {id : 10, name : "Sony LCD 45", price : 800})
// Watches
CREATE (watchSeries8:Item:Watch {id : 11, name : "Series 8", price : 500})
CREATE (watchGalaxy5:Item:Watch {id : 12, name : "Galaxy 5", price : 350})
CREATE (watchPixel:Item:TV {id : 13, name : "Pixel", price : 300})
CREATE (watchSense2:Item:TV {id : 14, name : "Fitbit Sense 2", price : 250})


// Customers
CREATE (aRadionov:Customer {id : 15, name : "Andrii Rodionov"})
CREATE (vPabyrivskyi:Customer {id : 16, name : "Volodymyr Pabyrivskyi"})
CREATE (ySelina:Customer {id : 17, name : "Yaroslava Selina"})
CREATE (mKozachuk:Customer {id : 18, name : "Myroslava Kozachuk"})

// Orders
CREATE (aRadionov) -[:BOUGHT]-> (order1:Order {id:19, date: date("2023-04-24")})
CREATE (order1) -[:CONTAINS]-> (phoneSamsungA23)
CREATE (order1) -[:CONTAINS]-> (tvLgOled65)
CREATE (vPabyrivskyi) -[:BOUGHT]-> (order2:Order {id:20, date: date("2023-04-25")})
CREATE (order2) -[:CONTAINS]-> (phoneIponeXS)
CREATE (order2) -[:CONTAINS]-> (tvLgOled55)
CREATE (ySelina) -[:BOUGHT]-> (order3:Order {id:21, date: date("2023-04-25")})
CREATE (order3) -[:CONTAINS]-> (tvLgOled65)
CREATE (order3) -[:CONTAINS]-> (phoneIpone14)
CREATE (mKozachuk) -[:BOUGHT]-> (order4:Order {id:22, date: date("2023-04-26")})
CREATE (order4) -[:CONTAINS]-> (watchSeries8)
CREATE (order4) -[:CONTAINS]-> (tvSonyOled65)
CREATE (order4) -[:CONTAINS]-> (phoneSamsungA23)
CREATE (vPabyrivskyi) -[:BOUGHT]-> (order5:Order {id:23, date: date("2023-04-26")})
CREATE (order5) -[:CONTAINS]-> (watchGalaxy5)

// Views
CREATE (aRadionov) -[:VIEW]-> (phoneSamsungA23)
CREATE (aRadionov) -[:VIEW]-> (phoneSmasungA54)
CREATE (aRadionov) -[:VIEW]-> (tvLgOled65)
CREATE (aRadionov) -[:VIEW]-> (tvSonyOled65)
CREATE (vPabyrivskyi) -[:VIEW]-> (phoneIponeXS)
CREATE (vPabyrivskyi) -[:VIEW]-> (phoneIpone14)
CREATE (vPabyrivskyi) -[:VIEW]-> (tvLgOled55)
CREATE (vPabyrivskyi) -[:VIEW]-> (tvSonyOled65)
CREATE (vPabyrivskyi) -[:VIEW]-> (watchGalaxy5)
CREATE (vPabyrivskyi) -[:VIEW]-> (watchSeries8)
CREATE (ySelina) -[:VIEW]-> (tvLgOled65)
CREATE (ySelina) -[:VIEW]-> (tvSonyOled65)
CREATE (ySelina) -[:VIEW]-> (phoneIpone14)
CREATE (ySelina) -[:VIEW]-> (watchSeries8)
CREATE (mKozachuk) -[:VIEW]-> (watchSeries8)
CREATE (mKozachuk) -[:VIEW]-> (tvSonyOled65)
CREATE (mKozachuk) -[:VIEW]-> (phoneSamsungA23)


