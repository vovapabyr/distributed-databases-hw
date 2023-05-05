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

CREATE TABLE IF NOT EXISTS ecommerce.orders_by_customer (
    id int,
    customer text,
    order_date timestamp,
    total_sum float,
    items list<tuple<text, int>>,
    PRIMARY KEY ((customer), order_date, id));

CREATE INDEX IF NOT EXISTS ON ecommerce.orders_by_customer (items);

CREATE MATERIALIZED VIEW IF NOT EXISTS ecommerce.orders_by_customer_total AS
   SELECT * FROM ecommerce.orders_by_customer
   WHERE customer IS NOT NULL AND order_date IS NOT NULL AND id IS NOT NULL AND total_sum IS NOT NULL
   PRIMARY KEY ((customer), total_sum, order_date, id)
   WITH CLUSTERING ORDER BY (total_sum DESC, order_date ASC, id ASC);