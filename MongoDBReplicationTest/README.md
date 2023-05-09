# Mongosh scrupts results
## How to run
Before running mongodb replica set in docker, create network: ```docker network create mongo-cluster-6-hw``` 
### Run cluster of three nodes
 - First node: ```docker run --name mongo-node1 -d --net mongo-cluster-6-hw -p 27017:27017 mongo:5.0.16 --replSet rs0```
 - Second node: ```docker run --name mongo-node2 -d --net mongo-cluster-6-hw -p 27018:27017 mongo:5.0.16 --replSet rs0```
 - Third node: ```docker run --name mongo-node3 -d --net mongo-cluster-6-hw -p 27019:27017 mongo:5.0.16 --replSet rs0```
### Create replica set
To create replica set connect to any of the node and run:
```
rs.initiate({
  _id : 'rs0',
  members : [
    { _id : 0, host : 'mongo-node1:27017' },
    { _id : 1, host : 'mongo-node2:27017'},
    { _id : 2, host : 'mongo-node3:27017'}
  ]
})
```
To run replica set with replication delay (30s) to secondary nodes run:
```
rs.initiate({
  _id : 'rs0',
  members : [
    { _id : 0, host : 'mongo-node1:27017' },
    { _id : 1, host : 'mongo-node2:27017', secondaryDelaySecs : 30, priority: 0 },
    { _id : 2, host : 'mongo-node3:27017', secondaryDelaySecs : 30, priority: 0 }
  ]
  })
```
### Connect to replica set to run queries
```docker run -it --network mongo-cluster-6-hw --rm mongo:5.0.16 mongosh "mongodb://mongo-node1:27017,mongo-node2:27017,mongo-node3:27017/ecommerce?replicaSet=rs0"```

## Results
Numbers corresponds to the order in the hw document.
### 1 Replica set status
![1-replica-set-status](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/6716d6fe-e9aa-4261-a715-0dfd65bca86c)
### 2 Primary node write with different write concerns
![2-primary-write-concerns](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/d697fec0-714d-4135-8d75-1bfb11b4e94b)
### 3 Read with different preferences
 - Primary
![3 1-read-with-primary-pref](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/edf1aada-cbfe-4631-a0ea-4a781b585fad)
 - Secondary
![3 2-read-with-secondary-pref](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/9eb58acd-5e71-4b50-a26e-df98dca0c24e)
### 4 Write with w=3, with one node paused. Write is blocked until blocked node is alive again
![4-block-write-with-one-node-paused-then-unpause-inserted](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/05d9fd7d-d604-4879-bd3d-e1f817db1db3)
### 5 Write with w=3, with one node paused, but this time with timeout. When timeout elapsed, error occurs, but we still can read data with 'majority' concern, as only one out of three nodes are unavailable
![5-timeout-write-successful-read-with-majority](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/d889c4cf-b6c7-4ab2-86b4-3bd79e632573)
### 6 New leader election
 - Turn off primary and observe new leader election
![6 1-new-primary-election](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/ceada9d5-fda4-4d73-80f7-aa750608ba9a)
 - New elements insrted with new primary
![6 2-new-elements-inserted](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/e978110a-6be5-468b-b106-38e4e7365e35)
 - Turn on paused node, and observe that data is replicated
![6 3-read-from-recovered-secondary](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/22b30c43-f43c-4702-8197-a0c3836b922c)
### 7 Make cluster inconsistent
 - Turn off two secondary nodes. Primary is going to be primary for the next 10 sec, so insert new element. Observe that we can read new data with 'local' read concern (dirty reads!), but data is unavaiable for 'majority' and 'linearizable' concerns
 ![7 1-insert-into-single-node-and-dirty-reads-with-local](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/929d65ed-3e1b-46eb-a0d5-9a5f95831c47)
 - Stop single executing node. Run two nodes, that were stopped at the beggining. New primary is elected, and observe that when old primary is back 'dirty reads' data are lost.
![7 2-lost-dirty-reads](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/a99cd63f-ad87-4613-af27-d8b693f15e6a)
### 8 Eventual consistency with delayed secondary replication
![8-eventual-consistency-with-delay](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/a6659bc2-33c7-4473-8391-67f69b9ce8b0)
### 9 Observe how read with 'linearizable' read concern level is blocked until delayed replication happens to majority.
![9-linearizable-block-by-delayed-replication](https://github.com/vovapabyr/distributed-databases-tests/assets/25819135/bef5b7c1-19fa-483d-a0c3-219c5085b3e8)

  

