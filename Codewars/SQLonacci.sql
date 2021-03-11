/*
Yes it's Fibonacci yet again ! But this time it's SQL.

You need to create a select statement which will produce first 90 Fibonnacci numbers. The column name is - number

Fibbonaccii sequence is:

 0, 1, 1, 2, 3, 5, 8, 13, ..., 89, 144, 233, 377, ...
where

f(0) = 0
f(1) = 1
...
f(n) = f(n-1) + f(n-2)
Have fun!

https://www.codewars.com/kata/59821d485a49f4d71f00000b
*/

WITH RECURSIVE fibonacci AS 
(
  SELECT 
    1 AS count,
    0::BIGINT AS first,
    1::BIGINT AS second,
    0::BIGINT AS number
  UNION ALL
  SELECT 
    count + 1,
    second,
    number,
    second + number
  FROM 
    fibonacci
  WHERE 
    count < 90
)

SELECT 
  F1.number 
FROM fibonacci F1
ORDER BY 
F1.count