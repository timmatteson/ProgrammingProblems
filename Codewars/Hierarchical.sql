/*
For this challenge you need to create a RECURSIVE Hierarchical query. You have a table employees of employees, you must order each employee by level. You must use a WITH statement and name it employee_levels after that has been defined you must select from it.

A Level is in correlation what manager managers the employee. e.g. an employee with a manager_id of NULL is at level 1 and then direct employees with the employee at level 1 will be level 2.

employees table schema
id
first_name
last_name
manager_id (can be NULL)
resultant schema
level
id
first_name
last_name
manager_id (can be NULL)
NOTE: refer to the Results: expected table if you're stuck with how it should be displayed.

https://www.codewars.com/kata/5812a2a2492760dfca000450
*/

WITH RECURSIVE EMPLOYEE_LEVELS AS (
   SELECT
      1 as level,
      employees.id,
      first_name,
      last_name,
      manager_id
   FROM
      employees
   WHERE 
     manager_id IS NULL
   UNION ALL
      SELECT
         r.level + 1,
         e.id,
         e.first_name,
         e.last_name,
         e.manager_id
      FROM
         employees e
      INNER JOIN  EMPLOYEE_LEVELS r ON e.manager_id = r.id
) 
SELECT
    level,
    id,
    first_name,
    last_name,
    manager_id
FROM
    EMPLOYEE_LEVELS
GROUP BY 
  level,
  id,
  first_name,
  last_name,
  manager_id
ORDER BY 
  level, id
;