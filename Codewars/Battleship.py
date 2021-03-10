#Write a method that takes a field for well-known board game "Battleship" as an argument and returns true if it has a valid disposition of ships, 
#false otherwise. Argument is guaranteed to be 10*10 two-dimension array. Elements in the array are numbers, 0 if the cell is free and 1 if occupied by ship.
#https://www.codewars.com/kata/52bb6539a4cf1b12d90005b7

def rotate45(array):
    ret = []

    for i in range(len(array)):
        ret.append([])
        cnt = i

        for j in range(i + 1):
            ret[i].append(array[cnt][j])
            cnt -= 1
    
    for i in range(1, len(array)):
        ret.append([])
        cnt = len(array) - 1

        for j in range(i, len(array)):
            ret[i + len(array) - 1].append(array[cnt][j])
            cnt -= 1

    return ret


def count_ships(field, shipCount):
    curShip = 0

    for i in field:
        for j in i:
            if j == 1:
                curShip += 1
            else:
                if curShip > 0: shipCount[curShip] += 1 
                curShip = 0
        
        if curShip > 0:
            shipCount[curShip] += 1
            curShip = 0

def validate_battlefield(field):
    valid = True
    bound = len(field)

    shipsX = [0] * (bound + 1)
    shipsY =  [0] * (bound + 1)
    ships45 =  [0] * (bound + 1)
    ships135 = [0] * (bound + 1)

    count_ships(field, shipsX)
    count_ships(rotate45(field), ships45)
    count_ships(zip(*field[::-1]), shipsY)
    count_ships(rotate45(list(zip(*field[::-1]))), ships135)

    sumX = sum(shipsX[x] * x for x in range(2, 5))
    sumY = sum(shipsY[x] * x for x in range(2, 5))

    valid = (sumX > 0 and shipsY[1] - sumX == 4) or (sumY > 0 and shipsX[1] - sumY == 4) or (sumX == 0 and shipsY[1] == 4) or  (sumY == 0 and shipsX[1] == 4)
    ships = [x + y for x,y in zip(shipsX, shipsY)]
    valid = valid and ships[2] == 3
    valid = valid and ships[3] == 2
    valid = valid and ships[4] == 1
    valid = valid and sum(ships[x] for x in range(5, 11)) == 0
    valid = valid and sum(ships45[x] for x in range(2, 11)) == 0
    valid = valid and sum(ships135[x] for x in range(2, 11)) == 0

    return valid



battleField = [[1, 0, 1, 1, 1, 0, 0, 0, 0, 0],
                [0, 0, 0, 0, 0, 0, 0, 1, 1, 0],
                [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                [0, 0, 0, 0, 0, 1, 1, 0, 0, 1],
                [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                [0, 1, 1, 1, 1, 0, 1, 0, 1, 0],
                [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                [0, 0, 0, 0, 0, 0, 1, 1, 1, 0],
                [1, 1, 0, 0, 0, 0, 0, 0, 0, 0]]

print(validate_battlefield(battleField));
