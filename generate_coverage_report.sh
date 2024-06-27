#!/bin/bash

# Diretório base
baseDirectory=$(pwd)
projectTestPath="$baseDirectory/LiteStreaming.XUnitTest"
sourceDirs="$baseDirectory/LiteStreaming.Application:$baseDirectory/LiteStreaming.Domain:$baseDirectory/LiteStreaming.Repository:$baseDirectory/LiteStreaming.WebApi:$baseDirectory/LiteStreaming.AdministrativeApp"
reportPath="$projectTestPath/TestResults"
coverageXmlPath="$reportPath/coveragereport"
filefilters="$baseDirectory/LiteStreaming.DataSeeders/**:-$baseDirectory/Migrations.MsSqlServer/**:-$baseDirectory/Migrations.MySqlServer/**:-$baseDirectory/AngularApp/**:-$baseDirectory/LiteStreaming.AdministrativeApp/Views/**"

# Função para matar processos com base no nome do processo que estejam em execução
stop_processes_by_name() {
    pkill -f 'dotnet'
}

# Função para remover o diretório de resultados de testes, se existir
remove_test_results() {
    if [ -d "$reportPath" ]; then
        rm -rf "$reportPath"
    fi
}

# Função para esperar pelos resultados dos testes
wait_test_results() {
    local REPEAT_WHILE=0
    while [ ! -d "$reportPath" ]; do
        echo "Aguardando TestResults..."
        sleep 10
        if [ $REPEAT_WHILE -eq 6 ]; then break; fi
        REPEAT_WHILE=$((REPEAT_WHILE + 1))
    done

    REPEAT_WHILE=0
    while [ ! -d "$coverageXmlPath" ]; do
        echo "Aguardando Coverage Report..."
        sleep 10
        if [ $REPEAT_WHILE -eq 6 ]; then break; fi
        REPEAT_WHILE=$((REPEAT_WHILE + 1))
    done
}

# Encerra qualquer processo em segundo plano relacionado
stop_processes_by_name

# Exclui todo o conteúdo da pasta TestResults, se existir
remove_test_results

# Limpa e compila o projeto
dotnet clean > /dev/null 2>&1
dotnet build "$projectTestPath" > /dev/null 2>&1

# Verifica se o argumento -w foi passado
if [[ "$*" == *"-w"* ]]; then
    # Executa os testes em modo de observação
    dotnet watch test --configuration Release --project "$projectTestPath/LiteStreaming.XunitTest.csproj" --collect:"XPlat Code Coverage;Format=opencover" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura &
    wait $!
    xdg-open "$coverageXmlPath/index.html"
else
    # Executa os testes e gera o relatório de cobertura
    dotnet test "$projectTestPath/LiteStreaming.XunitTest.csproj" --results-directory "$reportPath" -p:CollectCoverage=true -p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover"
    reportgenerator -reports:"$projectTestPath/coverage.cobertura.xml" -targetdir:"$coverageXmlPath" -reporttypes:"Html;lcov;" -sourcedirs:"$sourceDirs" -filefilters:"-$filefilters"
    wait_test_results
    xdg-open "$coverageXmlPath/index.html"
fi

# Encerra qualquer processo em segundo plano relacionado novamente
stop_processes_by_name

exit 0
