# Cassandra replication test results
## How to run
Before running cassadra cluster lets create the netowrk with the subnet ```docker network create --subnet=172.18.0.0/29 cassandra-network```
### Run cluster of three nodes
 - First node: ```docker run --name cassandra-1 --network --ip 172.18.0.2 cassandra-network -d cassandra:4.1.1```
 - Second node: ```docker run --name cassandra-2 -d --network cassandra-network --ip 172.18.0.3 -e CASSANDRA_SEEDS=cassandra-1 cassandra:4.1.1```
 - Third node: ```docker run --name cassandra-3 -d --network cassandra-network --ip 172.18.0.4 -e CASSANDRA_SEEDS=cassandra-1 cassandra:4.1.1```
### Connect to cluster node to run cql scripts
```docker run -it --network cassandra-network --rm cassandra:4.1.1 cqlsh cassandra-2```

## Results
Numbers corresponds to the order in the hw document.
### 1,2 Cluster status
![1,2-cluster-status](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/b85cb8c6-56fc-4ea6-a7e5-8274a8b64b2c)
### 3 Create keyspaces with different replication factor
![3-create-keyspaces](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/cf8e64f5-0d03-4f5e-bfd5-87066437b5fa)
### 4 Create tables in keyspaces
#### Items
![4 1-create-items-table](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/328bb2db-d390-4ccf-81ec-eb56b6df7ee8)
#### Orders
![4 2-create-orders-table](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/993b80d6-5cf1-4449-8713-3ef274ba7fab)
#### Reviews
![4 3-create-reviews-table](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/9efefbf5-5b34-4415-a9ff-55222dc87770)
### 5 Write, read from different nodes
![5-insert-read-from-diff-nodes](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/68a63c5e-5ea6-4e86-9a44-3cb5cd254cfa)
### 6 Insert data and check data distribution across cluster
As you can see on the image below, each node holds:
- all items (because of replication factor=3)
- 2/3 of orders (because of replication factor=2)
- 1/3 of reviews (because of replication factor=1)  
![6-check-keyspace-status](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/613b079e-c462-4819-8a1c-892705a8ee77)
### 7 Check nodes which holds specific partition
![7-check-parition-replication](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/bda2fc66-20a8-4576-8e05-226d7d78f461)



docker network disconnect cassandra-network cassandra-2
docker network disconnect cassandra-network cassandra-3

INSERT INTO itemskeyspace.items_by_category JSON '{"id":100, "category": "Phone", "name": "iPhone 15", "producer": "Apple", "price": 10000, "data": {"Node": "3"}}';
