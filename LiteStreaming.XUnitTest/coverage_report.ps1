$baseDirectory = ($PWD)
$projectTestPath = Join-Path -Path ($baseDirectory) -ChildPath "LiteStreaming.XUnitTest"            

if (-Not (Test-Path -Path $projectTestPath)) {
    $baseDirectory = (Resolve-Path -Path ..).Path    
    $projectTestPath = Join-Path -Path ($baseDirectory) -ChildPath "LiteStreaming.XUnitTest"
}

$projectAngular = (Resolve-Path -Path "$baseDirectory\AngularApp");
$sourceDirs = "$baseDirectory\LiteStreaming.Application;$baseDirectory\LiteStreaming.Domain;$baseDirectory\LiteStreaming.Repository;$baseDirectory\LiteStreaming.WebApi;$baseDirectory\LiteStreaming.AdministrativeApp;"
$reportPath = Join-Path -Path $projectTestPath -ChildPath "TestResults"
$coverageXmlPath = Join-Path -Path (Join-Path -Path $projectTestPath -ChildPath "TestResults") -ChildPath "coveragereport"
$filefilters = "$baseDirectory\LiteStreaming.DataSeeders\**;-$baseDirectory\Migrations.MsSqlServer\**;-$baseDirectory\Migrations.MySqlServer\**;-$baseDirectory\AngularApp\**;-$baseDirectory\LiteStreaming.AdministrativeApp\Views\**;"

# Excuta Teste Unitarios sem restore e build e gera o relatório de cobertura do Backend
dotnet clean > $null 2>&1
dotnet test $projectTestPath/LiteStreaming.XunitTest.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover"
reportgenerator -reports:$projectTestPath/coverage.cobertura.xml -targetdir:$coverageXmlPath -reporttypes:"Html;lcov;" -sourcedirs:$sourceDirs -filefilters:-$filefilters 

# Verifica se existe a pasta node_module, e sem não existir executa npm install 
if (-not (Test-Path $projectAngular\node_modules)) {
    $watchProcess = Start-Process npm -ArgumentList "install" -WorkingDirectory $projectAngular -NoNewWindow -PassThru
    $watchProcess.WaitForExit()	
}

# Executa Teste Unitários e gera o relatório de cobertura do Frontend 
$watchProcess = Start-Process npm -ArgumentList "run", "test:coverage" -WorkingDirectory $projectAngular -NoNewWindow -PassThru
$watchProcess.WaitForExit()