docker network create --subnet=172.18.0.0/29 cassandra-network

docker run --name cassandra-1 --network --ip 172.18.0.2 cassandra-network -d cassandra:4.1.1

docker run --name cassandra-2 -d --network cassandra-network --ip 172.18.0.3 -e CASSANDRA_SEEDS=cassandra-1 cassandra:4.1.1

docker run --name cassandra-3 -d --network cassandra-network --ip 172.18.0.4 -e CASSANDRA_SEEDS=cassandra-1 cassandra:4.1.1

docker run -it --network cassandra-network --rm cassandra:4.1.1 cqlsh cassandra-2

docker network disconnect cassandra-network cassandra-2
docker network disconnect cassandra-network cassandra-3

INSERT INTO itemskeyspace.items_by_category JSON '{"id":100, "category": "Phone", "name": "iPhone 15", "producer": "Apple", "price": 10000, "data": {"Node": "3"}}';