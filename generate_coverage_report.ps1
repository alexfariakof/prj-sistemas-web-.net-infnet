cls

# Pasta onde o relat�rio ser� gerado
$baseDirectory = ($PWD)
$projectTestPath = Join-Path -Path ($baseDirectory) -ChildPath "LiteStreaming.XUnitTest"            
$sourceDirs = "$baseDirectory\LiteStreaming.Application;$baseDirectory\LiteStreaming.Domain;$baseDirectory\LiteStreaming.Repository;$baseDirectory\LiteStreaming.WebApi;$baseDirectory\LiteStreaming.AdministrativeApp;"
$reportPath = Join-Path -Path ($baseDirectory) -ChildPath "LiteStreaming.XunitTest\TestResults"
$coverageXmlPath = Join-Path -Path ($reportPath) -ChildPath "coveragereport"
$filefilters = "$baseDirectory\LiteStreaming.DataSeeders\**;-$baseDirectory\Migrations.MsSqlServer\**;-$baseDirectory\Migrations.MySqlServer\**;-$baseDirectory\AngularApp\**;-$baseDirectory\LiteStreaming.AdministrativeApp\Views\**;"

# Fun��o para matar processos com base no nome do processo que estajam em execu��o 
function Stop-ProcessesByName {
    $processes = Get-Process | Where-Object { $_.ProcessName -like 'dotnet*' } | Where-Object { $_.MainWindowTitle -eq '' }
    if ($processes.Count -gt 0) {
        $processes | ForEach-Object { Stop-Process -Id $_.Id -Force }
    }
}


function Remove-TestResults {    
    if (Test-Path $reportPath) {
        Remove-Item -Recurse -Force $reportPath
    }
}

 function Wait-TestResults {
    $REPEAT_WHILE = 0
    while (-not (Test-Path $reportPath)) {
        echo "Agaurdando TestResults..."
        Start-Sleep -Seconds 10        
        if ($REPEAT_WHILE -eq 6) { break }
        $REPEAT_WHILE = $REPEAT_WHILE + 1
    }

    $REPEAT_WHILE = 0
    while (-not (Test-Path $coverageXmlPath)) {
        echo "Agaurdando Coverage Report..."
        Start-Sleep -Seconds 10        
        if ($REPEAT_WHILE -eq 6) { break }
        $REPEAT_WHILE = $REPEAT_WHILE + 1
    }   

 } 

# Encerra qualquer processo em segundo plano relacionado
Stop-ProcessesByName
# Exclui todo o conte�do da pasta TestResults, se existir
Remove-TestResults

dotnet clean > $null 2>&1
dotnet build $projectTestPath > $null 2>&1
if ($args -contains "-w") {

    $watchProcess = Start-Process "dotnet" -ArgumentList "watch", "test", "--configuration Release", "--project ./LiteStreaming.XunitTest/LiteStreaming.XunitTest.csproj", "--collect:""XPlat Code Coverage;Format=opencover""", "/p:CollectCoverage=true", "/p:CoverletOutputFormat=cobertura" -PassThru
    Invoke-Item $coverageXmlPath\index.html
    $watchProcess.WaitForExit()
}
else {
    dotnet test ./LiteStreaming.XunitTest/LiteStreaming.XunitTest.csproj --results-directory $reportPath  /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover"
    reportgenerator -reports:$projectTestPath/coverage.cobertura.xml -targetdir:$coverageXmlPath -reporttypes:"Html;lcov;" -sourcedirs:$sourceDirs -filefilters:-$filefilters 
    Wait-TestResults
    Invoke-Item $coverageXmlPath\index.html
}  

 Stop-ProcessesByName; 
 Exit 