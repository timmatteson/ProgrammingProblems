
#!/bin/python3

import math
import os
import random
import re
import sys

# Complete the compareTriplets function below.
def compareTriplets(a, b):
    ret = [sum(i) for i in zip(*map(score, zip(a,b)))]
    return ret

def score(s):
    if (s[0] > s[1]):
        return (1, 0)
    if (s[0] < s[1]):
        return (0, 1)
    return (0, 0)

if __name__ == '__main__':
    fptr = sys.stdout

    a = list(map(int, input().rstrip().split()))

    b = list(map(int, input().rstrip().split()))

    result = compareTriplets(a, b)

    fptr.write(' '.join(map(str, result)))
    fptr.write('\n')

    fptr.close()
