/*
Write a function that takes a positive integer and returns the next smaller positive integer containing the same digits.

For example:

next_smaller(21) == 12
next_smaller(531) == 513
next_smaller(2071) == 2017
Return -1 (for Haskell: return Nothing, for Rust: return None), when there is no smaller number that contains the same digits. Also return -1 when the next smaller number with the same digits would require the leading digit to be zero.

next_smaller(9) == -1
next_smaller(135) == -1
next_smaller(1027) == -1  # 0721 is out since we don't write numbers with leading zeros
some tests will include very large numbers.
test data only employs positive integers.
The function you write for this challenge is the inv

https://www.codewars.com/kata/5659c6d896bc135c4c00021e
*/
using System;
using System.Linq;
using System.Collections.Generic;

public class Kata
{
  public static long NextSmaller(long value)
  {
      var digits = value.ToString().ToCharArray().Select(x => int.Parse(x.ToString()));
      int index = digits.Count() - 1;
      long ret = -1;
      int length = value.ToString().Length;
  
      while (index >= 0)
      {
          long test = MakeNextSmaller(digits, index);
          if (test > -1 && test < value && test.ToString().Length == length)
          {
              digits = test.ToString().ToCharArray().Select(x => int.Parse(x.ToString()));
              if (ret < test || ret == -1)
              {
                  ret = test;
              }
          }
          index--;
      }
      return ret;
  }
  
  private static long MakeNextSmaller(IEnumerable<int> value, int currentIndex)
  {
      int[] newValue = null;
      int level = value.ElementAt(currentIndex);
      var tail = value.Skip(currentIndex + 1);
      int candidate = -1;
  
      try
      {
          candidate = tail.OrderByDescending(x => x).First(x => x < level);
      }
      catch { }
      
      if (candidate != -1)
      {
          Console.WriteLine(candidate);
          int swap = tail.ToList().IndexOf(candidate) + 1;
  
          newValue = value.ToArray();
          newValue[currentIndex] = candidate;
          newValue[currentIndex + swap] = level;
  
          int[] newTail = newValue.Skip(currentIndex + 1).OrderByDescending(x => x).ToArray();
          newTail.CopyTo(newValue, currentIndex + 1);
  
      }
  
      return (newValue == null ? -1 : ArrayToInt(newValue));
  }
  
  private static long ArrayToInt(int[] input)
  {
      long ret = 0;
      int cnt = 0;
      for (int i = input.Length - 1; i >= 0; i--)
      {
          ret += input[i] * (long) Math.Pow(10, cnt);
          cnt++;
      }
      return ret;
  }
}