/*Your task is to parse and Calculate Mathematical Expressions.
Don't use any 'parse', 'compile' or 'eval' function of the Compiler (by the way C# has no 'eval' function like Java/Javascript/..., 
but surely there are ways to use similar methods, or parts of it) - write your own parser/evaluation function.
https://www.codewars.com/kata/564d9ebde30917684f000048
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Evaluation
{
    public enum Precedence
    {
        None = 0,
        Number = 1,
        LeftParenthesis = 2,
        RightParenthesis = 3,
        Function = 4,
        Power = 5,
        Negation = 6,
        MultiplicationDivision = 7,
        AdditionSubtraction = 8
    }

    public class Evaluate
    {
        private struct EquationComponent
        {
            public Precedence ComponentType { get; }
            public string Component { get; }
            
            public EquationComponent(Precedence componentType, string component)
            {
                ComponentType = componentType;
                Component = component;
            }

            public static Precedence GetComponentType(string component, Precedence previous)
            {
                switch (component)
                {
                    case "&":
                        return Precedence.Power;
                    case "*":
                        return Precedence.MultiplicationDivision;
                    case "/":
                        return Precedence.MultiplicationDivision;
                    case "+":
                        return Precedence.AdditionSubtraction;
                    case "-":
                        if (previous == Precedence.Number || previous == Precedence.RightParenthesis)
                            return Precedence.AdditionSubtraction;
                        else
                            return Precedence.Negation;
                    case "(":
                        return Precedence.LeftParenthesis;
                    case ")":
                        return Precedence.RightParenthesis;
                    default:
                        return Precedence.None;
                }
            }

        }

        private List<EquationComponent> ParseExpression(string expression)
        {
            List<EquationComponent> result = new List<EquationComponent>();
            string currentValue = string.Empty;
            string currentFunction = string.Empty;
            char last = ' ';

            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];

                if (char.IsNumber(c) || c == '.' || c == 'e' || (last == 'e' && (c == '-' || c == '+')))
                    currentValue += c;
                else if (char.IsLetter(c)) currentFunction += c;
                else
                {
                    if (currentValue != string.Empty) result.Add(new EquationComponent(Precedence.Number, currentValue));
                    if (currentFunction != string.Empty) result.Add(new EquationComponent(Precedence.Function, currentFunction.ToLower()));
                    currentFunction = string.Empty;
                    currentValue = string.Empty;

                    if (c != ' ')
                    {
                        Precedence previous = Precedence.None;

                        if (result.Count > 0) previous = result.Last().ComponentType;

                        Precedence currentPrecedence = EquationComponent.GetComponentType(c.ToString(), previous);

                        if (currentPrecedence == Precedence.Negation && previous == Precedence.Negation)
                        {
                            result.Remove(result.Last());
                            if (result.Last().Component != "+")
                                result.Add(new EquationComponent(Precedence.AdditionSubtraction, "+"));
                        }
                        else
                            result.Add(new EquationComponent(currentPrecedence, c.ToString()));   
                    }
                }
                last = c;
            }

            if (currentValue != string.Empty) result.Add(new EquationComponent(Precedence.Number, currentValue));
            if (currentFunction != string.Empty) result.Add(new EquationComponent(Precedence.Function, currentFunction.ToLower()));

            return result;
        }

        private List<EquationComponent> ConvertToPostfix(List<EquationComponent> input)
        {
            Stack<EquationComponent> operators = new Stack<EquationComponent>();
            List<EquationComponent> result = new List<EquationComponent>();

            foreach (var component in input)
            {
                Precedence current = component.ComponentType;

                if (current == Precedence.Number)
                    result.Add(component);
                else
                {
                    if (current > Precedence.RightParenthesis)
                    {

                        while (operators.Count() > 0 && 
                            ((operators.Peek().ComponentType <= current) && 
                                !(current == Precedence.Power && current == operators.Peek().ComponentType)) &&
                            operators.Peek().ComponentType != Precedence.LeftParenthesis && 
                            operators.Peek().ComponentType != Precedence.RightParenthesis)
                        {
                            result.Add(operators.Pop());
                        }
                        operators.Push(component);
                    }
                    if (current == Precedence.LeftParenthesis)
                        operators.Push(component);
                    if (current == Precedence.RightParenthesis)
                    {
                        while (operators.Count() > 0 &&
                            operators.Peek().ComponentType != Precedence.LeftParenthesis)
                        {
                            result.Add(operators.Pop());
                        }
                        if (operators.Count() == 0) throw new Exception("Unmatched Parenthesis");

                        if (operators.Count() > 0 && operators.Peek().ComponentType == Precedence.LeftParenthesis)
                            operators.Pop();
                    }
                }
            }
            while (operators.Count() > 0)
            {
                result.Add(operators.Pop());
            }

            return result;
        }

        private double PerformOperation(Stack<double> operands, EquationComponent item)
        {
            double op2 = 0;
            double op1 = 0;

            switch (item.Component)
            {
                case "&":
                    op2 = operands.Pop();
                    op1 = operands.Pop();
                    return Math.Pow(op1, op2);
                case "*":
                    op2 = operands.Pop();
                    op1 = operands.Pop();
                    return op1 * op2;
                case "/":
                    op2 = operands.Pop();
                    op1 = operands.Pop();
                    return op1 / op2;
                case "+":
                    op2 = operands.Pop();
                    op1 = operands.Pop();
                    return op1 + op2;
                case "-":
                    if (item.ComponentType == Precedence.Negation)
                    {
                        op1 = operands.Pop();
                        return op1 * -1;
                    }
                    else
                    {
                        op2 = operands.Pop();
                        op1 = operands.Pop();
                        return op1 - op2;
                    }
                case "log":
                    op1 = operands.Pop();
                    return Math.Log(op1);
                case "ln":
                    op1 = operands.Pop();
                    return Math.Log(op1);
                case "exp":
                    op1 = operands.Pop();
                    return Math.Exp(op1);
                case "sqrt":
                    op1 = operands.Pop();
                    return Math.Sqrt(op1);
                case "abs":
                    op1 = operands.Pop();
                    return Math.Abs(op1);
                case "atan":
                    op1 = operands.Pop();
                    return Math.Atan(op1);
                case "acos":
                    op1 = operands.Pop();
                    return Math.Acos(op1);
                case "asin":
                    op1 = operands.Pop();
                    return Math.Asin(op1);
                case "sinh":
                    op1 = operands.Pop();
                    return Math.Sinh(op1);
                case "cosh":
                    op1 = operands.Pop();
                    return Math.Cosh(op1);
                case "tanh":
                    op1 = operands.Pop();
                    return Math.Tanh(op1);
                case "tan":
                    op1 = operands.Pop();
                    return Math.Tan(op1);
                case "sin":
                    op1 = operands.Pop();
                    return Math.Sin(op1);
                case "cos":
                    op1 = operands.Pop();
                    return Math.Cos(op1);
            }
            return double.NaN;
        }
        public string eval(string expression)
        {
            Console.WriteLine(expression);
            try
            {
                List<EquationComponent> parsed = ParseExpression(expression);
                List<EquationComponent> postfix = ConvertToPostfix(parsed);
                Stack<Double> operands = new Stack<double>();

                foreach (var item in postfix)
                {
                    if (item.ComponentType == Precedence.Number)
                        operands.Push(double.Parse(item.Component));
                    else
                    {
                        if (operands.Count() > 0)
                        {
                            double result = PerformOperation(operands, item);
                            operands.Push(result);
                        }
                    }
                }

                if (operands.Count() > 0)
                {
                    double result = operands.Peek();
                    if (double.IsInfinity(result) || double.IsNaN(result))
                        return "ERROR";
                    else
                        return result.ToString();
                }
                else
                    return "ERROR";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "ERROR";
            }
        }

    }
}