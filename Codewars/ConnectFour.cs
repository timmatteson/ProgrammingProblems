/*
Take a look at wiki description of Connect Four game:

Wiki Connect Four

The grid is 6 row by 7 columns, those being named from A to G.

You will receive a list of strings showing the order of the pieces which dropped in columns:

  pieces_position_list = ["A_Red",
                          "B_Yellow",
                          "A_Red",
                          "B_Yellow",
                          "A_Red",
                          "B_Yellow",
                          "G_Red",
                          "B_Yellow"]
The list may contain up to 42 moves and shows the order the players are playing.

The first player who connects four items of the same color is the winner.

You should return "Yellow", "Red" or "Draw" accordingly.
https://www.codewars.com/kata/56882731514ec3ec3d000009
*/

using System;
using System.Collections.Generic;

public class ConnectFour
{
  public static string WhoIsWinner(List<string> piecesPositionList)
  {
      int[,] board = new int[6,7];
  
      Dictionary<string, int> map = new Dictionary<string, int>()
      {
          {"A", 0},
          {"B", 1},
          {"C", 2},
          {"D", 3},
          {"E", 4},
          {"F", 5},
          {"G", 6}
      };
  
      foreach (string move in piecesPositionList)
      {
          int column = map[move.Split("_")[0]];
          int color = (move.Split("_")[1] == "Red") ? 1 : 2;
  
          int row = board.GetUpperBound(0);
  
          while (board[row, column] != 0 && row >= 0)
          {
              row--;
          }
  
          board[row, column] = color;
          PrintBoard(board);
  
          if (Score(board, row, column, color, 4) > 0)
              return (color == 1) ? "Red" : "Yellow";
      }
  
      return "Draw";
  }
  
  private static int Score(int[,] board, int row, int column, int piece, int threshold)
  {
      int score = 0;
  
      score = CountHorizontal(board, row, column, piece);
      if (score >= threshold) return score;
  
      score = CountVertical(board, row, column, piece);
      if (score >= threshold) return score;
  
      score = CountDiagonalRightDown(board, row, column, piece);
      if (score >= threshold) return score;
       
      score = CountDiagonalLeftDown(board, row, column, piece);
      if (score >= threshold) return score;
  
      return 0; 
  }
  
  private static int CountVertical(int[,] board, int row, int column, int piece)
  {
      int cnt = 1;
      int i = row - 1;
  
      while (i >= 0 && board[i, column] == piece)
      {
          cnt++;
          i--;
      }
  
      i = row + 1;
      while (i <= board.GetUpperBound(0) && board[i, column] == piece)
      {
          cnt++;
          i++;
      }
      return cnt;
  }
  private static int CountHorizontal(int[,] board, int row, int column, int piece)
  {
      int cnt = 1;
      int i = column - 1;
  
      while (i >= 0 && board[row, i] == piece)
      {
          cnt++;
          i--;
      }
  
      i = column + 1;
      while (i <= board.GetUpperBound(1) && board[row, i] == piece)
      {
          cnt++;
          i++;
      }
      return cnt;
  }
  private static int CountDiagonalLeftDown(int[,] board, int row, int column, int piece)
  {
      int cnt = 1;
      int i = row - 1;
      int j = column - 1;
  
      while (i >= 0 && j >= 0 && board[i, j] == piece)
      {
          cnt++;
          i--;
          j--;
      }
  
      i = row + 1;
      j = column + 1;
  
      while (i <= board.GetUpperBound(0) && j <= board.GetUpperBound(1) && board[i, j] == piece)
      {
          cnt++;
          i++;
          j++;
      }
      return cnt;
  }
  private static int CountDiagonalRightDown(int[,] board, int row, int column, int piece)
  {
      int cnt = 1;
      int i = row - 1;
      int j = column + 1;
  
      while (i >= 0 && j <= board.GetUpperBound(1) && board[i, j] == piece)
      {
          cnt++;
          i--;
          j++;
      }
  
      i = row + 1;
      j = column - 1;
  
      while (i <= board.GetUpperBound(0) && j >= 0 && board[i, j] == piece)
      {
          cnt++;
          i++;
          j--;
      }
      return cnt;
  }
  
  
  
  public static void PrintBoard(int[,] board)
  {
      string printBoard = string.Empty;
      for (int i = 0; i <= board.GetUpperBound(0); i++)
      {
          printBoard += "| ";
          for (int j = 0; j <= board.GetUpperBound(1); j++)
          {
              printBoard += board[i, j].ToString() + " ";
          }
          printBoard += " |" + Environment.NewLine;
      }
      Console.WriteLine(printBoard);
  }
}