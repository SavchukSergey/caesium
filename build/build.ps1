$baseDir = resolve-path ..
$srcPath = $baseDir;
$buildDir = "$baseDir\build"
$packageDir = "$buildDir\package"

$buildNuGet = $true;
$nugetPackageId = "caesium";
$nugetVersion = "1.0";
$nugetPrelease = "beta1"
$nugetSourcePath = "$buildDir\caesium.nuspec"
$nuget = "$buildDir\nuget\nuget.exe"
$nugetOutput = "$buildDir\output"

$msbuild = "C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe"
$configuration = "Release"

$nugetSpec = "$packageDir\caesium.nuspec"

function Ensure-Folder($path) {
	if(!(Test-Path -Path $path)){
		New-Item -ItemType directory -Path $path
	}
}

function Delete-Folder($path) {
	if(Test-Path -Path $path){
		Remove-Item $path -recurse
	}
}

function Update-NuSpec ($path) {
    $xml = [xml](Get-Content $path)
	$meta = $xml.package.metadata;
	$meta.id = $nugetPackageId;
	$meta.version = $nugetVersion;
    $xml.save($path)
}

function Pack-NuSpec ($path) {
	Ensure-Folder $nugetOutput
	& $nuget pack $path -Symbols -OutputDirectory $nugetOutput
}

function Build-Project($path) {
	& $msbuild $path /p:Configuration=$configuration
}

function Copy-Build($projectDir)  {
	$binSourcePath = "$projectDir\bin\$configuration";
	$binTargetPath = "$packageDir\lib\net45"
	Ensure-Folder $binTargetPath
    Copy-Item -Path $binSourcePath\*.dll -Destination $binTargetPath -recurse
    Copy-Item -Path $binSourcePath\*.pdb -Destination $binTargetPath -recurse
    Copy-Item -Path $binSourcePath\*.xml -Destination $binTargetPath -recurse
}

Delete-Folder $packageDir
Delete-Folder $nugetOutput

Write-Host "Building Caesium project"
Build-Project "$srcPath\Caesium\Caesium.csproj"
Write-Host "Building Caesium Tests project"
Build-Project "$srcPath\Caesium.Tests\Caesium.Tests.csproj"

Write-Host "Copying bin files to package"
Ensure-Folder $packageDir
Ensure-Folder "$packageDir\lib"
Copy-Build "$srcPath\Caesium"

Write-Host "Updating nuspec file at $nugetSpec" -ForegroundColor Green
Copy-Item -Path $nugetSourcePath -Destination $nugetSpec
Update-NuSpec $nugetSpec

Write-Host "Building NuGet package with ID $packageId and version $nugetVersion" -ForegroundColor Green
Pack-NuSpec($nugetSpec);
