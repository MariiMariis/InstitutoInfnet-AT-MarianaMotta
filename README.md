# Markdown File

# Installar ef migrations
```powershell
dotnet tool install --global dotnet-ef --version 5.0.9
```
# Migrations
Executados na pasta do projeto Data, utilizando NuGet PM:
```powershell
Add-migration InitialMig -Context FabricantesContext -Project Data -StartupProject Fabricantes.Api -Verbose
update-database -Context FabricantesContext -Verbose
```

#Migrations com Identity
NuGet PM:
```powershell
dotnet ef migrations add AddMigrationsCRUDFabricante --startup-project Presentation --project Presentation --context LoginContext --output-dir Areas\Identity\Data\Migrations

dotnet ef database update --startup-project Presentation --project Presentation --context LoginContext
```