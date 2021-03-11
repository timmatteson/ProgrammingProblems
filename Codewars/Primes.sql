/*
Write a SELECT query which will return all prime numbers smaller than 100 in ascending order.

Your query should return one column named prime.

https://www.codewars.com/kata/59be9f425227ddd60c00003b
*/


WITH RECURSIVE primes AS 
(
  SELECT
    2 AS prime
  UNION ALL
  SELECT 
    prime + 1
  FROM primes
  WHERE prime < 100
)
SELECT 
  p.prime 
FROM 
  primes p
  LEFT OUTER JOIN primes p2
  ON p.prime > p2.prime AND p.prime % p2.prime = 0 
WHERE 
  p2.prime IS NULL