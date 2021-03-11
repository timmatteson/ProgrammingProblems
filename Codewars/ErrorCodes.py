"""
A system is transmitting messages in binary, however it is not a perfect transmission, and sometimes errors will occur which result in a single bit flipping from 0 to 1, or from 1 to 0.

To resolve this, A 2-dimensional Parity Bit Code is used: https://en.wikipedia.org/wiki/Multidimensional_parity-check_code

In this system, a message is arrayed out on a M x N grid. A 24-bit message could be put on a 4x6 grid like so:

1 0 1 0 0 1
1 0 0 1 0 0
0 1 1 1 0 1
1 0 0 0 0 1

Then, Parity bits are computed for each row and for each column, equal to 1 if there is an odd number of 1s in the row of column, and 0 if it is an even number. The result for the above message looks like:

1 0 1 0 0 1 1
1 0 0 1 0 0 0
0 1 1 1 0 1 0
1 0 0 0 0 1 0
1 1 0 0 0 1

Since the 1st row, and 1st, 2nd and 6th column each have an odd number of 1s in them, and the others all do not.

Then the whole message is sent, including the parity bits. This is arranged as:

message + row_parities + column_parities
101001100100011101100001 + 1000 + 110001
1010011001000111011000011000110001

If an error occurs in transmission, the parity bit for a column and row will be incorrect, which can be used to identify and correct the error. If a row parity bit is incorrect, but all column bits are correct, then we can assume the row parity bit was the one flipped, and likewise if a column is wrong but all rows are correct.

Your Task:
Create a function correct, which takes in integers M and N, and a string of bits where the first M*N represent the content of the message, the next M represent the parity bits for the rows, and the final N represent the parity bits for the columns. A single-bit error may or may not have appeared in the bit array.

The function should check to see if there is a single-bit error in the coded message, correct it if it exists, and return the corrected string of bits.

https://www.codewars.com/kata/5d870ff1dc2362000ddff90b
"""
import collections
import sys
from functools import reduce

def get_rows(l, m, n):
    for i in range(0, m * n, n):
        yield l[i:i + n]

def get_cols(l, m, n):
    for i in range(0, n):
        yield l[i:m*n:n]


def index_of_first_mismatch(l1, l2):
    for i in range(len(l1)):
        if l1[i] != l2[i]:
             return i
    return -1

def correct(m, n, bits):
    start = m*n
    grid = list(map(lambda x: int(x), bits))

    rowCheckSum = list(grid[start:start+m])
    colCheckSum = list(grid[start+m:start+m+n])
    rowTotal = list(map(lambda x: sum(y for y in x) % 2, get_rows(grid, m, n)))
    colTotal = list(map(lambda x: sum(y for y in x) % 2, get_cols(grid, m, n)))
    
    r = index_of_first_mismatch(rowCheckSum, rowTotal)
    c = index_of_first_mismatch(colCheckSum, colTotal)
    
    if r > -1 and c < 0:
        grid[start+r] = abs(grid[start+r] - 1)
    if r < 0 and c > -1:
        grid[start+m+c] = abs(grid[start+m+c] - 1)
    if r > -1 and c > -1:
        index = r*n+c

        grid[index] = abs(grid[index] - 1)

    return ''.join(map(str,grid))