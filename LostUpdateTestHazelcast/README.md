# Lost Update Hazelcast Solutions Test
This project displays how can we get lost update problem in Hazelcast and different ways to fix it. Check [logs](logs) folder for logs.

## Lost Update Test ~ 11s ([log file](logs/lost-update~11sec.txt))
![image](https://user-images.githubusercontent.com/25819135/233843644-a00bce3e-989b-4634-97c3-9615a28f2e5a.png)

## Pesimistic Locking Update Test ~ 18min ([log18min file](logs/pesimistic-lock-10mil-delay~18min.txt), [log1h25min file](logs/pesimistic-lock-no-delay~1h-25min.txt))
![image](https://user-images.githubusercontent.com/25819135/233844434-bd5f2579-c8c7-4bde-b860-74dcdbb84173.png)
![image](https://user-images.githubusercontent.com/25819135/233844452-94874009-0c03-40db-983a-a4a4531f0959.png)

At first, lock on map didn't work for me, the counter was always less than 100_000 at the end of the update. Then, I came across the [locking in Hazelcast .Net client](https://github.com/hazelcast/hazelcast-csharp-client/blob/master/doc/dev/doc/locking.md) article, which explains the problem with async model in .Net and Hazelcast locking and offer the workaround for implmenting locks in async manner. So, basically each time we want to lock we should do:
```
using var context = AsyncContext.New()
await map.LockAsync("key")
```
New AsyncContext for each thread solved the issue, but there was sagnificant performance degradation (with no delays 10 concurrent threads executed 1h 25min!!!). Then, I executed 10 instances of the application in docker compose, each incrementing a counter 10_000 times with the same map.LockAsync. And we reach 100_000 counter value in 100ms!!! It looks like, there is a problem of using multiple AsyncContext in the same process.

## Optimistic Locking Update Test ~ 40s ([log file](logs/optimistic-lock-no-delay~45s.txt))
![image](https://user-images.githubusercontent.com/25819135/233844667-2499993c-1e7a-4a00-936a-daf934611001.png)

## CP Subsytem Atomic Long Test ~ 19s ([log file](logs/atomic-long~19s.txt), [cp log file](logs/hazelcast-node-cp-setup.txt))
![image](https://user-images.githubusercontent.com/25819135/233844870-8a2d321e-ddc5-468e-9cda-23c32c246029.png)
![image](https://user-images.githubusercontent.com/25819135/233844884-cca4dd30-0f42-49ef-a3dc-feb65959c2b2.png)
![image](https://user-images.githubusercontent.com/25819135/233844927-c4bb0081-7d2c-47ed-bb5b-a71bbdff9fe9.png)

