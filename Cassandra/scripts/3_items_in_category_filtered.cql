SELECT * FROM ecommerce.items_by_category WHERE category='Phone' AND name='iPhone XS';

SELECT * FROM ecommerce.items_by_category WHERE category='Phone' AND price > 500 AND price < 1200;

SELECT * FROM ecommerce.items_by_category_producer WHERE category='Phone' AND price=600 AND producer='Apple';
