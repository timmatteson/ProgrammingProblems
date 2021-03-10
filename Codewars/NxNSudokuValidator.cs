/*
Given a Sudoku data structure with size NxN, N > 0 and √N == integer, write a method to validate if it has been filled out correctly.

The data structure is a multi-dimensional Array, i.e:

[
  [7,8,4,  1,5,9,  3,2,6],
  [5,3,9,  6,7,2,  8,4,1],
  [6,1,2,  4,3,8,  7,5,9],
  
  [9,2,8,  7,1,5,  4,6,3],
  [3,5,7,  8,4,6,  1,9,2],
  [4,6,1,  9,2,3,  5,8,7],
  
  [8,7,6,  3,9,4,  2,1,5],
  [2,4,3,  5,6,1,  9,7,8],
  [1,9,5,  2,8,7,  6,3,4]
]
Rules for validation

Data structure dimension: NxN where N > 0 and √N == integer
Rows may only contain integers: 1..N (N included)
Columns may only contain integers: 1..N (N included)
'Little squares' (3x3 in example above) may also only contain integers: 1..N (N included)
https://www.codewars.com/kata/540afbe2dc9f615d5e000425
*/

using System;
using System.Linq;

class Sudoku
{
  private int[][] _sudokuData;

  public Sudoku(int[][] sudokuData)
  {
      _sudokuData = sudokuData;
  }

  public bool IsValid()
  {
      return ValidateSolution(_sudokuData);
  }

  public static bool ValidateSolution(int[][] board)
  {
      int n = board.GetUpperBound(0) + 1;
      int? square = IntSquareRoot(n);
      int grid = square.GetValueOrDefault();

      //no square root of N is int, invalid board.
      if (square == null) return false;

      for (int i = 0; i < n; i++)
      {
          //Board not square.
          if (board[i].GetUpperBound(0) != n - 1) return false;

          for (int j = 0; j < n; j++)
          {
              if ((i + j == 0) || (i % grid == 0 && j % grid == 0))
              {
                  if (!TestSodukuSeries(Transpose(board, j, i, j + (grid - 1), i + (grid - 1)), n)) return false;
              }
              if (i == 0)
              {
                  if (!TestSodukuSeries(Transpose(board, j, i, j, board.GetUpperBound(0)), n)) return false;
              }
          }

          if (!TestSodukuSeries(board[i], n)) return false;
      }


      return true;
  }

  public static int? IntSquareRoot(int test)
  {
      if (Math.Sqrt((double)test) % 1d == 0d)
          return ((int)Math.Sqrt(test));
      else
          return null;
  }

  public static bool TestSodukuSeries(int[] test, int bound)
  {
      for (int j = 1; j <= bound; j++)
      {
          var count = test.Count(x => x == j);
          if (count != 1) return false;
      }
      return true;
  }

  public static int[] Transpose(int[][] source, int startH, int startV, int endH, int endV)
  {
      int[] result = new int[(endH - startH + 1) * (endV - startV + 1)];
      int index = 0;

      for (int i = startH; i <= endH; i++)
      {
          for (int j = startV; j <= endV; j++)
          {
              result[index] = source[j][i];
              index++;
          }
      }
      return result;
  }
}