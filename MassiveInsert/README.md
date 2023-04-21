# Lost Update Solutions Test
Uncomment the test you want to run.

## Lost Update Test ~ 1min 20s
As we can see lost update problem happens. Instead of 100 000, we get 10 354
![lost_update_10_x_10000](https://user-images.githubusercontent.com/25819135/233636420-0551c1b7-f07c-4f3a-9622-b6aa20f55d8e.png)

## In-place Update Test ~ 1min 30s
In-place update solves the lost update issue and is the MOST PERFORMANT solution. The drawback is, that it's not always possible to do all the changes in place.
![inplace_update_10_x_10000](https://user-images.githubusercontent.com/25819135/233637690-3de4902b-485d-49c0-8957-faf97900a464.png)

## Row-level Locking Update Test ~ 2min 40s
Row-level locking solves the lost update issue, but is not as quick as in-place update.
![row_locking_update_10_x_10000](https://user-images.githubusercontent.com/25819135/233638247-02c28da1-14a7-4868-b5ec-b5fcdb75c417.png)

## Optimistic Concurrency Update Test
### Optimistic Concurrency With Version Column
Optimistic concurrency with 2 threads and 50 000 iterations per each is a way faster than 10 threads with 10 000 iterations each. So, we can see the prove that Optimistic Concurrency With Version Column has performance degradation with big number of concurrent updates.
#### 10 Threads with 10 000 iterations ~ 40min
![optimistic_concurrency_10_x_10000](https://user-images.githubusercontent.com/25819135/233638991-60bc441e-23fd-4a5b-9dea-df45ac8c1012.png)
#### 2 Threads with 50 000 iterations ~ 4min 20s
![optimistic_concurrency_2_x_50000](https://user-images.githubusercontent.com/25819135/233639349-20470538-d24a-4214-8b99-71f5c3379501.png)
