set prefix=Rusty.Template
set root=..\..\
set rootedPrefix=%root%%prefix%
set dirPrefix=%root%\src\%prefix%
set project=%rootedPrefix%.Infrastructure\%prefix%.Infrastructure.csproj 
set startupProject=%rootedPrefix%.Presentation\%prefix%.Presentation.csproj
set configuration=Debug
set connectionString="Server=RUSTY;Initial Catalog=ApiTest;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True"
set context=ScaffoldedDbContext
set contextDir=%dirPrefix%.Infrastructure\Database
set contextNamespace=%prefix%.Infrastructure.Database
set namespace=%prefix%.Domain
set outputDir=%dirPrefix%.Domain\Scaffolded

dotnet ef dbcontext scaffold --project %project% --startup-project %startupProject% --configuration %configuration% %connectionString% "Microsoft.EntityFrameworkCore.SqlServer " --context %context% --context-dir %contextDir% --context-namespace %contextNamespace% --namespace %namespace% --output-dir %outputDir% --no-onconfiguring --force 