docker run --name cassandra-1 --network cassandra-network -d cassandra:4.1.1

docker run --name cassandra-2 -d --network cassandra-network -e CASSANDRA_SEEDS=cassandra-1 cassandra:4.1.1

docker run --name cassandra-3 -d --network cassandra-network -e CASSANDRA_SEEDS=cassandra-1 cassandra:4.1.1

docker run -it --network cassandra-network --rm cassandra:4.1.1 cqlsh cassandra-2