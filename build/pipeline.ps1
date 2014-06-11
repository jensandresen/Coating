properties {
    $rootDir = Resolve-Path ..
    $buildDir = Resolve-Path .
    $outputDir = "$buildDir\Output"
    $sourceDir = "$rootDir\src"
    $solutionFilePath = "$sourceDir\Coating.sln"
}

Task Clean -description "Cleans both solution and build output directory" {

    if (Test-Path $outputDir) {
        rmdir $outputDir -recurse -force
        Write-Host "Output directory is cleaned"
    }

    mkdir $outputDir | out-null

    exec {
        msbuild "$solutionFilePath" /t:Clean /p:Configuration="Debug" /verbosity:q /nologo 
        msbuild "$solutionFilePath" /t:Clean /p:Configuration="Release" /verbosity:q /nologo 

        Write-Host "Solution build output directories is cleaned"
    }
}


Task Compile -depends Clean -description "Builds the solution in release mode to any platform" {

    exec {
        msbuild "$solutionFilePath" /p:OutputPath="$outputDir" /p:Configuration="Release" /p:Platform="Any CPU" /nologo /verbosity:q
    }    
}

Task Pack  {

    exec {
        & nuget pack "$sourceDir\Coating.Core\Coating.Core.csproj" -Build -OutputDirectory "$outputDir" -Properties Configuration=Release
    }

}
