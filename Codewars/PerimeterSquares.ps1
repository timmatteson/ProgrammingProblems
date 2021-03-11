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