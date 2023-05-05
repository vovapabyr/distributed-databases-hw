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
#### 1 Describe of items_by_category table. [script](scripts/1_describe_items.cql)
![1_describe_items](https://user-images.githubusercontent.com/25819135/236435729-69d0646f-596c-4178-893a-2990512ae11d.PNG)
#### 2 Items of specific category ordered by price descending [script](scripts/2_items_in_category_sorted_by_price.cql)
![2_items_in_category_sorted_by_price](https://user-images.githubusercontent.com/25819135/236435924-380b3064-fc52-46aa-9aca-edd99010bd44.PNG)
#### 3 Items filtered by different properties of specific category [script](scripts/3_items_in_category_filtered.cql)
![3_items_in_category_filtered](https://user-images.githubusercontent.com/25819135/236436274-1498ebd1-8ff0-4844-8c5f-3e35fef2c6b7.PNG)
#### 4 Items that have specific property (or specific property equals to some value) in the map [script](scripts/4_items_map_contains_key_and_filtered.cql)  
![4_items_map_contains_key_and_filtered](https://user-images.githubusercontent.com/25819135/236436560-9b9755e2-0c19-49f4-87a4-d75c4259ed8e.PNG)
#### 5 Update item map property [script](scripts/5_update_items.cql)
![5_update_items](https://user-images.githubusercontent.com/25819135/236436768-33c89b1e-2c52-4846-abad-2f6d2e721e84.PNG)
