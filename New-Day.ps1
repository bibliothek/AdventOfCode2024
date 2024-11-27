[CmdletBinding()]
param (
    [Parameter(Mandatory = $true)]
    [int]
    $Day
)

$solverContent = Get-Content $PSScriptRoot/src/AdventOfCode2023/Solver/DayXY.fs
$solverContent.Replace("XY", $Day) | Out-File $PSScriptRoot/src/AdventOfCode2023/Solver/Day$Day.fs -Encoding utf8

$testContent = Get-Content $PSScriptRoot/test/AdventOfCode2023.Tests/DayXYTests.fs
$testContent.Replace("XY", $Day).Replace('(Skip="Template")', "") | Out-File $PSScriptRoot/test/AdventOfCode2023.Tests/Day$($Day)Tests.fs -Encoding utf8

New-Item $PSScriptRoot/input/Day$Day.txt

$solverProject = [System.Xml.XmlDocument](Get-Content $PSScriptRoot/src/AdventOfCode2023/AdventOfCode2023.fsproj)
$solverElement = $solverProject.CreateElement("Compile")
$solverElement.SetAttribute("Include", "Solver\Day$Day.fs")
$programNode = $solverProject.SelectSingleNode('//Compile[@Include="Program.fs"]')
$solverProject.GetElementsByTagName("ItemGroup")[0].InsertBefore($solverElement, $programNode)
$solverProject.Save("$PSScriptRoot/src/AdventOfCode2023/AdventOfCode2023.fsproj")

$testProject = [System.Xml.XmlDocument](Get-Content $PSScriptRoot/test/AdventOfCode2023.Tests/AdventOfCode2023.Tests.fsproj)
$testElement = $testProject.CreateElement("Compile")
$testElement.SetAttribute("Include", "Day$($Day)Tests.fs")
$testProject.GetElementsByTagName("ItemGroup")[0].PrependChild($testElement)
$testProject.Save("$PSScriptRoot/test/AdventOfCode2023.Tests/AdventOfCode2023.Tests.fsproj")
