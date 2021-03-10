/*
Description
Given a posts table that contains a created_at timestamp column write a query that returns a first date of the month, a number of posts created in a given month and a month-over-month growth rate.

The resulting set should be ordered chronologically by date.

Note:

percent growth rate can be negative
percent growth rate should be rounded to one digit after the decimal point and immediately followed by a percent symbol "%". See the desired output below for the reference.
Desired Output
The resulting set should look similar to the following:

date       | count | percent_growth
-----------+-------+---------------
2016-02-01 |   175 |           null
2016-03-01 |   338 |          93.1%
2016-04-01 |   345 |           2.1%
2016-05-01 |   295 |         -14.5%
2016-06-01 |   330 |          11.9%
...
date - (DATE) a first date of the month
count - (INT) a number of posts in a given month
percent_growth - (TEXT) a month-over-month growth rate expressed in percents

https://www.codewars.com/kata/589e0837e10c4a1018000028
*/

SELECT
  date_trunc('MONTH',currentPosts.created_at)::DATE as date,
  COUNT(currentPosts.created_at) as count,
  trim(to_char(ROUND(100 * (COUNT(currentPosts.created_at)::decimal / previousPosts.count::decimal) - 100, 1), '990D9%'))  as percent_growth
FROM posts as currentPosts
  LEFT OUTER JOIN 
    (SELECT
      date_trunc('MONTH',P.created_at)::DATE as date,
      COUNT(P.created_at) as count
    FROM 
      posts as P
    GROUP BY 
      date_trunc('MONTH',P.created_at)::DATE
    ) AS previousPosts
  ON date_trunc('MONTH',currentPosts.created_at)::DATE = previousPosts.date + INTERVAL'1 month'
GROUP BY
  date_trunc('MONTH',currentPosts.created_at)::DATE,
  previousPosts.count
ORDER BY
  date_trunc('MONTH',currentPosts.created_at)::DATE