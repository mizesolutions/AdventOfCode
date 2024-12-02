

$list1 = @()
$list2 = @()
$result1 = 0

Get-Content "2024\pwsh\day01\day01input.txt" | ForEach-Object {
    $splitLine = $_ -split "   " # Replace "," with your actual delimiter if different
    $list1 += $splitLine[0] -as [int]
    $list2 += $splitLine[1] -as [int]
}

$list1Sort = $list1 | Sort-Object
$list2Sort = $list2 | Sort-Object

for ($i = 0; $i -le $list1Sort.Count; $i++ ) {
    $result1 += ($list1Sort[$i] - $list2Sort[$i] -lt 0? ($list1Sort[$i] - $list2Sort[$i])*-1 : $list1Sort[$i] - $list2Sort[$i])
}

Write-Host "Result1: " $result1