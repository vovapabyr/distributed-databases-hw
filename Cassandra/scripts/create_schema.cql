CREATE KEYSPACE IF NOT EXISTS ecommerce WITH replication = {'class': 'SimpleStrategy'};

CREATE TABLE IF NOT EXISTS ecommerce.items_by_category (
    id int,
    category text,
    name text,
    producer text,
    price float,
    data map<text, text>,
    PRIMARY KEY ((category), price, producer, id));

CREATE MATERIALIZED VIEW IF NOT EXISTS ecommerce.items_by_category_name AS
   SELECT * FROM ecommerce.items_by_category
   WHERE category IS NOT NULL AND price IS NOT NULL AND producer IS NOT NULL AND id IS NOT NULL AND name IS NOT NULL
   PRIMARY KEY ((category), name, producer, price, id);