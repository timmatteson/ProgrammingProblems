"""
Laura Bassi was the first female professor at a European university.
Despite her immense intellect, she was not always allowed to lecture publicly.

One day a professor with very strong beliefs against women in academia sent some agents to find Bassi and end her career.

Help her escape by telling her the safest places in town!

Task
Implement the function advice(agents, n) where

agents is an array of agent coordinates.
n defines the size of the city that Bassi needs to hide in, in other words the side length of the square grid.
The function should return a list of coordinates that are the furthest away (by Manhattan distance) from all agents.

As an example, say you have a 6x6 map, and agents at locations

[(0, 0), (1, 5), (5, 1)]
The distances to the nearest agent look like this.

The safest spaces are the ones with distance 4, marked in bright red. So the function should return

[(2, 2), (3, 3), (4, 4), (5, 5)]
in any order.

Edge cases:

If there is an agent on every grid cell, there is no safe space, so return an empty list.
If there are no agents, then every cell is a safe spaces, so return all coordinates.
if n is 0, return an empty list.
If agent coordinates are outside of the map, they are simply not considered.
There are no duplicate agents on the same square.
Performance
All reference solutions run in around 6 seconds. You might not pass the tests if you use a brute-force solution.

There are 200 random tests with n <= 50. Inefficient solutions might time out.

This kata is inspired by ThoughtWorks' coding challenge

#https://www.codewars.com/kata/5dd82b7cd3d6c100109cb4ed
"""
import time

current_milli_time = lambda: int(round(time.time() * 1000))

def advice(agents, n):
    start = current_milli_time()
    print(current_milli_time() - start)
    maxRisk = 0
    inCity = list(filter(lambda x: x[0] < n and x[1] < n, agents))
    locations =  sorted([(i,j) for i in range(n) for j in range(n) if (i, j) not in inCity], reverse=True)
    grid = [] if inCity else locations
    print(current_milli_time() - start)

    if inCity:
        for location in locations:
            curRisk = -1

            for agent in inCity:
                temp = abs(agent[0] - location[0])  + abs(agent[1] - location[1])

                if temp < maxRisk and maxRisk > 0:
                    curRisk = 0
                    break
                else:
                    if temp < curRisk or curRisk == -1: curRisk = temp
            
            if curRisk >= maxRisk:
                grid.append((location[0], location[1], curRisk))
                maxRisk = curRisk
        
        grid = list(map(lambda x: (x[0], x[1]), list(filter(lambda y: y[2] == maxRisk, grid))))
    
    print(current_milli_time() - start)

    return sorted(grid)