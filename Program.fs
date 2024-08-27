namespace NewsFeedApp

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.EntityFrameworkCore
open NewsFeedApp

module Program =

    [<EntryPoint>]
    let main args =
        let builder = WebApplication.CreateBuilder(args)

        // Add services to the container.
        builder.Services.AddScoped<INewsRepository, NewsRepository>()
        builder.Services.AddScoped<INewsService, NewsService>()
        builder.Services.AddControllers()

        let app = builder.Build()

        // Configure the HTTP request pipeline.
        if app.Environment.IsDevelopment() then
            app.UseDeveloperExceptionPage()

        app.UseHttpsRedirection()
        app.UseAuthorization()
        app.MapControllers()

        app.Run()

        0 // Return an integer exit code
``
