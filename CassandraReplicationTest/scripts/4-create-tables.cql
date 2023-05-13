CREATE TABLE IF NOT EXISTS itemskeyspace.items_by_category (
    id int,
    category text,
    name text,
    producer text,
    price float,
    data map<text, text>,
    PRIMARY KEY ((category), price, id));

CREATE TABLE IF NOT EXISTS orderskeyspace.orders_by_customer (
    id int,
    customer text,
    order_date timestamp,
    total_sum float,
    items list<tuple<text, int>>,
    PRIMARY KEY ((customer), order_date, id));

CREATE TABLE IF NOT EXISTS reviewskeyspace.reviews_by_item (
    id int,
    customer text,
    rating int,
    item_id tuple<text, int>,
    PRIMARY KEY ((item_id), rating, customer, id));