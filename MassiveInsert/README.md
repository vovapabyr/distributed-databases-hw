# Lost Update Solutions Test
All the tests are done at 'PostgreSQL 15.2 (Debian 15.2-1.pgdg110+1) on x86_64-pc-linux-gnu, compiled by gcc (Debian 10.2.1-6) 10.2.1 20210110, 64-bit' on Docker. 

Uncomment the test you want to run:
![image](https://user-images.githubusercontent.com/25819135/233640597-afcf3ba0-c56f-499f-8e05-f3d7e5b26dbc.png)

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
### Optimistic Concurrency With Automatic Lost Update Detection ~ 3min 40s
Another optimistic approach is to let database automaticaly detect lost updates (at least REPEATABLE READ isolation level required) and then retry failed transaction. On lost update detection we get next exception:
![automatic_detection_update_error](https://user-images.githubusercontent.com/25819135/233646775-5190f712-b5c3-4fbb-a25f-821c599491b3.png) which we need to handle and retry.
![automatic_detection_update_10_x_10000](https://user-images.githubusercontent.com/25819135/233646893-5531cfd4-797d-4fdd-a8f6-76f22208dd9d.png)
