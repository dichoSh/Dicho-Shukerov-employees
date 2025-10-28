using Employees.Services;
using Employees.Services.Interfaces;
using Scalar.AspNetCore;

namespace Employees
{
    public class Startup(IConfiguration configuration)
    {
        private readonly string corsPolicy = "cors";
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(corsPolicy, policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
            });

            services.AddControllers();

            services.AddOpenApi("doc");

            AddServices(services);

        }

        public virtual void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(op =>
                {
                    op.OpenApiRoutePattern = "/openapi/doc.json";
                    op.Servers = configuration.GetSection("ScalarServers").Get<string[]>()!
                    .Select(x => new ScalarServer(x))
                    .ToArray();
                });
            }
            app.UseCors(corsPolicy);

            app.UseAuthorization();

            app.MapControllers();


        }

        protected static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IEmployeesFileReader, CsvEmployeesFileReader>();
        }
    }
}
