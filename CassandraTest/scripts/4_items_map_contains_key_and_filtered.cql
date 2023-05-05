SELECT * FROM ecommerce.items_by_category WHERE data CONTAINS KEY 'size';

SELECT * FROM ecommerce.items_by_category WHERE data['size']='65';