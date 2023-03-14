# Set variable of the scaffolded entities output folder location.
$entityFolderPath = "Entities";

# Change directory.
Set-Location $entityFolderPath;
# Remove all files in this directory.
Remove-Item *.cs;
# Move to parent directory.
Set-Location ..

# Scaffold command.
dotnet ef dbcontext scaffold "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=turbo_bootcamp;" Npgsql.EntityFrameworkCore.PostgreSQL -d -f -c TurboBootcampDbContext -v -o $entityFolderPath --no-onconfiguring

# After you have successfully scaffolded the entities, don't forget to remove the parameterless constructor in your DbContext class.