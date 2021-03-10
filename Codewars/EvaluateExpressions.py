#Given a mathematical expression as a string you must return the result as a number.
#https://www.codewars.com/kata/52a78825cdfc2cfc87000005

class Tokens:
    Number = 1
    LeftParenthesis = 2
    RightParenthesis = 3
    Negation = 4
    Power = 5
    Multiplication = 6
    Division = 6
    Addition = 7
    Subtraction = 7

def gettokentype(t, last):
    if t == '^':
        return Tokens.Power
    if t == '*':
        return Tokens.Multiplication
    if t == '/':
        return Tokens.Division
    if t == '+':
        return Tokens.Addition
    if t == '-':
        if last == Tokens.Number or last == Tokens.RightParenthesis:
            return Tokens.Subtraction        
        else: 
            return Tokens.Negation
    if t == '(':
        return Tokens.LeftParenthesis  
    if t == ')':
        return Tokens.RightParenthesis  
    
    return Tokens.Number

def tokenizeexpression(expression):
    tokens = []
    index = 0
    buff = ""

    while True:
        char = expression[index]

        if char.replace(".", "1").isnumeric():
            buff += char
        elif char != " ":
            if buff != "":
                tokens.append((buff, Tokens.Number))
        
            last = tokens[len(tokens) - 1][1] if len(tokens) > 0 else None

            tokens.append((char, gettokentype(char, last)))
            buff = ""
        
        index += 1

        if index == len(expression):
            if buff != "":
                tokens.append((buff, Tokens.Number))
            break
    
    return tokens

def converttorpn(expression):
    tokens =  tokenizeexpression(expression)
    output = []
    operator = []
    
    for token in tokens:
        if token[1] == Tokens.Number:
            output.append(token)
        else:
            if token[1] > Tokens.RightParenthesis:
                while len(operator) > 0 and operator[len(operator) - 1][1] <= token[1] and operator[len(operator) - 1][1] != Tokens.LeftParenthesis and operator[len(operator) - 1][1] != Tokens.RightParenthesis:
                    
                    output.append(operator.pop())

                operator.append(token)

            if token[1] == Tokens.LeftParenthesis:
                operator.append(token)
            
            if token[1] == Tokens.RightParenthesis:
                while len(operator) > 0 and operator[len(operator) - 1][1] != Tokens.LeftParenthesis:
                    output.append(operator.pop())
                
                if len(operator) > 0 and operator[len(operator) - 1][1] == Tokens.LeftParenthesis:
                    operator.pop()

    while len(operator) > 0:
        output.append(operator.pop())
                
    return output

def operate(operands, operator):
    t = operator[0]

    if t == '^':
        operand2 = operands.pop()
        operand1 = operands.pop()
        return operand1 ** operand2
    if t == '*':
        operand2 = operands.pop()
        operand1 = operands.pop()
        return operand1 * operand2
    if t == '/':
        operand2 = operands.pop()
        operand1 = operands.pop()
        return operand1 / operand2
    if t == '+':
        operand2 = operands.pop()
        operand1 = operands.pop()
        return operand1 + operand2
    if t == '-':
        if operator[1] == Tokens.Negation:
            operand1 = operands.pop()
            return operand1 * -1
        else:
            operand2 = operands.pop()
            operand1 = operands.pop()
            return operand1 - operand2        
    

def calc(expression):
    print(expression)
    rpn = converttorpn(expression)
    operands = []

    for token in rpn:
        if token[1] == Tokens.Number:
            operands.append(float(token[0]))
        else:
            result = operate(operands, token)
            operands.append(result)

    return operands[len(operands) - 1] if len(operands) > 0 else None