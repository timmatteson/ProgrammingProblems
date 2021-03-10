
"""class Node:
    def __init__(self, freq,data):
        self.freq= freq
        self.data=data
        self.left = None
        self.right = None
"""        

def decodeHuff(root, s):
    ret = ""
    cur = root
    path = None

    for c in s:
        if c == "0":
            path = cur.left
        else: 
            path = cur.right
        
        if path.left == None or path.right == None:
            ret += path.data
            cur = root
        else:
            cur = path
    
    print(ret)

