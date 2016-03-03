param([Parameter(Mandatory=$true)][string]$version) 

# Update All AssemblyInfo file versions
$z29 = "./ExternalTools/ZeroToNine/Zero29.exe"
&$z29 -a $version

# Update Appveyor.yml

((Get-Content ./Appveyor.yml | Out-String) -replace "version: .*\.0", ("version: " + $version + ".0")).Trim("`r`n") | Set-Content -NoNewline Appveyor.yml
((Get-Content ./Appveyor.yml | Out-String) -replace "version: .*\.{build}", ("version: " + $version + ".{build}")).Trim("`r`n") |  Set-Content -NoNewline Appveyor.yml