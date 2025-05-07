using Microsoft.Extensions.Options;

namespace Web.API.Endpoints
{
    public static class CorsPolicyExtensions
    {
        public static void ConfigureCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
               opt.AddPolicy("ReactCrosPolicity", builder =>
               builder.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader());
            });
        }
    }
}
