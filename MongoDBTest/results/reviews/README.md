# Reviews mongosh script results
You can see that when we store one more review, the oldest review is replaced with the new review and number of reviews always stay 5

## 1 Create capped reviews collection
![1-create-capped-reviews](https://user-images.githubusercontent.com/25819135/234544314-019cba1a-8fa1-4f2f-b110-2871bc973af9.png)

## 2 Initial insert of 5 reviews
![2-insert-initial](https://user-images.githubusercontent.com/25819135/234544379-7e836839-1a4e-49ce-95b1-93b7429c386e.png)

## 3 Check that we have 5 results
![3-load-all](https://user-images.githubusercontent.com/25819135/234544452-3f1acc1e-e3f5-4fd0-a8c1-98b4d9dfcbf3.png)

## 4 Insert new review and check that the new review replaced the old one and number of reviews stay 5
![4-insert-new](https://user-images.githubusercontent.com/25819135/234544685-2151fb65-688a-4881-81f1-b6482ed81ab6.png)



