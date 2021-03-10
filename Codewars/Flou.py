"""
Work in progress!
n this kata, we'll play a mini-game Flou.



Task
You are given a gameMap, like this:

+----+
|.B..|    +,-,| --> The boundary of game map
|....|    B     --> The initial color block
|....|    .     --> The empty block
|..B.|    It is always a rectangular shape
+----+
In the test cases, it displayed like this:



The goal of the game is: move the initial color blocks, let each empty block became to color block.

Moving Rules
Each color block MUST/ONLY move once. The block should move at least one grid, otherwise, it is not a valid movement.
You need to specify the direction of the movement for each color block, the direction can be Left, Right, Up and Down.
The color block begins to move in the direction you specify, and each empty block on the path will turn into color. If an obstacle(border or another color block) is encountered, the moving direction will turn right 90 degrees, and the block will stop moving if there is still an obstacle after the turn.
For the gameMap above, we can let the purple block moving down:



Then, let the green block moving up:



In Python, the solution will be displayed in the console like this:

+----+
|aAbb|  # First colered block is designated 'A'
|aabb|
|aabb|
|aaBb|  # Second colored block is designated 'B', etc.
+----+
Ok, now all the blocks are colored. You win ;-)

So easy, right? ;-) Please write a nice solution to solve this kata. I'm waiting for you, code warrior ^_^

Output
Your output should be a 2D array, each subarray should contains 3 elements: [rowIndex, columnIndex, diretion]. rowIndex and columnIndex are 0-based, diretion should be one of "Left", "Right", "Up" and "Down".

For the gameMap above, the output should be [[0,1,"Down"],[3,2,"Up"]].

Not all test cases have a solution, if there is no solution, please return a boolean value false.

Examples and Note
You can submit the example solution(preloaded in the initial code) to see how it works ;-)

You also can play this game on the Web, here is the game link.

https://www.codewars.com/kata/5a93754d0025e98fde000048
"""

def make_move(map, position, move):
    return True

def solve_map(map, positions):
    moves = ["Left", "Right", "Up", "Down"]

    if len([(i,j) for i in range(0, len(map)) for j in range(0, len(map[i])) if map[i][j] == "."]) == 0:
        return True

    if len(positions) > 0:
        for p in positions:
            board[x][y] = p
            board[x][y] = 0
    else:
        return False

def play_flou(game_map):
    map = game_map.replace("+", "").replace("-", "").replace("|", "").splitlines()[1:]
    starts = [(i,j) for i in range(0, len(map)) for j in range(0, len(map[i])) if map[i][j] != "."]
    ret = []

    return ret

game_map = '''+----+
|B...|
|....|
|....|
|...B|
+----+'''

print(play_flou(game_map))
