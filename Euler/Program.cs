using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        public static List<Int64> Primes = new List<Int64>();

        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            ulong test = 0;
            Console.WriteLine(ulong.MaxValue);

            Console.WriteLine(SumOfDigits(FindFactorial(100).ToString()));

            watch.Stop();
            Console.WriteLine("Elapsed: " + watch.Elapsed.Milliseconds);
            Console.ReadLine();
        }

        public static ulong FindFactorial(ulong input)
        {
            
            ulong result = input;

            for (ulong i = 1; i < input; i++)
            {
                result *= i;
            }
            return result;
        }

        public static long FindLargestCollatzSequence(int start, int end)
        {
            int max = 0;
            long maxTerm = 0;

            for (long i = start; i <= end; i++)
            {
                int result = LengthOfCollatzSequence(i);

                if (result > max)
                {
                    max = result;
                    maxTerm = i;
                }
            }
            return maxTerm;
        }

        public static int LengthOfCollatzSequence(long start)
        {
            int length = 1;
            long seq = start;

            while (seq != 1)
            {
                if (seq % 2 == 0)
                {
                    seq = seq / 2;
                }
                else
                {
                    seq = (3 * seq) + 1;
                }

                length++;

                if (seq < 0)
                    Console.WriteLine(start);
            }

            return length;

        }

        public static void BigNumberProblem()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            byte[] result = new byte[] { 0 };
            string sum = string.Empty;

            using (System.IO.StreamReader reader = new System.IO.StreamReader(@"D:\Projects\Personal\Euler\ConsoleApplication1\bin\Debug\BigNumbers.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    result = AddBigNumbers(result, BigNumberToByteArray(line));
                }

                reader.Close();
            };

            for (int i = 0; i < result.Length; i++)
            {
                sum += result[i].ToString();
            }

            watch.Stop();

            Console.WriteLine(watch.Elapsed.Milliseconds);

            Console.WriteLine(sum);

        }

        public static byte[] AddBigNumbers(byte[] first, byte[] second)
        {
            int maxLength = (first.Length > second.Length ? first.Length : second.Length);
            int firstOffset = (second.Length > first.Length ? second.Length - first.Length : 0);
            int secondOffset = (first.Length > second.Length ? first.Length - second.Length : 0);
            byte[] ret = new byte[maxLength + 1];

            int cnt = maxLength - 1;
            int carry = 0;

            do
            {
                int firstNum = (cnt - firstOffset >= 0) ? (int) first[cnt - firstOffset] : 0;
                int secondNum = (cnt - secondOffset >= 0) ? (int) second[cnt - secondOffset] : 0;

                int result = firstNum + secondNum;

                ret[cnt + 1] = (byte) ((result + carry) % 10);
                carry = (result + carry) / 10;

                cnt--;
            } while (cnt >= 0);

            ret[0] = (byte) carry;

            return ret;
        }

        public static byte[] MultiplyBigNumbers(byte[] first, byte[] second)
        {
            int maxLength = (first.Length > second.Length ? first.Length : second.Length);
            int firstOffset = (second.Length > first.Length ? second.Length - first.Length : 0);
            int secondOffset = (first.Length > second.Length ? first.Length - second.Length : 0);
            byte[] ret = new byte[maxLength + 1];

            int cnt = maxLength - 1;
            int carry = 0;

            do
            {
                int firstNum = (cnt - firstOffset >= 0) ? (int)first[cnt - firstOffset] : 0;
                int secondNum = (cnt - secondOffset >= 0) ? (int)second[cnt - secondOffset] : 0;

                int result = firstNum * secondNum;

                ret[cnt + 1] = (byte)((result + carry) % 10);
                carry = (result + carry) / 10;

                cnt--;
            } while (cnt >= 0);

            ret[0] = (byte)carry;

            return ret;
        }

        public static byte[] BigNumberToByteArray(string bigNumber)
        {
            int length = bigNumber.Length;
            byte[] ret = new byte[length];
            char[] values = bigNumber.ToCharArray();

            for (int i = 0; i < length; i++)
            {
                ret[i] = byte.Parse(values[i].ToString());
            }

            return ret;
        }

        public static long ByteArrayToLong(byte[] input)
        {
            long ret = 0;
            int size = input.Length - 1;

            for (int i = 0; i < input.Length; i++)
            {
                ret += (long)((int)input[i] * Math.Pow(10, size - i));
            }
            return ret;
        }


        public static int FindBiggestPath(int[,] matrix, int distance)
        {
            int max = 0;

            for (int i = 0; i<=matrix.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= matrix.GetUpperBound(1); j++)
                {
                    int down = ProductDown(matrix, i, j, distance);
                    int right = ProductRight(matrix, i, j, distance);
                    int diagDown = ProductDiagDown(matrix, i, j, distance);
                    int diagUp = ProductDiagUp(matrix, i, j, distance);

                    if (down > max) 
                        max = down;
                    if (right > max) 
                        max = right;
                    if (diagDown > max) 
                        max = diagDown;
                    if (diagUp > max)
                        max = diagUp;
                }
            }
            return max;
        }

        public static int ProductDown(int[,] matrix, int x, int y, int distance)
        {
            int ret = 1;

            for (int i = x; i < x + distance; i++)
            {
                if (IsInBounds(matrix, i, y))
                {
                    ret *= matrix[i, y];
                }
                else
                {
                    ret = 0;
                    return ret;
                }
            }
            return ret;
        }

        public static int ProductRight(int[,] matrix, int x, int y, int distance)
        {
            int ret = 1;

            for (int i = y; i < y + distance; i++)
            {
                if (IsInBounds(matrix, x, i))
                {
                    ret *= matrix[x, i];
                }
                else
                {
                    ret = 0;
                    return ret;
                }
            }
            return ret;
        }

        public static int ProductDiagDown(int[,] matrix, int x, int y, int distance)
        {
            int ret = 1;
            int j = y;

            for (int i = x; i < x + distance; i++)
            {
                if (IsInBounds(matrix, i, j))
                {
                    ret *= matrix[i, j];
                }
                else
                {
                    ret = 0;
                    return ret;
                }
                j++;
            }
            return ret;
        }

        public static int ProductDiagUp(int[,] matrix, int x, int y, int distance)
        {
            int ret = 1;
            int j = y;

            for (int i = x; i < x + distance; i++)
            {
                if (IsInBounds(matrix, i, j))
                {
                    ret *= matrix[i, j];
                }
                else
                {
                    ret = 0;
                    return ret;
                }
                j--;
            }
            return ret;
        }

        public static bool IsInBounds(int[,] matrix, int x, int y)
        {
            if (x >= matrix.GetLowerBound(0) && x <= matrix.GetUpperBound(0) &&
                y >= matrix.GetLowerBound(1) && y <= matrix.GetUpperBound(1))
                return true;

            return false;            
        }

        private static int[] ParseStringArray(string[] parse)
        {
            int[] ret = new int[parse.Length];

            for (int i=0;i<=ret.GetUpperBound(0);i++)
            {
                ret[i] = int.Parse(parse[i]);
            }
            return ret;
        }

        public static int SumOfPower(long num, long power)
        {
            double result = (double) Math.Pow(num, power);
            string bigNumber = DoubleConverter.ToExactString(result);
            return SumOfDigits(bigNumber);
        }

        public static int SumOfDigits(string input)
        {
            char[] buff = input.ToCharArray();
            int sum = 0;

            for (int i = 0; i <= buff.GetUpperBound(0); i++)
            {
                sum += int.Parse(buff[i].ToString());
            }
            return sum;
        }

        public static long FindPrimeSum(int bound)
        {
            long sum = 2;

            for (int i = 3; i < bound; i += 2)
            {
                if (IsPrime(i))
                {
                    sum += i;
                }
            }
            return sum;
        }

        public static int FindTriplet(int sum)
        {
            for (int i = 1; i < sum; i++)
            {
                for (int j = i + 1; j < sum; j++)
                {
                    for (int k = j + 1; k < sum; k++)
                    {
                        if (i + j + k == sum)
                        {
                            if (i * i + j * j == k * k)
                            {
                                return i * j * k;
                            }
                        }
                    }
                }
            }
            return 0;
        }

        public static int FindLargestProduct(string series, int bound)
        {
            List<int> results = new List<int>();

            for (int i = 0; i < series.Length - bound - 1; i++)
            {
                List<char> list = new List<char>();

                list.AddRange(series.ToCharArray(i, bound));
                list.Sort();
                list.Reverse();

                results.Add(FindProduct(list.ToArray()));
            }

            results.Sort();
            return results.Last();

        }

        public static int FindProduct(char[] ara)
        {
            int product = 1;

            for (int i = 0; i <= ara.GetUpperBound(0); i++)
            {
                product *= int.Parse(ara[i].ToString());
            }
            return product;
        }

        public static int FindPrimeAt(int index)
        {
            int currentIndex = 1;
            int currentPrime = 3;
            int test = 3;

            do
            {
                if (IsPrime(test))
                {
                    currentIndex ++;
                    currentPrime = test;

                    if (currentIndex == index)
                    {
                        return currentPrime;
                    }
                }
                test += 2;
            } while (currentIndex < index);

            return 0;
        }

        public static int FindLargestProducts(int lowerBound, int upperBound)
        {
            List<int> found = new List<int>();

            for (int i = upperBound; i >= lowerBound; i--)
            {
                for (int j = i - 1; j >= lowerBound; j--)
                {
                    if (IsPalindromicInt(j * i))
                    { 
                        found.Add(j * i); 
                    }
                }
            }
            found.Sort();

            return found.Last();
        }

        public static bool IsPalindromicInt(int test)
        {
             return Reverse(test.ToString()) == test.ToString();
        }

        public static string Reverse(string input)
        {
            StringBuilder builder = new StringBuilder(input.Length);

            builder.Append(input.Reverse().ToArray());
            return builder.ToString();
        }

        public static int FindSmallestDivisible(int start, int end)
        {
            int test = end;
            
            while (true)
            {
                bool found = false;

                for (int i=start;i<=end;i++)
                {
                    if (test % i == 0)
                    {
                        found = true;
                    }
                    else
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    return test;
                }
                test += end;
            }
        }

        public static Int64 FindLargestPrimeFactor(Int64 number)
        {
            Int64 bound = number / 2;
            Int64 lastPrime = 0;

            for (Int64 i = 1; i < bound; i += 2)
            {
                if (number % i == 0)
                {
                    Int64 candidate = number / i;
                    if (IsPrime(candidate))
                    {
                        lastPrime = candidate;
                        break;
                    }
                }
            }
            return lastPrime;
        }

        public static int GetSumOfSquares(int start, int end)
        {
            int sum = 0;

            for (int i = start; i <= end; i++)
            {
                sum += (i * i);
            }
            return sum;
        }

        public static int GetSquareOfSum(int start, int end)
        {
            int sum = 0;

            for (int i = start; i <= end; i++)
            {
                sum += i;
            }
            return sum * sum;
        }


        public static int FindGoldbackFail()
        {
            int start = 1;

            do
            {
                start += 2;

                //is prime?
                bool prime = IsPrime(start);

                if (!prime)
                {
                    int fact = 1;
                    bool found = false;
                    while ((fact * fact * 2) < start - 3)
                    {
                        if (IsPrime(start - (fact * fact * 2)))
                        {
                            found = true;
                            break;
                        }
                        fact++;
                    }
                    if (!found)
                    {
                        Console.WriteLine(start);
                        break;
                    }
                }
            } while (true);
            return start;
        }

        public static bool IsPrime(Int64 test)
        {
            if (test == 2) return true;
            if (test % 2 == 0) return false;

            foreach (int prime in Primes)
            {
                if (test % prime == 0) return false;
            }

            for (Int64 i = 3; i < test / 2; i += 2)
            {
                if (test % i == 0) return false;
            }
            Primes.Add(test);
            return true;
        }

        public static int GetFibonacciSum(int first, int second, int threshold, int tally)
        {
            tally += (second % 2 == 0) ? second : 0;

            int next = first + second;

            if (next < threshold)
            {
                return GetFibonacciSum(second, next, threshold, tally);
            }
            else
            {
                return tally;
            }
        }

        public static int GetSum(int threshold, int[] values)
        {
            HashSet<int> sums = new HashSet<int>();

            for (int i = 0; i <= values.GetUpperBound(0); i++)
            {
                int sum = values[i];

                do
                {
                    sums.Add(sum);
                    sum += values[i];
                } while (sum < threshold);
            }
            
            return sums.Sum();
        }

    }
}
