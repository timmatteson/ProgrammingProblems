/*
Kata based on Fluent Calculator (ruby)
Created into a new kata because of a certain limitation the Ruby kata posseses that this kata should also have if translated, which is what lead me to create a new one.
#Fluent Calculator Your task is to implement a simple calculator with fluent syntax
https://www.codewars.com/kata/5578a806350dae5b05000021
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace CodeWarsCSharp
{
    public class FluentCalculator
    {
        enum Operator
        {
            Multiply,
            Divide,
            Add,
            Subtract,
            Unknown
        }

        struct Operation
        {
            public double Value { get; set; }
            public Operator Operator { get; set; }

            public Operation(double value, Operator action)
            {
                this.Value = value;
                this.Operator = action;
            }
        }

        private Stack<Operation> operations = new Stack<Operation>();

        public FluentCalculator()
        { }

        public FluentCalculator(double init)
        {
            operations.Push(new Operation(init, Operator.Unknown));
        }

        public FluentCalculator Zero
        {
            get
            {
                operations.Push(new Operation(0, Operator.Unknown));
                return this;
            }
        }

        public FluentCalculator One
        {
            get
            {
                operations.Push(new Operation(1, Operator.Unknown));
                return this;
            }
        }
        public FluentCalculator Two
        {
            get
            {
                operations.Push(new Operation(2, Operator.Unknown));
                return this;
            }
        }
        public FluentCalculator Three
        {
            get
            {
                operations.Push(new Operation(3, Operator.Unknown));
                return this;
            }
        }
        public FluentCalculator Four
        {
            get
            {
                operations.Push(new Operation(4, Operator.Unknown));
                return this;
            }
        }
        public FluentCalculator Five
        {
            get
            {
                operations.Push(new Operation(5, Operator.Unknown));
                return this;
            }
        }
        public FluentCalculator Six
        {
            get
            {
                operations.Push(new Operation(6, Operator.Unknown));
                return this;
            }
        }
        public FluentCalculator Seven
        {
            get
            {
                operations.Push(new Operation(7, Operator.Unknown));
                return this;
            }
        }
        public FluentCalculator Eight
        {
            get
            {
                operations.Push(new Operation(8, Operator.Unknown));
                return this;
            }
        }

        public FluentCalculator Nine
        {
            get
            {
                operations.Push(new Operation(9, Operator.Unknown));
                return this;
            }
        }

        public FluentCalculator Ten
        {
            get
            {
                operations.Push(new Operation(10, Operator.Unknown));
                return this;
            }
        }

        public FluentCalculator Plus
        {
            get
            {
                Operation operation = operations.Pop();
                operation.Operator = Operator.Add;
                operations.Push(operation);
                return this;
            }
        }

        public FluentCalculator Minus
        {
            get
            {
                Operation operation = operations.Pop();
                operation.Operator = Operator.Subtract;
                operations.Push(operation);
                return this;
            }
        }

        public FluentCalculator Times
        {
            get
            {
                Operation operation = operations.Pop();
                operation.Operator = Operator.Multiply;
                operations.Push(operation);
                return this;
            }
        }

        public FluentCalculator DividedBy
        {
            get
            {
                Operation operation = operations.Pop();
                operation.Operator = Operator.Divide;
                operations.Push(operation);
                return this;
            }
        }

        public double Value
        {
            get
            {
                PrintCurrentEquation();
                DoOperations(Operator.Divide);
                DoOperations(Operator.Multiply);
                DoOperations(Operator.Subtract);
                DoOperations(Operator.Add);
                return operations.Peek().Value;
            }
        }

        public double Result()
        {
            return this.Value;
        }

        private void DoOperations(Operator todo)
        {
            List<Operation> results = new List<Operation>();
            Operation current = operations.Pop();
            
            while (operations.Count > 0)
            {
                Operation next = operations.Pop();

                if (next.Operator == todo)
                {
                    current.Value = DoOperation(next.Value, current.Value, next.Operator);
                }
                else
                {
                    results.Add(current);
                    current = next;
                }
            }
            results.Add(current);

            operations.Clear();
            results.Reverse();

            foreach (Operation op in results)
                operations.Push(op);

            PrintCurrentEquation();
        }

        private void PrintCurrentEquation()
        {
            string result = "";

            foreach (Operation op in operations)
            {
                result = op.Value.ToString() + " " + GetTextOfOperator(op.Operator) + " " + result;
            }
            Console.WriteLine(result);
        }

        private string GetTextOfOperator(Operator toGet)
        {
            switch (toGet)
            {
                case Operator.Add:
                    return "+";
                case Operator.Subtract:
                    return "-";
                case Operator.Divide:
                    return "/";
                case Operator.Multiply:
                    return "*";
                case Operator.Unknown:
                    return "";
            }
            return "";
        }

        private double DoOperation(double a, double b, Operator todo)
        {
            switch (todo)
            {
                case Operator.Add:
                    return a + b;
                case Operator.Subtract:
                    return a - b;
                case Operator.Divide:
                    return a / b;
                case Operator.Multiply:
                    return a * b;
                case Operator.Unknown:
                    return a;
            }
            return a;
        }

        public static implicit operator FluentCalculator(double d) => new FluentCalculator(d);
        public static implicit operator double(FluentCalculator v) => v.Value;

    }
}
