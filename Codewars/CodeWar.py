"""
If your phone buttons have letters, then it is easy remember long phone numbers by making words from the substituted digits.

https://en.wikipedia.org/wiki/Phoneword

source: imgur.com
This is common for 1-800 numbers

1-800 number format
This format probably varies for different countries, but for the purpose of this Kata here are my rules:

A 1-800 number is an 11 digit phone number which starts with a 1-800 prefix.

The remaining 7 digits can be written as a combination of 3 or 4 letter words. e.g.

1-800-CODE-WAR
1-800-NEW-KATA
1-800-GOOD-JOB
The - are included just to improve the readibility.

Story
A local company has decided they want to reserve a 1-800 number to help with advertising.

They have already chosen what words they want to use, but they are worried that maybe that same phone number could spell out other words as well. Maybe bad words. Maybe embarrassing words.

They need somebody to check for them so they can avoid any accidental PR scandals!

That's where you come in...

Kata Task
There is a preloaded array of lots of 3 and 4 letter (upper-case) "words".

For Python it is: WORDS
Given the 1-800 (phone word) number that the company wants to use, you need to check against all known words and return the set of all possible 1-800 numbers which can be formed using those same digits.

Notes
The desired phone-word number provided by the company is formatted properly. No need to check.
All the letters are upper-case. No need to check.
Only words in the list are allowed.
https://www.codewars.com/kata/5a3267b2ee1aaead3d000037
"""

def get_digits(text):
    digits = {"A": 2, "B": 2, "C": 2, 
              "D": 3, "E": 3, "F": 3, 
              "G": 4, "H": 4, "I": 4, 
              "J": 5, "K": 5, "L": 5, 
              "M": 6, "N": 6, "O": 6, 
              "P": 7, "Q": 7, "R": 7, "S": 7, 
              "T": 8, "U": 8, "V": 8, 
              "W": 9, "X": 9, "Y": 9, "Z": 9}
    return map(lambda x: digits[x], text)

def get_numbers(l, s1, s2):

    if len(s1) > 0 and len(s2) > 0:
        for t1 in s1: 
          for t2 in s2:
              l.add("1-800-" + t1 + "-" + t2)


def check1800(s):
    results = set()
    num = list(get_digits(s.split("-")[2] + s.split("-")[3]))

    set1 = list(filter(lambda x: num[0:3] == list(get_digits(x)), WORDS))
    set2 = list(filter(lambda x: num[3:7] == list(get_digits(x)), WORDS))
    set3 = list(filter(lambda x: num[0:4] == list(get_digits(x)), WORDS))
    set4 = list(filter(lambda x: num[4:7] == list(get_digits(x)), WORDS))
    
    get_numbers(results, set1, set2)
    get_numbers(results, set3, set4)
        
    return results