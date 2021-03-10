#https://www.codewars.com/kata/559a28007caad2ac4e000083
function Get-SumOfFibonacci([int] $steps,[int] $curStep,[long] $lastNum,[long] $num,[long] $sum)
{
    if ($curStep -le $steps)
    {
        $sum += $num
        Get-SumOfFibonacci $steps ($curStep + 1) $num ($num + $lastNum) $sum
    }
    else
    {
        return $sum
    }
}

function perimeter($n) 
{
    return ((Get-SumOfFibonacci $n 0 0 1 0) * 4)
}

#https://www.codewars.com/kata/5ce399e0047a45001c853c2b
function parts-sums($ls) {
  $bound = $ls.GetUpperBound(0)
  $s = 0
  
  $ret = @(0) * ($bound + 2)
  
  for ($j = $bound;$j -gt -1 ;$j--)
  {
      $s += $ls[$j]
      $ret[$j] = $s
  }
  
  return $ret
}

#https://www.codewars.com/kata/514b92a657cdc65150000006
function Get-SumOfMultiples($Value)
{
    $ret = 0
    $cnt = 0

    while ($cnt -lt $Value)
    {
        If($cnt % 3 -eq 0)
        {
            $ret += $cnt
        }
        else
        {
            If($cnt % 5 -eq 0){$ret += $cnt}
        }
        $cnt += 1
    }
    
  return $ret
}

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

#https://www.codewars.com/kata/59df2f8f08c6cec835000012
function meeting($s){
  return ($s.ToUpper().split(";") | 
    Select-Object @{Label = "Name";Expression ={"($($_.Split(':')[1]), $($_.Split(':')[0]))"}} | 
    Sort-Object -Property Name | 
    Select-Object -ExpandProperty Name) -join ""
}
