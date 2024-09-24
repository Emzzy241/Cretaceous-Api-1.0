using CretaceousApi.Models;
using Microsoft.EntityFrameworkCore;
using CretaceousApi.Data;
using CretaceousApi.Repositories;
using CretaceousApi.Services;
using CretaceousApi.Controllers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Since we're building a web API that means there are no views, so we only add controllers as a service with the line builder.Services.AddControllers();; this is unlike in MVC apps where we add both controllers and views with builder.Services.AddControllersWithViews();.
// Add services to the container
builder.Services.AddControllers();

// Enable CORS globally
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Adding the middleware for our DbContext
builder.Services.AddDbContext<CretaceousApiContext>(
                  dbContextOptions => dbContextOptions
                    .UseMySql(
                      builder.Configuration["ConnectionStrings:DefaultConnection"], 
                      ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"]
                    )
                  )
                );

                /*
                  Explanation:
DbContext Registration: This registers the CretaceousDbContext to manage your database connections.
Scoped Service Registration: Both AnimalService and AnimalRepository are registered using AddScoped. This means they will be created once per request and disposed after the request ends.
Mapping Controllers: app.MapControllers(); is responsible for setting up the API routing for your controllers.
                */

// Register your services and repositories
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IAnimalService, AnimalService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Swagger is a service that automatically documents the available endpoints in our application
// The builder.Services.AddEndpointsApiExplorer(); code enables Swagger documentation to do its job; it exposes our API's endpoints for documentation and other things. Later, we'll learn how to use tools via ApiExplorer that will let us control what endpoints are visible to Swagger documentation.
var app = builder.Build();

// Configure the HTTP request pipeline.
// Since we're building a web API that means there are no views, so we only add controllers as a service with the line builder.Services.AddControllers();; this is unlike in MVC apps where we add both controllers and views with builder.Services.AddControllersWithViews();.


if (app.Environment.IsDevelopment())
{
    // This if statement checks if our application is being run in a development environment, and if so configures our HTTP pipeline to actually use Swagger and Swagger UI.
    // Swagger UI is the very cool user interface for our documentation that we can always find at http://localhost:<port>/swagger/v1/swagger.json.
    app.UseSwagger();
    app.UseSwaggerUI();

    /*
        We only have Swagger configured to run during development, because it's recommend to only be used in development. While Swagger UI can be used to document API endpoints publicly, this needs to be done with care and intention in order to not expose sensitive data. As we'll see, we can use Swagger UI to send actual requests to our API. If we make that publicly available, a malicious user could access key information about our API or change our API in ways that we don't want.

        So, Swagger is usually used for developers in development, and public-facing documentation is created elsewhere. That said, we can configure what endpoints are visible and accessible through Swagger, and use Swagger documentation effectively as public-facing documentation. However, using Swagger documentation could potentially slow up our applications performance. Considerations like these need to be made before using Swagger documentation in production. We'll revisit this topic when we learn how to document our API's endpoints, and soon we'll see exactly what Swagger documentation looks like!

        Next, we configure the app.MapControllers(); middleware:

        app.MapControllers();
        

        With app.MapControllers();, we're configuring our app to rely on attributes that we add to our API controllers and actions to properly route HTTP requests. This is in contrast to what we used with our MVC apps, in which we set up a default routing pattern:

        app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
        );
    */
}
else
{
    app.UseHttpsRedirection();
    // Updating Program.cs to Not Redirect to HTTPS in Development​
// We're going to update Program.cs to not redirect to HTTPS when we run our apps during development. While we've already set up a developer security certificate, Postman can still run into problems with HTTPS and ASP.NET Core apps. To avoid the trouble with Postman, we'll stick to using HTTP during development.
    // We've added an else statement that handles adding HTTPS redirection if we are not in development. This ensures that HTTPS redirection is only used during production, and we can use HTTP in development.
}

app.UseAuthorization();

app.MapControllers();

app.Run();


/* NOTE explaining appsettings.Development.json and appsettings.json
    Filtering Hosts and Configuring Logging in appsettings.json​
Let's get to know the new "Logging" and "AllowedHosts" keys in the boilerplate for appsettings.json and appsettings.Development.json.

The "AllowedHosts" key lets us specify the host names that can access our API. When we set the value "*", it means that any host can make an API call to our API. If we set a value of "example.com;localhost", it means that requests to our API can only be made from example.com or localhost. Read more about this here.

With "Logging", we are configuring how logging happens in our application. By "logging", we mean how we log information about any requests, events, or bugs in our application. Default logging is implicitly configured when we create our WebApplicationBuilder after calling WebApplication.CreateBuilder(args) in Program.cs. This is just like how appsettings.json is implicitly loaded as a configuration file. When we specify a "Logging" key in appsettings.json, we can further configure how we want logging to happen in our applications.

With the "LogLevel" key, we get to decide what we want logged: warnings, errors, everything that happens in our app, or just the general idea of what's happening in our app? Visit this section of the MS Docs to learn about every log level. We have two log levels specified:

"Default": "Information" means that the default configuration for all logging providers in our application should be at the "information" log level, which is supposed to track the general flow of the app.
"Microsoft.AspNetCore": "Warning" means that any warnings that happen within the Microsoft.AspNetCore category should be logged. The Microsoft.AspNetCore category includes Microsoft.AspNetCore.Builder. Think of "category" like a namespace.
While the defaults are okay for both the "Logging" and "AllowedHosts" keys in appsettings.json, we'll update appsettings.Development.json to log information that's relevant to both ASP.NET Core and EF Core:

appsettings.Development.json

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Trace",
      "Microsoft.AspNetCore": "Information",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
 

With the above change, we've configured our logging to trace any activity within the Microsoft category, which includes ASP.NET Core and EF Core namespaces. The log level "Trace" will log the most detailed messages, and is great for development! However, this level of logging would slow up production applications, so we are not including it in our appsettings.json.

Notice that we're setting "Microsoft.AspNetCore": "Information". Shouldn't this be covered in our "Microsoft" category? Yes, but only if we haven't included a more specific category than "Microsoft", and we've done just that when we list "Microsoft.AspNetCore": "Warning" in our appsettings.json — this is a more specific category. That means the logging level set for "Microsoft.AspNetCore" will take precedence over the logging level for "Microsoft". If we want "Microsoft.AspNetCore" to be set to anything other than "Warning" in development, we need to specify a new value for "Microsoft.AspNetCore" in appsettings.Development.json, and that's exactly what we've done. To learn more about logging, visit the MD Docs on Logging.

*/