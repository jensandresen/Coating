param (
    $tasks = "Compile"
)

$sourceDir = resolve-path ".\..\src"

# restore nuget packages
write-host "Restore nuget packages" -ForegroundColor Cyan
gci -path "$sourceDir" -include "*.sln" -recurse | % { 

  $solutionFilePath = $_.FullName
  write-host "Restoring nuget packages for $solutionFilePath"
  & nuget Restore "$solutionFilePath" -NonInteractive
  
}

# invoke psake
$psakeScriptFile = gci -path "$sourceDir\packages" -include "psake.ps1" -recurse | select -first 1
&($psakeScriptFile) -buildFile .\pipeline.ps1 -taskList $tasks -framework "4.0x64" -nologo