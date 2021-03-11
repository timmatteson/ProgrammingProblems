/*
Description
Given the schema presented below write a query, which uses a LATERAL join, that returns two most viewed posts for every category.

Order the result set by:

category name alphabetically

number of post views largest to lowest

post id lowest to largest

Note:
Some categories may have less than two or no posts at all.
Two or more posts within the category can be tied by (have the same) the number of views. Use post id as a tie breaker - a post with a lower id gets a higher rank.
Schema
categories
 Column     | Type                        | Modifiers
------------+-----------------------------+----------
id          | integer                     | not null
category    | character varying(255)      | not null
posts
 Column     | Type                        | Modifiers
------------+-----------------------------+----------
id          | integer                     | not null
category_id | integer                     | not null
title       | character varying(255)      | not null
views       | integer                     | not null
Desired Output
The desired output should look like this:

category_id | category | title                             | views | post_id
------------+----------+-----------------------------------+-------+--------
5           | art      | Most viewed post about Art        | 9234  | 234
5           | art      | Second most viewed post about Art | 9234  | 712
2           | business | NULL                              | NULL  | NULL
7           | sport    | Most viewed post about Sport      | 10    | 126
...
category_id - category id
category - category name
title - post title
views - the number of post views
post_id - post id

https://www.codewars.com/kata/5820176255c3d23f360000a9
*/

SELECT 
  C.id as category_id,
  category,
  P.title, 
  P.views,
  P.post_id
FROM 
  categories C 
  LEFT JOIN LATERAL 
  (SELECT 
    title,
    views,
    category_id,
    id as post_id
   FROM posts 
   WHERE posts.category_id = C.id
   ORDER BY 
     views DESC, 
     id
   LIMIT 2) P
   ON true
ORDER BY 
  category,
  P.views DESC,
  P.post_id