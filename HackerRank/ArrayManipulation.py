import math
import os
import random
import re
import sys

# Complete the arrayManipulation function below.
def arrayManipulation(n, queries):
    matrix = [0] * (n + 2)
    ret = 0
    cnt = 0

    for q in queries:
        matrix[q[0]] += q[2]
        matrix[q[1]+1] -= q[2]
            
    for i in sorted(matrix, reverse=True):
        cnt += i
        ret = max(cnt, ret)

    return ret

if __name__ == '__main__':
    fptr = sys.stdout

    nm = input().split()

    n = int(nm[0])

    m = int(nm[1])

    queries = []

    for _ in range(m):
        queries.append(list(map(int, input().rstrip().split())))

    result = arrayManipulation(n, queries)

    fptr.write(str(result) + '\n')

    fptr.close()
