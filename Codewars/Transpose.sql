/*
Given a table with a following schema

   Table "public.matrices"
 Column |  Type  | Modifiers
--------+--------+-----------
 matrix | text[] | not null
which holds a set of two-dimensional text arrays i.e.

      matrix
-------------------
 {{1,2,3},{4,5,6}}
 {{a,b,c},{d,e,f}}
(2 rows)
your goal is to wite a SELECT statement or a CTE that returns array data in a transposed form

       matrix
---------------------
 {{1,4},{2,5},{3,6}}
 {{a,d},{b,e},{c,f}}
(2 rows)
You can't use / create user definded functions and resort to procedural PL/pgSQL.
https://www.codewars.com/kata/592b1e4c96cc12de1e0000b1
*/


WITH results AS  
(
SELECT 
    (row_number() OVER (PARTITION BY m.matrixid)) as rowid,
    (row_number() OVER (PARTITION BY m.matrixid) + m.upper_bound - 1) % m.upper_bound as itemid,
    m.item,
    m.upper_bound,
    m.matrixid,
    m.original
  FROM 
    (SELECT 
      row_number() OVER (ORDER BY 1) as matrixid,
      matrix as original,
      array_upper(matrix, 2) as upper_bound,
      unnest(matrix) as item
    FROM 
      public.matrices 
    ) m
)
SELECT matrix FROM (
 SELECT DISTINCT ARRAY(
 SELECT a5.array1 FROM
   (SELECT DISTINCT array1, a4.matrixid, a4.itemid FROM
     (SELECT DISTINCT 
       ARRAY(SELECT a3.item FROM results a3 WHERE a3.itemid = a1.itemid AND a3.matrixid = a1.matrixid) AS array1,
       a1.item, a1.itemid, a1.matrixid, a1.rowid
     FROM
      results a1
      INNER JOIN results a2
      ON a1.itemid = a2.itemid AND a1.matrixid = a2.matrixid
    ORDER BY 
      a1.matrixid, a1.itemid, a1.rowid) a4
    WHERE
      a6.matrixid = a4.matrixid
    ORDER BY a4.matrixid, a4.itemid) a5
    )  AS matrix, a6.matrixid
  FROM 
    results a6
  ORDER BY 
    a6.matrixid) a7