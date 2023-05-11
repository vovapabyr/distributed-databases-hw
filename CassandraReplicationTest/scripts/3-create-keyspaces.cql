CREATE KEYSPACE IF NOT EXISTS itemskeyspace WITH replication = {'class': 'SimpleStrategy', 'replication_factor' : 3};
CREATE KEYSPACE IF NOT EXISTS orderskeyspace WITH replication = {'class': 'SimpleStrategy', 'replication_factor' : 2};
CREATE KEYSPACE IF NOT EXISTS reviewskeyspace WITH replication = {'class': 'SimpleStrategy', 'replication_factor' : 1};