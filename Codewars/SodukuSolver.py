#Write a function that will solve a 9x9 Sudoku puzzle. The function will take one argument consisting of the 2D puzzle array, with the value 0 representing an unknown square.
#https://www.codewars.com/kata/5296bc77afba8baa690002d7

def getSquareOptions(board, x, y):
    posX = (x // 3) * 3
    posY = (y // 3) * 3
    known = [board[i][j] for i in range(posX,posX + 3) for j in range(posY, posY + 3) if board[i][j] > 0]
    results = [x for x in range(1,10) if x not in known]

    return results

def getHorizontalOptions(board, x, y):
    posX = x
    posY = 0
    known = [board[x][i] for i in range(posY,posY + 9) if board[x][i] > 0]
    results = [x for x in range(1,10) if x not in known]

    return results

def getVerticalOptions(board, x, y):
    posX = 0
    posY = y
    known = [board[i][y] for i in range(posX,posX + 9) if board[i][y] > 0]
    results = [x for x in range(1,10) if x not in known]

    return results

def getPossibles(board, x, y):
    possibles = list(set(getSquareOptions(board, x, y)) & set(getHorizontalOptions(board, x, y)) & set(getVerticalOptions(board, x, y)))
    
    return possibles

def sudoku(puzzle):

    while True:
        cnt = 0

        for i in range(0, len(puzzle[0])):
            for j in range(0, len(puzzle)):
                if (puzzle[i][j] == 0):
                    cnt+=1
                    temp = getPossibles(puzzle, i, j)
                    if len(temp) == 1:
                        puzzle[i][j] = temp[0]
                        print(temp)
        if cnt == 0:
            break

    return puzzle