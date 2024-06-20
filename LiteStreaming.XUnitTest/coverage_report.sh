#!/bin/bash

# Define o diretório base atual
baseDirectory=$(pwd)

# Define o caminho do projeto de teste
projectTestPath="$baseDirectory/LiteStreaming.XUnitTest"

# Verifica se o caminho do projeto de teste existe
if [ ! -d "$projectTestPath" ]; then
    # Redefine o diretório base para o diretório pai
    baseDirectory=$(dirname "$baseDirectory")
    
    # Atualiza o caminho do projeto de teste com o novo diretório base
    projectTestPath="$baseDirectory/LiteStreaming.XUnitTest"
fi

# Define o caminho do projeto Angular
projectAngular=$(realpath "$baseDirectory/AngularApp")

# Define os diretórios de origem
sourceDirs="$baseDirectory/LiteStreaming.Application:$baseDirectory/LiteStreaming.Domain:$baseDirectory/LiteStreaming.Repository:$baseDirectory/LiteStreaming.WebApi:$baseDirectory/LiteStreaming.AdministrativeApp"

# Define o caminho do relatório de testes
reportPath="$projectTestPath/TestResults"

# Define o caminho do relatório de cobertura XML
coverageXmlPath="$projectTestPath/TestResults/coveragereport"

# Define os filtros de arquivos
filefilters="$baseDirectory/LiteStreaming.DataSeeders/**,-$baseDirectory/Migrations.MsSqlServer/**,-$baseDirectory/Migrations.MySqlServer/**,-$baseDirectory/AngularApp/**,-$baseDirectory/LiteStreaming.AdministrativeApp/Views/**"

# Executa testes unitários sem restore e build e gera o relatório de cobertura do Backend
dotnet clean > /dev/null 2>&1
dotnet test "$projectTestPath/LiteStreaming.XunitTest.csproj" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover"
reportgenerator -reports:"$projectTestPath/coverage.cobertura.xml" -targetdir:"$coverageXmlPath" -reporttypes:"Html;lcov" -sourcedirs:"$sourceDirs" -filefilters:"-$filefilters"

# Verifica se a pasta node_modules existe, e se não existir, executa npm install
if [ ! -d "$projectAngular/node_modules" ]; then
    (cd "$projectAngular" && npm install)
fi

# Executa testes unitários e gera o relatório de cobertura do Frontend
(cd "$projectAngular" && npm run test:coverage)
