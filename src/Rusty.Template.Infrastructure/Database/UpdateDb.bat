        dotnet ef dbcontext scaffold --project BlotterFX.Server.Infrastructure\BlotterFX.Server.Infrastructure.csproj
            --startup-project BlotterFX.Server.Api\BlotterFX.Server.Api.csproj --configuration Debug "" +
            "Server=RUSTY;Initial Catalog=WeatherForecastTest;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True"
        "Microsoft.EntityFrameworkCore.SqlServer " --data-annotations --context TradingInternalContext --context-dir Database --context-namespace Rusty.Template.Infrastructure.Database
        --namespace Rusty.Template.Domain\Scaffolded --output-dir ..\Rusty.Template.Domain\Scaffolded --no-onconfiguring --force &&
   dotnet ef migrations script --project Rusty.Template.Infrastructure\Rusty.Template.Infrastructure.csproj --startup-project Rusty.Template.Presentation\Rusty.Template.Presentation.csproj 
   --context Rusty.Template.Infrastructure.Database.AppDbContext --configuration Debug 20221129190310_InitialCreate 20221129190310_InitialCreate --output script.sql --force