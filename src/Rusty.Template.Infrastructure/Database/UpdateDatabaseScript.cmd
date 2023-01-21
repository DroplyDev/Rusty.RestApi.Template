set prefix=Rusty.Template
set root=..\..\
set rootedPrefix=%root%%prefix%
set dirPrefix=%root%src\%prefix%
set project=%rootedPrefix%.Infrastructure\%prefix%.Infrastructure.csproj 
set startupProject=%rootedPrefix%.Presentation\%prefix%.Presentation.csproj
set configuration=Debug
set connectionString="Server=RUSTY;Initial Catalog=ApiTest;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True"
set context=AppDbContext
set contextDir=%dirPrefix%.Infrastructure\Database
set contextNamespace=%prefix%.Infrastructure.Database
set entityNamespace=%prefix%.Domain
set entityDir=%dirPrefix%.Domain\Scaffolded

dotnet ef dbcontext scaffold --project %project% --startup-project %startupProject% --configuration %configuration% %connectionString% "Microsoft.EntityFrameworkCore.SqlServer " --context %context% --context-dir %contextDir% --context-namespace %contextNamespace% --namespace %entityNamespace% --output-dir %entityDir% --no-onconfiguring --force