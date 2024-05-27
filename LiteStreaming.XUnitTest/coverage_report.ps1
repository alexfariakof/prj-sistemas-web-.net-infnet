$projectTestPath = Get-Location
$projectPath =  (Resolve-Path -Path ..).Path
$projectAngular = (Resolve-Path -Path "$projectPath\AngularApp");
$sourceDirs = "$projectPath\LiteStreaming.Application;$projectPath\LiteStreaming.Domain;$projectPath\LiteStreaming.Repository;$projectPath\LiteStreaming.WebApi;$projectPath\LiteStreaming.AdministrativeApp;"
$reportPath = Join-Path -Path $projectTestPath -ChildPath "TestResults"
$coverageXmlPath = Join-Path -Path (Join-Path -Path $projectTestPath -ChildPath "TestResults") -ChildPath "coveragereport"
$filefilters = "$projectPath\LiteStreaming.DataSeeders\**;-$projectPath\Migrations.MsSqlServer\**;-$projectPath\Migrations.MySqlServer\**;-$projectPath\AngularApp\**;-$projectPath\LiteStreaming.AdministrativeApp\Views\**;"

# Excuta Teste Unitarios sem restore e build e gera o relatório de cobertura do Backend
dotnet test ./LiteStreaming.XunitTest.csproj --results-directory $reportPath /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover" --no-restore --no-build > $null 2>&1
reportgenerator -reports:$projectTestPath\coverage.cobertura.xml -targetdir:$coverageXmlPath -reporttypes:"Html;lcov;" -sourcedirs:$sourceDirs -filefilters:-$filefilters > $null 2>&1

# Verifica se existe a pasta node_module, e sem não existir executa npm install 
if (-not (Test-Path $projectAngular\node_modules)) {
	cd $projectAngular
	npm install
	cd $projectTestPath 
}

# Executa Teste Unitários e gera o relatório de cobertura do Frontend 
Start-Process npm -ArgumentList "run", "test:coverage" -WorkingDirectory $projectAngular -NoNewWindow