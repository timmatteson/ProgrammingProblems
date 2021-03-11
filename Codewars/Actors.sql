/*
Given the the schema presented below find two actors who cast together the most and list titles of only those movies they were casting together. Order the result set alphabetically by the movie title.

Table film_actor
 Column     | Type                        | Modifiers
------------+-----------------------------+----------
actor_id    | smallint                    | not null
film_id     | smallint                    | not null
...
Table actor
 Column     | Type                        | Modifiers
------------+-----------------------------+----------
actor_id    | integer                     | not null 
first_name  | character varying(45)       | not null
last_name   | character varying(45)       | not null
...
Table film
 Column     | Type                        | Modifiers
------------+-----------------------------+----------
film_id     | integer                     | not null
title       | character varying(255)      | not null
...
The desired output:
first_actor | second_actor | title
------------+--------------+--------------------
John Doe    | Jane Doe     | The Best Movie Ever
...
first_actor - Full name (First name + Last name separated by a space)
second_actor - Full name (First name + Last name separated by a space)
title - Movie title

https://www.codewars.com/kata/5818bde9559ff58bd90004a2
*/

SELECT  
    a.first_actor,
    a.second_actor,
    b.title
FROM
  (SELECT 
    COUNT(film.film_id),
    actor.first_name || ' ' || actor.last_name as first_actor,
    a2.first_name || ' ' || a2.last_name as second_actor,
    actor.actor_id as first_actor_id,
    a2.actor_id as second_actor_id
  FROM 
    film 
    INNER JOIN film_actor 
    ON film.film_id = film_actor.film_id
    INNER JOIN actor 
    ON film_actor.actor_id = actor.actor_id
    INNER JOIN film_actor f2
    ON film.film_id = f2.film_id
    AND f2.actor_id > actor.actor_id
    INNER JOIN actor a2
    ON f2.actor_id = a2.actor_id
  GROUP BY 
    actor.first_name || ' ' || actor.last_name,
    a2.first_name || ' ' || a2.last_name,
    actor.actor_id,
    a2.actor_id
  ORDER BY 
    COUNT(film.film_id) DESC
  LIMIT 1) a
  INNER JOIN 
  (SELECT 
    film.film_id,
    film.title,
    actor.actor_id as first_actor_id,
    a2.actor_id as second_actor_id
  FROM 
    film 
    INNER JOIN film_actor 
    ON film.film_id = film_actor.film_id
    INNER JOIN actor 
    ON film_actor.actor_id = actor.actor_id
    INNER JOIN film_actor f2
    ON film.film_id = f2.film_id
    AND f2.actor_id > actor.actor_id
    INNER JOIN actor a2
    ON f2.actor_id = a2.actor_id) b
  ON a.first_actor_id = b.first_actor_id AND
    a.second_actor_id = b.second_actor_id 
  ORDER BY b.title