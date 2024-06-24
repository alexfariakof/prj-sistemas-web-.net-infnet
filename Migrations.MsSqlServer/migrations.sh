### MsSqlSever Add Migrations 

## Migrations.Application
dotnet ef migrations add Initial -c MsSqlServerContext -p ./Migrations.MsSqlServer/Migrations.MsSqlServer.csproj -s ./LiteStreaming.WebApi -o Migrations.Application
dotnet ef migrations add Update-Configurations-Perfil -c MsSqlServerContext -p ./Migrations.MsSqlServer/Migrations.MsSqlServer.csproj -s ./LiteStreaming.WebApi -o Migrations.Application

dotnet ef database update -c MsSqlServerContext -p ./Migrations.MsSqlServer/Migrations.MsSqlServer.csproj -s ./LiteStreaming.WebApi

## Migrations.Administrative
dotnet ef migrations add Initial -c MsSqlServerContextAdministrative -p ./Migrations.MsSqlServer/Migrations.MsSqlServer.csproj -s ./LiteStreaming.WebApi -o Migrations.Administrative
dotnet ef migrations add Initial -c MsSqlServerContextAdministrative -p ./Migrations.MsSqlServer/Migrations.MsSqlServer.csproj -s ./LiteStreaming.AdministrativeApp -o Migrations.Administrative
dotnet ef migrations add Create-PefilMapping -c MsSqlServerContextAdministrative -p ./Migrations.MsSqlServer/Migrations.MsSqlServer.csproj -s ./LiteStreaming.AdministrativeApp -o Migrations.Administrative
dotnet ef migrations add Update-Configurations-Perfil -c MsSqlServerContextAdministrative -p ./Migrations.MsSqlServer/Migrations.MsSqlServer.csproj -s ./LiteStreaming.AdministrativeApp -o Migrations.Administrative

dotnet ef database update -c MsSqlServerContextAdministrative -p ./Migrations.MsSqlServer/Migrations.MsSqlServer.csproj -s ./LiteStreaming.AdministrativeApp


# Return to a state creatred by Mingrations
dotnet ef database update 20231221234827_InitialCreate
dotnet ef database update 20231222054303_Changes-Props_Email_Password_To_Value_Objects
