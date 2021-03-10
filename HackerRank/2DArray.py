#!/bin/python3

import math
import os
import random
import re
import sys


def hourglassSum(arr):
    high = sum([abs(x) for sub in arr for x in sub]) * -1

    for i in range(len(arr) - 2):
        for j in range(len(arr[i]) - 2):
            val = sum(arr[i][j : j + 3])
            val += arr[i + 1][j + 1]
            val += sum(arr[i + 2][j : j + 3])
            high = max(val, high)

    return high

if __name__ == '__main__':
    fptr = sys.stdout

    arr = []

    for _ in range(6):
        arr.append(list(map(int, input().rstrip().split())))

    result = hourglassSum(arr)

    fptr.write(str(result) + '\n')

    fptr.close()