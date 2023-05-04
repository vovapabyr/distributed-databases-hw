CREATE KEYSPACE IF NOT EXISTS ecommerce WITH replication = {'class': 'SimpleStrategy'};

CREATE TABLE IF NOT EXISTS ecommerce.items_by_category (
    id int,
    category text,
    name text,
    producer text,
    price float,
    data map<text, text>,
    PRIMARY KEY ((category), price, id));

CREATE INDEX IF NOT EXISTS ON ecommerce.items_by_category (keys(data));
CREATE INDEX IF NOT EXISTS ON ecommerce.items_by_category (entries(data));
CREATE INDEX IF NOT EXISTS ON ecommerce.items_by_category (name);

CREATE MATERIALIZED VIEW IF NOT EXISTS ecommerce.items_by_category_producer AS
   SELECT * FROM ecommerce.items_by_category
   WHERE category IS NOT NULL AND price IS NOT NULL AND producer IS NOT NULL AND id IS NOT NULL
   PRIMARY KEY ((category), producer, price, id);