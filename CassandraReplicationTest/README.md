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
### 8 Check possible consistency levels of reads/writes with one node off
#### Items (replication factor=3)
User should always be able to write/read with ONE, TWO=QUORUM consistency levels, as each node holds each partition of items table, thus allowing always to read with QUORUM=TWO consistency level. Apparently, user cannot write/read items with ALL(THREE) consistency level.
![8-items-keyspace-test-concern-levels](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/3a4ff95b-d197-4972-9a58-dd8b3ddb697c)
#### Orders (replication factor=2)
User should always be able to write/read with ONE consistency level. But, here it becomes more tricky, for TWO=QUORUM=ALL=2 it depends on, which nodes are going to store partition that the user try to insert data on. If it happens that correpsonding partition replicas are stored on two live nodes, then we will be able to insert/read data with TWO=QUORUM=ALL consistency levels:
![8 1-orders-keyspace-test-concern-levels](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/322cdd64-954c-45d0-9109-e6a853aad5c1)
But if it happens that partitioner directs one replica of the partition to the dead node, insert/read with TWO=QUORUM=ALL consistency levels would fail.
#### Reviews (replication factor=1)
Similiar story to orders: ONE=ALL would fail if the partition is stored on dead node, otherwise will succecced. One the image below, you can see that review record, with partition key=("Consistency", 1) is successfuly inserted with ONE=ALL consistency level, while record with partition key=("Watch", 1) failed to insert with the same ONE=ALL consistency level:
![8 2-reviews-keyspace-test-consern-levels](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/76329532-7168-46e9-8264-f8025be5000d)
### 9 Partition cluster
Disconect nodes 2 and 3 from the 'cassandra-network':
 - ```docker network disconnect cassandra-network cassandra-2```
 - ```docker network disconnect cassandra-network cassandra-3```
### 10 Add different version of the data with the same primary key to all three nodes
 - add data to nodes 2 and 3
![9,10,11-split-brain-2,3-node](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/d46b7425-d0d9-457a-8f91-d82b77aa961d)
 - add data to 1 node
![9,10,11-split-brain-3,1-node](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/3983daa2-79e3-4ddf-b2d5-7538362122e3)
### 11 Connect 2 and 3 nodes back to the main network and observer results
Connect 2 and 3 nodes back to the main network:
 - ```docker network connect --ip 172.18.0.3 cassandra-network cassandra-2```
 - ```docker network connect --ip 172.18.0.4 cassandra-network cassandra-3```

After reunion we can observe that node 1 version of data replaced values on nodes 2 and 3:  
![9,10,11-split-brain-reunioin-result](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/7fbb5147-3c0a-4833-9696-6cea5ef510f8)
The reason for that was, that node 1 version of data was written last, which means that it has the latest timestamp. So, after reunion node 1 with the help of 'hinted handoff' mechanism propogates its own version of data to nodes 2 and 3.  
