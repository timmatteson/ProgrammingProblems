#https://www.codewars.com/kata/59d727d40e8c9dd2dd00009f

function balance($book) {
    $books = ($book -replace "[^a-zA-Z0-9. \n]*","").Split("`n")
    $balance = 0
    $total = 0
    $expenses = 0
    $ret = ""

    foreach ($item in $books)
    {
        if ($item -ne "")
        {
            $line = $item.Split(" ")
            if ($line.Count -eq 1) 
            {
                Write-Host $balance.ToString("#.##")
                $balance = $line[0]
                $ret = "Original Balance: {0:f2}`n" -f $balance
            }
            else
            {
                $balance -= $line[2]
                $total += $line[2]
                $expenses += 1
                $ret += "{0} {1} {2:f2} Balance {3:f2}`n" -f $line[0],$line[1],$line[2],$balance
            }
        }
    }
    $ret += "Total expense  {0:f2}`n" -f $total 
    $ret += "Average expense  {0:f2}" -f $($total / $expenses)
    return $ret
}