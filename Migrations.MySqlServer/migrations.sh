### MySqlSever Add Migrations 

## Migrations.Application
dotnet ef migrations add Initial -c MySqlServerContext -p ./Migrations.MySqlServer/Migrations.MySqlServer.csproj -s ./LiteStreaming.WebApi -o Migrations.Application
dotnet ef migrations add Update-Databae-Context -c MySqlServerContext -p ./Migrations.MySqlServer/Migrations.MySqlServer.csproj -s ./LiteStreaming.WebApi -o Migrations.Application
dotnet ef database update -c MySqlServerContext -p ./Migrations.MySqlServer/Migrations.MySqlServer.csproj -s ./LiteStreaming.WebApi

## Migrations.Administrative
dotnet ef migrations add Initial -c MySqlServerContextAdministrative -p ./Migrations.MySqlServer/Migrations.MySqlServer.csproj -s ./LiteStreaming.AdministrativeApp -o Migrations.Administrative
dotnet ef migrations add Create-PefilMapping -c MySqlServerContextAdministrative -p ./Migrations.MySqlServer/Migrations.MySqlServer.csproj -s ./LiteStreaming.AdministrativeApp -o Migrations.Administrative
dotnet ef migrations add Update-Database-Context -c MySqlServerContextAdministrative -p ./Migrations.MySqlServer/Migrations.MySqlServer.csproj -s ./LiteStreaming.AdministrativeApp -o Migrations.Administrative

dotnet ef database update -c MySqlServerContextAdministrative -p ./Migrations.MySqlServer/Migrations.MySqlServer.csproj -s ./LiteStreaming.AdministrativeApp


### Return to a state creatred by Mingrations
dotnet ef database update 20231221234827_InitialCreate
dotnet ef database update 20231222054303_Changes-Props_Email_Password_To_Value_Objects

dotnet ef database update 20231222054303_Changes-relationship-between-Card-and-CardBrand

