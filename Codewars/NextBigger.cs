/*
Create a function that takes a positive integer and returns the next bigger number that can be formed by rearranging its digits. For example:

12 ==> 21
513 ==> 531
2017 ==> 2071
nextBigger(num: 12)   // returns 21
nextBigger(num: 513)  // returns 531
nextBigger(num: 2017) // returns 2071
If the digits can't be rearranged to form a bigger number, return -1 (or nil in Swift):

9 ==> -1
111 ==> -1
531 ==> -1
nextBigger(num: 9)   // returns nil
nextBigger(num: 111) // returns nil
nextBigger(num: 531) // returns nil
https://www.codewars.com/kata/55983863da40caa2c900004e
*/

using System;
using System.Linq;
using System.Collections.Generic;

public class Kata
{

  public static long NextBiggerNumber(long value)
  {
      var digits = value.ToString().ToCharArray().Select(x => int.Parse(x.ToString()));
      int index = digits.Count() - 1;
      long ret = -1;
      int length = value.ToString().Length;
  
      while (index >= 0)
      {
          long test = MakeNextLarger(digits, index);
          if (test > -1 && test > value && test.ToString().Length == length)
          {
              digits = test.ToString().ToCharArray().Select(x => int.Parse(x.ToString()));
              if (ret > test || ret == -1)
              {
                  ret = test;
              }
          }
          index--;
      }
      return ret;
  }
  
  private static long MakeNextLarger(IEnumerable<int> value, int currentIndex)
  {
      int[] newValue = null;
      int level = value.ElementAt(currentIndex);
      var tail = value.Skip(currentIndex + 1);
      int candidate = -1;
  
      try
      {
          candidate = tail.OrderBy(x => x).First(x => x > level);
      }
      catch { }
      
      if (candidate != -1)
      {
          Console.WriteLine(candidate);
          int swap = tail.ToList().IndexOf(candidate) + 1;
  
          newValue = value.ToArray();
          newValue[currentIndex] = candidate;
          newValue[currentIndex + swap] = level;
  
          int[] newTail = newValue.Skip(currentIndex + 1).OrderBy(x => x).ToArray();
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