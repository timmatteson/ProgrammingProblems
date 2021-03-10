/*
The year is 1214. One night, Pope Innocent III awakens to find the the archangel Gabriel floating before him. Gabriel thunders to the pope:

Gather all of the learned men in Pisa, especially Leonardo Fibonacci. In order for the crusades in the holy lands to be successful, these men must calculate the millionth number in Fibonacci's recurrence. Fail to do this, and your armies will never reclaim the holy land. It is His will.

The angel then vanishes in an explosion of white light.

Pope Innocent III sits in his bed in awe. How much is a million? he thinks to himself. He never was very good at math.

He tries writing the number down, but because everyone in Europe is still using Roman numerals at this moment in history, he cannot represent this number. If he only knew about the invention of zero, it might make this sort of thing easier.

He decides to go back to bed. He consoles himself, The Lord would never challenge me thus; this must have been some deceit by the devil. A pretty horrendous nightmare, to be sure.

Pope Innocent III's armies would go on to conquer Constantinople (now Istanbul), but they would never reclaim the holy land as he desired.

In this kata you will have to calculate fib(n) where:

fib(0) := 0
fib(1) := 1
fin(n + 2) := fib(n + 1) + fib(n)
Write an algorithm that can handle n up to 2000000.

Your algorithm must output the exact integer answer, to full precision. Also, it must correctly handle negative numbers as input.
https://www.codewars.com/kata/53d40c1e2f13e331fc000c26
*/

using System;
using System.Numerics;

public class Fibonacci
{

  public static BigInteger fib(int n)
  {
      if (n == 0) return BigInteger.Zero;
      if (n == 1 || n ==2 || n == -1) return BigInteger.One;
      if (n == -2) return -1 * BigInteger.One;

      int cnt = Math.Abs(n);
  
      BigInteger[,] m1 = { { BigInteger.One, BigInteger.One }, { BigInteger.One, BigInteger.Zero } };
      BigInteger[,] m2 = (BigInteger[,]) m1.Clone();
  
      while (cnt > 0)
      {
          if (cnt % 2 == 1) m2 = Matrix2x2Multiply(m2, m1);
  
          m1 = Matrix2x2Multiply(m1, m1);
          cnt /= 2;
      }
  
  
      return (n < 0 && n % 2 == 0) ? m2[1, 1] * -1 : m2[1, 1];
  }
  
  private static BigInteger[,] Matrix2x2Multiply(BigInteger[,] mat2x2_1, BigInteger[,] mat2x2_2)
  {
      BigInteger[,] result = new BigInteger[,]
      {
          {
              (mat2x2_1[0,0] * mat2x2_2[0,0]) + (mat2x2_1[0,1] * mat2x2_2[1,0]),
              (mat2x2_1[0,0] * mat2x2_2[0,1]) + (mat2x2_1[0,1] * mat2x2_2[1,1])
          },
          {
              (mat2x2_1[1,0] * mat2x2_2[0,0]) + (mat2x2_1[1,1] * mat2x2_2[1,0]),
              (mat2x2_1[1,0] * mat2x2_2[0,1]) + (mat2x2_1[1,1] * mat2x2_2[1,1])
          }
      };
  
      return result;
  }
}