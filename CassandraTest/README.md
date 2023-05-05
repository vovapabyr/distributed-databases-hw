# CQL scripts results
## How to run
- Use custom image of cassandra which has 'materialized_views_enabled' option set to true by default. Run cassandra cluster with one node: 
```docker run --name cassandra-1 --network cassandra-network -d pabyrivskyiv/cassandra-mv-enabled:4.1.1```
- To run queries in the cluster run second node with cql shell. Substitute 'path_to_scripts' with the path to the [scripts](scripts) folder on your host machine, to be able to run scripts from cql shell:
```docker run -it -v path_to_scripts:/scripts --name cassandra-cqlsh --network cassandra-network --rm pabyrivskyiv/cassandra-mv-enabled:4.1.1 cqlsh cassandra-1```
- Create 'ecommerce' keyspace with 'items', 'orders' tables and all related materialized views and indexes:
  ```
  SOURCE '/scripts/create_schema.cql'
  ```
  Check the keyspace with ```DESCRIBE ecommerce;``` command.
- Insert 'items' and 'orders':
  ```
  SOURCE '/scripts/insert_items.cql'
  SOURCE '/scripts/insert_orders.cql'
  ```
## Results
Numbers corresponds to the order in the hw document.

### Items
#### 1 Description of items_by_category table. [script](scripts/1_describe_items.cql)
![1_describe_items](https://user-images.githubusercontent.com/25819135/236435729-69d0646f-596c-4178-893a-2990512ae11d.PNG)
#### 2 Items of specific category ordered by price descending. [script](scripts/2_items_in_category_sorted_by_price.cql)
![2_items_in_category_sorted_by_price](https://user-images.githubusercontent.com/25819135/236435924-380b3064-fc52-46aa-9aca-edd99010bd44.PNG)
#### 3 Items filtered by different properties of specific category. [script](scripts/3_items_in_category_filtered.cql)
![3_items_in_category_filtered](https://user-images.githubusercontent.com/25819135/236436274-1498ebd1-8ff0-4844-8c5f-3e35fef2c6b7.PNG)
#### 4 Items that have specific property (or specific property equals to some value) in the map. [script](scripts/4_items_map_contains_key_and_filtered.cql)  
![4_items_map_contains_key_and_filtered](https://user-images.githubusercontent.com/25819135/236436560-9b9755e2-0c19-49f4-87a4-d75c4259ed8e.PNG)
#### 5 Update item map property. [script](scripts/5_update_items.cql)
![5_update_items](https://user-images.githubusercontent.com/25819135/236436768-33c89b1e-2c52-4846-abad-2f6d2e721e84.PNG)

### Orders
#### 1 Description of orders_by_customer table. [script](scripts/1_describe_orders.cql) 
![1_describe_orders](https://user-images.githubusercontent.com/25819135/236437954-06232412-a4c3-4c2e-91ad-6a66866b4a85.PNG)
#### 2 Orders of specific customer ordered by date ascending. [script](scripts/2_orders_of_customer_by_date.cql)
![2_orders_of_customer_by_date](https://user-images.githubusercontent.com/25819135/236438200-325a7c73-cda9-436c-9f6f-e0c6651d1253.PNG)
#### 3 Orders of specific customer that have specific item. [script](scripts/3_order_of_customer_with_specific_item.cql)
![3_order_of_customer_with_specific_item](https://user-images.githubusercontent.com/25819135/236438562-350af1ab-f398-4d10-b767-569a53413f06.PNG)
#### 4 Orders of specific customer within a date range and their count. [script](scripts/4_orders_of_customer_in_date_range.cql)
![4_orders_of_customer_in_date_range](https://user-images.githubusercontent.com/25819135/236438757-baa9cb86-2077-4c9c-841f-99823e92a0f8.PNG)
#### 5 Total sum of orders for each customer. [script](scripts/5_total_sum_of_orders_of_customers.cql)
![5_total_sum_of_orders_of_customer](https://user-images.githubusercontent.com/25819135/236438917-136d5d03-6bfc-4e9a-9230-e919d132a982.PNG)
#### 6 Order with max total sum for each customer. [script](scripts/6_max_order_of_customers.cql)
![6_max_order_of_customers](https://user-images.githubusercontent.com/25819135/236439053-dfb1660d-2fd7-4f67-b77f-c296031a9a5f.PNG)
#### 7 Update items order and update total sum. [script](scripts/7_update_order.cql)
![7_update_order](https://user-images.githubusercontent.com/25819135/236439270-847af100-a521-4e1e-8523-f4b12eeccb66.PNG)
#### 8 Display total sum's writetime for each order. [script](scripts/8_orders_total_writetime.cql) 
![8_orders_total_writetime](https://user-images.githubusercontent.com/25819135/236439514-6fd288d3-2ef3-407f-8add-eb41562d491c.PNG)
#### 9 Insert order with TTL. [script](scripts/9_insert_order_ttl.cql)
![9_insert_order_ttl](https://user-images.githubusercontent.com/25819135/236439643-da1f1092-0092-400f-a0ed-5ec8f6c4636e.PNG)
#### 10 Orders as JSON. [script](scripts/10_orders_json.cql)
![10_orders_json](https://user-images.githubusercontent.com/25819135/236439745-21b1a2b9-e7af-4a69-92e1-814919055c28.PNG)
#### 11 Insert order as JSON. [script](scripts/11_insert_order_json.cql)
![11_insert_order_json](https://user-images.githubusercontent.com/25819135/236439844-93d24c84-cfd8-4a07-973d-b43333e1254b.PNG)




