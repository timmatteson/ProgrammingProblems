#!/bin/python3

import math
import os
import random
import re
import sys
import copy

#
# Complete the 'climbingLeaderboard' function below.
#
# The function is expected to return an INTEGER_ARRAY.
# The function accepts following parameters:
#  1. INTEGER_ARRAY ranked
#  2. INTEGER_ARRAY player
#

def climbingLeaderboard(ranked, player):
    results = []
    ranking = list(set(ranked))
    ranking.sort(reverse=True)
    bound = len(ranking)
    tail = ranking[bound - 1]
    head = ranking[0]
    currentRank = bound
    last = head
    
    for i in range(len(player)):
        score = player[i]
        if (score < tail): 
            results.append(bound + 1)
        elif (score >= head):
            results.append(1)
        else:
            for j in reversed(range(currentRank)):
                rank = ranking[j]
                if (score == last or (score > last and score < rank)):
                    currentRank = j + 1
                    results.append(j + 2)       
                    break
                last = rank

    return results

    


if __name__ == '__main__':
    fptr = sys.stdout

    ranked_count = int(input().strip())

    ranked = list(map(int, input().rstrip().split()))

    player_count = int(input().strip())

    player = list(map(int, input().rstrip().split()))

    result = climbingLeaderboard(ranked, player)

    fptr.write('\n'.join(map(str, result)))
    fptr.write('\n')

    fptr.close()
