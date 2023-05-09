rs.initiate({
            _id : 'rs0',
            members : [
              { _id : 0, host : 'mongo-node1:27017' },
              { _id : 1, host : 'mongo-node2:27017', secondaryDelaySecs : 30, priority: 0 },
              { _id : 2, host : 'mongo-node3:27017', secondaryDelaySecs : 30, priority: 0 }
            ]
          })

rs.initiate({
  _id : 'rs0',
  members : [
    { _id : 0, host : 'mongo-node1:27017' },
    { _id : 1, host : 'mongo-node2:27017'},
    { _id : 2, host : 'mongo-node3:27017'}
  ]
})

docker run --name mongo-node1 -d --net mongo-cluster-6-hw -p 27017:27017 mongo:5.0.16 --replSet rs0
docker run --name mongo-node2 -d --net mongo-cluster-6-hw -p 27018:27017 mongo:5.0.16 --replSet rs0
docker run --name mongo-node3 -d --net mongo-cluster-6-hw -p 27019:27017 mongo:5.0.16 --replSet rs0