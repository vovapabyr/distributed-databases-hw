UPDATE ecommerce.items_by_category SET data['camera'] = '14mp' WHERE category='Phone' AND price = 1000 AND id = 3;

UPDATE ecommerce.items_by_category SET data['water_resistent'] = 'true' WHERE category='Phone' AND price = 1000 AND id = 3;

DELETE data['weight'] FROM ecommerce.items_by_category WHERE category='Phone' AND price = 1000 AND id = 3;